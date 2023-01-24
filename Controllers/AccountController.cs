using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using TheaterLaakBackend.Models;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheaterLaakBackend.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace TheaterLaakBackend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly TheaterDbContext _context;
    private readonly UserManager<Account> _userManager;

    public AccountController(TheaterDbContext context, UserManager<Account> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    // GET: api/Account
    [Authorize(Roles="Medewerker")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
      if (_context.Accounts == null)
      {
        return NotFound();
      }
      return await _context.Accounts.ToListAsync();
    }   


    // GET: api/Account/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(string id)
    {
      if (_context.Accounts == null)
      {
        return NotFound();
      }
      var account = await _context.Accounts.FindAsync(id);

      if (account == null)
      {
        return NotFound();
      }

      return account;
    }

    // GET: api/Account/5
    [HttpGet("checkDonatie/{totaal}")]
    public async Task<ActionResult<String>> GetCheckDonateur(int totaal)
    {
      if (_context.Accounts == null)
      {
        return NotFound();
      }

      //get user from jwt token
      // string token = HttpContext.Request.Query["Authorization"];
      Request.Headers.TryGetValue("Authorization", out StringValues token);
      var JWT = token.ToString().Split(' ')[1];

      var tokenR = new JwtSecurityToken(JWT);
      string email = tokenR.Claims.First(c => c.Type == "Email").Value;
      var Username = _context.Accounts.FirstOrDefault(a => a.Email == email).UserName;
      var _user = await _userManager.FindByNameAsync(Username);

      if (totaal > 1000) {
        //do role give thing
        if (!await _userManager.IsInRoleAsync(_user, "Donateur")) {
          await _userManager.AddToRoleAsync(_user, "Donateur");
        }
      } else {
        //revoke donateur role
        if (await _userManager.IsInRoleAsync(_user, "Donateur")) {
          var output = await _userManager.RemoveFromRoleAsync(_user, "Donateur");
        }
        // return "{\"message\": \"Niet genoeg gedoneerd\"}";
      }

      var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"));
      //prepare settings for the jwt token
      var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
      var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName), new Claim("Id", _user.Id)};
      var roles = await _userManager.GetRolesAsync(_user);
      //get all the roles of the user
      foreach (var role in roles)
        claims.Add(new Claim(ClaimTypes.Role, role));
      //make the jwt token
      var tokenOptions = new JwtSecurityToken
      (
        issuer: "https://theater-laak-backend-prod.azurewebsites.net/",
        audience: "https://theater-laak-backend-prod.azurewebsites.net/",
        claims: claims,
        expires: DateTime.Now.AddMinutes(120),
        signingCredentials: signingCredentials
      );
      return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });

      // return "{\"message\": \"U bent nu een geregisteerde donateur \"}";
    }

    // GET: api/Account/name/5
    [HttpGet("name/{id}")]
    public async Task<ActionResult<Account>> GetAccountName(int id)
    {
      if (_context.Accounts == null)
      {
        return NotFound();
      }
      var account = await _context.Accounts.FindAsync(id);
      account.IsDonator = false;
      account.Password = null;
      account.Email = null;
      account.PhoneNumber = null;
      account.IsSubscribed = false;

      if (account == null)
      {
        return NotFound();
      }

      return account;
    }

    // PUT: api/Account/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccount(string id, Account account)
    {
      if (id != account.Id)
      {
        return BadRequest();
      }

      _context.Entry(account).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AccountExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // DELETE: api/Account/5
    [Authorize(Roles="Medewerker")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
      if (_context.Accounts == null)
      {
        return NotFound();
      }
      var account = await _context.Accounts.FindAsync(id);
      if (account == null)
      {
        return NotFound();
      }

      _context.Accounts.Remove(account);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool AccountExists(string id)
    {
      return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
