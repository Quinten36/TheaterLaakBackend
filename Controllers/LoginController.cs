
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
          if (_context.Accounts == null)
          {
            return NotFound();
          }
          return await _context.Accounts.ToListAsync();
        }

//TODO: zorg dat je nog steeds met username en emial kan inloggen
        [HttpPost]
        [Route("check")]
        public async Task<ActionResult<Account>> LoginUser([FromBody] Account gebruikerLogin)
        {
          HashPWs hashpasswords = new HashPWs();
          var salt = "";
          var hashedPassword = "";
          var account = _context.Accounts
                .Where(a => a.UserName == gebruikerLogin.UserName || a.Email == gebruikerLogin.Username)
                .Select(a => new { a.Password })
                .FirstOrDefault();
          
          if (account != null)
          {
              var saltPassword = account.Password.Split(':');
              salt = saltPassword[1];
              hashedPassword = saltPassword[0];
          }   

          if(!(account.Password == hashpasswords.Sha256(password , salt))){
            return Unauthorized(new { message = "Inlog gegevens verkeerd." });
          }

          var _user = await _userManager.FindByNameAsync(gebruikerLogin.UserName);
          if (_user != null)
              if (await _userManager.CheckPasswordAsync(_user, _user.Password))
              {
                  var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"));

                  var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
                  var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName) };
                  var roles = await _userManager.GetRolesAsync(_user);
                  foreach (var role in roles)
                      claims.Add(new Claim(ClaimTypes.Role, role));
                  var tokenOptions = new JwtSecurityToken
                  (
                      issuer: "http://localhost:5086",
                      audience: "http://localhost:5086",
                      claims: claims,
                      expires: DateTime.Now.AddMinutes(10),
                      signingCredentials: signingCredentials
                  );
                  return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
              }

          return Unauthorized();
        }
    }
}


