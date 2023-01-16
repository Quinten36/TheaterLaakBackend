
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Models;

namespace TheaterLaakBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TheaterDbContext _context;

        public LoginController(TheaterDbContext context)
        {
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
                 .Where(a => a.Username == username || a.Email == username)
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


