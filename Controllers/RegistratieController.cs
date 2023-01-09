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
    Console.WriteLine("AddUser called");
    Console.WriteLine(Account.Email);
    await _context.Accounts.AddAsync(Account);
    await _context.SaveChangesAsync();
    return Ok();
        }
    }
}
