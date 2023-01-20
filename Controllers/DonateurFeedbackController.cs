
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using TheaterLaakBackend.Models;
using Microsoft.AspNetCore.Identity;
using TheaterLaakBackend.Contexts;

namespace TheaterLaakBackend.Controllers;
public class DonateurFeedbackController : ControllerBase
{
    private readonly TheaterDbContext _context;
    private readonly UserManager<Account> _userManager;
    private readonly SignInManager<Account> _signInManager;
    // private readonly RoleManager<Gebruiker> _roleManager;

    public DonateurFeedbackController(TheaterDbContext context, UserManager<Account> userManager, SignInManager<Account> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    [Route("api/[controller]")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedbackDonateurs>>> getFeedback()
    {
        if (_context.FeedbackDonateurs == null)
        {
            return NotFound();
        }

        return await _context.FeedbackDonateurs.ToListAsync();
    }
    [Route("api/[controller]")]
    [HttpPost]
    public async Task<ActionResult> setFeedback([FromBody] FeedbackDonateurs feedbackDonateurs)
    {
        if (feedbackDonateurs == null)
        {
            return BadRequest();
        }

        _context.FeedbackDonateurs.Add(new FeedbackDonateurs { AccountId = feedbackDonateurs.AccountId, feedbackText = feedbackDonateurs.feedbackText });
        await _context.SaveChangesAsync();
        return Ok();
    }
}