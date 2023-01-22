
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheaterLaakBackend.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace TheaterLaakBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TheaterDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        // private readonly RoleManager<Gebruiker> _roleManager;

        public LoginController(TheaterDbContext context, UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/Login
        [Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _context.Accounts.ToListAsync();
        }

        //TODO: zorg dat je nog steeds met username en email kan inloggen
        [HttpPost]
        // [Route("")]
        public async Task<ActionResult<Account>> LoginUser([FromBody] Account gebruikerLogin)
        {
            HashPWs hashpasswords = new HashPWs();
            var salt = "";
            var hashedPassword = "";
            var account = _context.Accounts
                  .Where(a => a.UserName == gebruikerLogin.UserName || a.Email == gebruikerLogin.Email)
                  .Select(a => new { a.Password })
                  .FirstOrDefault();

            if (account != null)
            {
                if (account.Password.Contains(':'))
                {
                    var saltPassword = account.Password.Split(':');
                    salt = saltPassword[1];
                    hashedPassword = saltPassword[0];
                }
                else
                {
                    hashedPassword = gebruikerLogin.Password;
                }
            }

            if (!(hashedPassword == hashpasswords.Sha256(gebruikerLogin.Password, salt).Split(':')[0]))
            {
                return Unauthorized(new { message = "Inlog gegevens verkeerd." });
            }

            //get username from email
            var username = _context.Accounts.Single(a => a.UserName == gebruikerLogin.UserName|| a.Email == gebruikerLogin.Email).UserName;
            var _user = await _userManager.FindByNameAsync(username);
            //if user is found
            if (_user != null)
            {
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"));
                //prepare settings for the jwt token
                var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName), new Claim("Id", _user.Id) };
                var roles = await _userManager.GetRolesAsync(_user);
                //get all the roles of the user
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));
                //make the jwt token
                var tokenOptions = new JwtSecurityToken
                (
                  issuer: "http://localhost:5086",
                  audience: "http://localhost:5086",
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: signingCredentials
                );
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
            }
            return Unauthorized();
        }

        [HttpPut]
        [Route("wwvergeten/{gebruikersnaam}")]
        public async Task<ActionResult> WWvergeten(string gebruikersnaam)
        {
            HashPWs hashPWs = new HashPWs();
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == gebruikersnaam || a.Email == gebruikersnaam);

            if (account == null)
            {
                return BadRequest(new { message = "Account bestaad niet" });
            }

            var newPassword = hashPWs.generateNewPassword();

            MailSender ms = new MailSender();

            ms.sendMail(account.Email, "Hier is uw nieuwe wacthwoord : " + newPassword + ". Verander hem zo snel mogelijk. De optie om dit te doen staat onder mijn account", "Wachtwoord vergeten ");

            //verander wachtwoord van account
            _context.Accounts.FirstOrDefault(a => a.UserName == gebruikersnaam || a.Email == gebruikersnaam).Password = hashPWs.Sha256(newPassword);
            _context.SaveChanges();
            return Ok();
        }
    }
}

