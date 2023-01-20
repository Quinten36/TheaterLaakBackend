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

namespace TheaterLaakBackend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MiscController : ControllerBase
  {
    private readonly TheaterDbContext _context;
    private readonly UserManager<Account> _userManager;

    public MiscController(TheaterDbContext context, UserManager<Account> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    // GET: api/Account
    [HttpPost]
    [Route("setToken")]
    public ContentResult GetAccounts([FromForm] string token)
    {
      // if (_context.Accounts == null)
      // {
      //   return NotFound();
      // }
      Console.WriteLine(token);
      
      var html = "<!DOCTYPE html><html><head></head><body><a href=\"http://localhost:3000/heeftAutherized/"+token+"\">Klik here to proceed</a></body></html>";
      return new ContentResult
      {
          Content = html,
          ContentType = "text/html"
      };
    }   
  }
}