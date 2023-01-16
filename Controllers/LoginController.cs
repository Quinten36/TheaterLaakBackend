
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

        [HttpGet]
        [Route("api/Login/{username}/{password}")]
        public async Task<ActionResult<Account>> LoginUser(string username, string password)
        {
            HashPWs hashpasswords = new HashPWs();
            var salt = "";
            var hashedPassword = "";
            var account = _context.Accounts
                 .Where(a => a.UserName == username || a.Email == username)
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

            return Ok();
        }
    }
}


