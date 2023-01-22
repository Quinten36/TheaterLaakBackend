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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheaterLaakBackend.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace TheaterLaakBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistratieController : ControllerBase
    {
        private readonly ILogger<RegistratieController> _logger;
        private readonly TheaterDbContext _context;
        private readonly UserManager<Account> _userManager;

        public RegistratieController(ILogger<RegistratieController> logger, TheaterDbContext context, UserManager<Account> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> AddUser([FromBody] Account Account)
        {
            try
            {
                // Check if account already exists
                AccountInformationChecker AIC = new AccountInformationChecker(_context);
                var UitslagUserNameCheck = AIC.BestaandeGebruikerCheck(Account.UserName, Account.Email);
                if (UitslagUserNameCheck != "Succes")
                {
                    return BadRequest(new { message = UitslagUserNameCheck });
                }

                var UitslagPasswordCheck = AIC.PasswordCheck(Account.UserName, Account.Password);
                if (UitslagPasswordCheck != "Succes")
                {
                    return BadRequest(new { message = UitslagPasswordCheck });
                }

                HashPWs HashPasswordSha256 = new HashPWs();
                Account.Password = HashPasswordSha256.Sha256(Account.Password);
                // Add the new account to the database
                var resultaat = await _userManager.CreateAsync(Account, Account.Password);
                await _context.SaveChangesAsync();

                var _user = await _userManager.FindByNameAsync(Account.UserName);
                await _userManager.AddToRoleAsync(_user, "Gast");


                VerificatieCodeGenerator VCG = new VerificatieCodeGenerator(_context);
                VCG.sendVertificatie(Account.Id, Account.Email);
                
                return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : Ok(new { id = Account.Id });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                _logger.LogCritical(e.StackTrace);
            }

            // return ;
            throw new Exception();
        }

        [HttpPut]
        [Route("api/validate/{AccountID}/{VeritficatieCodeInvoer}")]
        public async Task<ActionResult> ValidateUser(string AccountID, int VeritficatieCodeInvoer)
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


        [HttpGet]
        [Route("/OpnieuwVerzendenVerificatieMail/{AccountID}")]
        public async Task<ActionResult> opnieuwVerzendenVerificatieMail(string AccountID)
        {

            var account = await _userManager.FindByIdAsync(AccountID);

            if (account == null)
            {
                return BadRequest("account bestaat niet");
            }

            VerificatieCodeGenerator VCG = new VerificatieCodeGenerator(_context);
            VCG.sendVertificatie(account.Id, account.Email);

            return Ok(account.Email);
        }
    }

}


