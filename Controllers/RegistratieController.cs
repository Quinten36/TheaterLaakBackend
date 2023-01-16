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
            var UitslagUserNameCheck = AIC.BestaandeGebruikerCheck(Account.Username, Account.Email);
            if (UitslagUserNameCheck != "Succes")
            {
                return BadRequest(new { message = UitslagUserNameCheck });
            }
            var UitslagPasswordCheck = AIC.PasswordCheck(Account.Username, Account.Password);
            if (UitslagPasswordCheck != "Succes")
            {
                return BadRequest(new { message = UitslagPasswordCheck });
            }
            HashPWs HashPasswordSha256 = new HashPWs();
            Account.Password= HashPasswordSha256.Sha256(Account.Password);
            // Add the new account to the database
            await _context.Accounts.AddAsync(Account);
            await _context.SaveChangesAsync();

            VerificatieCodeGenerator VCG = new VerificatieCodeGenerator(_context);
            VCG.sendVertificatie(Account.Id, Account.Email);

            return Ok(new { id = Account.Id });
        }
        [HttpPut]
        [Route("api/validate/{AccountID}/{VeritficatieCodeInvoer}")]
        public async Task<ActionResult> ValidateUser(int AccountID, int VeritficatieCodeInvoer)
        {


            var veritficatie = _context.Verificaties.FirstOrDefault(v => v.AccountID == AccountID && v.ValidationCode == VeritficatieCodeInvoer);

            if (veritficatie == null)
            {
                return BadRequest(new { message = "Verification Code incorrect" });
            }

            _context.Accounts.FirstOrDefault(v => v.Id == AccountID).isValidated = true;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}


