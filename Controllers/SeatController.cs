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
  public class SeatController : ControllerBase
  {
    private readonly TheaterDbContext _context;

    public SeatController(TheaterDbContext context)
    {
      _context = context;
    }

    // GET: api/Seat
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Seat>>> GetSeats()
    {
      if (_context.Seats == null)
      {
        return NotFound();
      }
      return await _context.Seats.ToListAsync();
    }

    // GET: api/Seat/disabledInHall/5
    [HttpGet("disabledInHall/{id}")]
    public async Task<ActionResult<bool>> CheckIfDisabledSeatExists(int id)
    {
      if (_context.Seats == null)
      {
        return NotFound();
      }
      var seat = _context.Seats.Where(s => s.HallId == id).Any(s => s.ForDisabled == true);

      if (seat == null)
      {
        return NotFound();
      }

      return seat;
    }

    // GET: api/Seat/byHall/5
    [HttpGet("byHall/{id}")]
    public async Task<ActionResult<List<Seat>>> GetSeatInHall(int id)
    {
      if (_context.Seats == null)
      {
        return NotFound();
      }
      var seat = _context.Seats.Include("Tickets").Where(s => s.HallId == id).ToList();

      if (seat == null)
      {
        return NotFound();
      }
      return seat;
    }

    // GET: api/Seat/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Seat>> GetSeat(int id)
    {
      if (_context.Seats == null)
      {
        return NotFound();
      }
      var seat = await _context.Seats.FindAsync(id);

      if (seat == null)
      {
        return NotFound();
      }

      return seat;
    }

    // PUT: api/Seat/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles="Medewerker, Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeat(int id, Seat seat)
    {
      if (id != seat.Id)
      {
        return BadRequest();
      }

      _context.Entry(seat).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SeatExists(id))
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

    // POST: api/Seat
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Seat>> PostSeat(Seat seat)
    {
      if (_context.Seats == null)
      {
        return Problem("Entity set 'TheaterDbContext.Seats'  is null.");
      }
      _context.Seats.Add(seat);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetSeat", new { id = seat.Id }, seat);
    }

    // DELETE: api/Seat/5
    [Authorize(Roles="Medewerker, Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeat(int id)
    {
      if (_context.Seats == null)
      {
        return NotFound();
      }
      var seat = await _context.Seats.FindAsync(id);
      if (seat == null)
      {
        return NotFound();
      }

      _context.Seats.Remove(seat);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool SeatExists(int id)
    {
      return (_context.Seats?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
