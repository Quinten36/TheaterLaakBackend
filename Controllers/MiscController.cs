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
      var html = "<!DOCTYPE html><html><head></head><body><a href=\"http://localhost:3000/heeftAutherized/"+token+"\">Klik here to proceed</a></body></html>";
      return new ContentResult
      {
          Content = html,
          ContentType = "text/html"
      };
    }   
    
    // GET: api/Account
    [HttpPost]
    [Route("setPayment")]
    public ContentResult GetPayment([FromForm] string account, [FromForm] string succes, [FromForm] string reference)
    {
      string outputHTML;
      string redirect;

      if (succes == "true"){
        outputHTML = "<p style=\"color:green; font-size: 1.6em;font-weight: 600;\">Betaling gelukt</p>";
        redirect = "https://theater-laak-backend-prod.azurewebsites.net/bestellingAfronden";
      }
      else{
        outputHTML = "<p style=\"color:red; font-size: 1.6em;font-weight: 600;\">Betaling is mislukt</p>";
        redirect = "https://theater-laak-backend-prod.azurewebsites.net/winkelwagen";
      }
      var html = "<!DOCTYPE html><html><head></head><body>"+outputHTML+"<a href=\""+redirect+"\">Rond de betaling af</a></body></html>";
      return new ContentResult
      {
          Content = html,
          ContentType = "text/html"
      };
    }   
  }
}
