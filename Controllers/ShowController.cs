using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Contexts;
using TheaterLaakBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace TheaterLaakBackend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ShowController : ControllerBase
  {
    private readonly TheaterDbContext _context;

    public ShowController(TheaterDbContext context)
    {
        _context = context;
    }

    // GET: api/Show
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Show>>> GetShows()
    {
      if (_context.Shows == null)
      {
        return NotFound();
      }
        return await _context.Shows.Include(show => show.Program).ToListAsync();
    }

    // GET: api/Show/limit/5
    [HttpGet("limit/{id}")]
    public async Task<ActionResult<IEnumerable<Show>>> GetLimitedShows(int id)
    {
      if (_context.Shows == null)
      {
        return NotFound();
      }

      List<Show> shows = await _context.Shows.ToListAsync();
      //TODO: alleen shows selecteren die na de dag van vandaag plaats vinden. of die populair zijn
      List<Show> limitedShow = shows.OrderBy(i => i.Start).Take(id).ToList();

      return limitedShow;
    }

    // GET: api/Show/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Show>> GetShow(int id)
    {
      if (_context.Shows == null)
      {
        return NotFound();
      }
      var show = await _context.Shows.FindAsync(id);

      if (show == null)
      {
        return NotFound();
      }

      return show;
    }

    // GET: api/Show/SeatStatus/5
    [HttpGet("SeatStatus/{id}")]
    public async Task<ActionResult<Show>> GetShowSeatStatus(int id)
    {
      if (_context.SeatShowStatus == null)
      {
        return NotFound();
      }
      var showSeatStatus = await _context.Shows
        .Include(s => s.SeatShowStatus)
        .ThenInclude(s => s.Seat)
        .FirstAsync(s => s.Id == id);

      if (showSeatStatus == null)
      {
        return NotFound();
      }

      return showSeatStatus;
    }

    // PUT: api/Show/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles="Medewerker, Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShow(int id, Show show)
    {
      if (id != show.Id)
      {
        return BadRequest();
      }

      _context.Entry(show).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ShowExists(id))
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

    // POST: api/Show
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles="Medewerker, Admin")]
    [HttpPost]
    public async Task<ActionResult<Show>> PostShow(Show show)
    {
      if (_context.Shows == null)
      {
        return Problem("Entity set 'TheaterDbContext.Shows'  is null.");
      }
      _context.Shows.Add(show);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetShow", new { id = show.Id }, show);
    }

    // DELETE: api/Show/5
    [Authorize(Roles="Medewerker, Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShow(int id)
    {
      if (_context.Shows == null)
      {
        return NotFound();
      }
      var show = await _context.Shows.FindAsync(id);
      if (show == null)
      {
        return NotFound();
      }

      _context.Shows.Remove(show);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ShowExists(int id)
    {
      return (_context.Shows?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
