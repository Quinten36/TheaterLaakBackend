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

namespace TheaterLaakBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistratieController : ControllerBase
    {
        private readonly TheaterDbContext _context;

        public RegistratieController(TheaterDbContext context)
        {
            _context = context;
        }
      [HttpPost]
public async Task<ActionResult<Account>> AddUser([FromBody] Account Account)
{
    // Check if account already exists
        AccountInformationChecker AIC = new AccountInformationChecker(_context);
        var UitslagUserNameCheck = AIC.BestaandeGebruikerCheck(Account.Username , Account.Email);
        if(UitslagUserNameCheck != "Succes"){
             return BadRequest(new { message = UitslagUserNameCheck });
        }
        var UitslagPasswordCheck = AIC.PasswordCheck(Account.Username , Account.Password);
    if( UitslagPasswordCheck != "Succes"){
        return BadRequest(new { message = UitslagPasswordCheck });
    }

    // Add the new account to the database
    await _context.Accounts.AddAsync(Account);
    await _context.SaveChangesAsync();
    return Ok();
        }
    }
}



