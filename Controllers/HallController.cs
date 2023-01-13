using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Models;

namespace TheaterLaakBackend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HallController : ControllerBase
  {
    private readonly TheaterDbContext _context;

    public HallController(TheaterDbContext context)
    {
<<<<<<< HEAD
        private readonly TheaterDbContext _context;

        public HallController(TheaterDbContext context)
        {
            _context = context;
        }

        // GET: api/Hall
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hall>>> GetHalls()
        {
          if (_context.Halls == null)
          {
              return NotFound();
          }
            return await _context.Halls.ToListAsync();
        }

        // GET: api/Hall/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hall>> GetHall(int id)
        {
          if (_context.Halls == null)
          {
              return NotFound();
          }
            var hall = await _context.Halls.FindAsync(id);

            if (hall == null)
            {
                return NotFound();
            }

            return hall;
        }

        // GET: api/hall/filtered
    [HttpGet("filtered")]
    public async Task<ActionResult<IEnumerable<Reservation>>> GetFilteredReservations()
    {
      if (_context.Reservations == null)
      {
        return NotFound();
      }

      // checked startdate not after end date
      var startDate = DateTime.ParseExact(HttpContext.Request.Query["start"], "MM-dd-yyyy",System.Globalization.CultureInfo.InvariantCulture);
      var endDate = DateTime.ParseExact(HttpContext.Request.Query["end"], "MM-dd-yyyy",System.Globalization.CultureInfo.InvariantCulture);
      var minCap = Int32.Parse(HttpContext.Request.Query["minCap"]);
      var maxCap = Int32.Parse(HttpContext.Request.Query["maxCap"]);

      //nog cap inbouwen.
      List<Reservation> reservations = _context.Reservations.Where(r => r.End > startDate && r.Start > endDate).ToList();
      List<Reservation> toDelete = new List<Reservation>();
      foreach (Reservation reservation in reservations) 
      {
        reservation.capacity = _context.Halls.First(h => h.Id == reservation.HallId).Capacity;
        if (reservation.capacity < minCap || reservation.capacity > maxCap)
          toDelete.Add(reservation);
        // code block to be executed
      }
      // List<Reservation> reservations = _context.Reservations.Where(r => r.Start < endDate && r.End > startDate).ToList();

      foreach (Reservation reservation in toDelete) 
      {
        reservations.Remove(reservation);
      }

      return reservations;
    }

        // PUT: api/Hall/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHall(int id, Hall hall)
        {
            if (id != hall.Id)
            {
                return BadRequest();
            }

            _context.Entry(hall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallExists(id))
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

        // POST: api/Hall
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hall>> PostHall(Hall hall)
        {
          if (_context.Halls == null)
          {
              return Problem("Entity set 'TheaterDbContext.Halls'  is null.");
          }
            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHall", new { id = hall.Id }, hall);
        }

        // DELETE: api/Hall/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            if (_context.Halls == null)
            {
                return NotFound();
            }
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HallExists(int id)
        {
            return (_context.Halls?.Any(e => e.Id == id)).GetValueOrDefault();
        }
=======
      _context = context;
>>>>>>> 85d6e9737d9d8154043693d0866ce1c437ee6707
    }

    // GET: api/Hall
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hall>>> GetHalls()
    {
      if (_context.Halls == null)
      {
        return NotFound();
      }
      return await _context.Halls.ToListAsync();
    }

    // GET: api/Hall/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Hall>> GetHall(int id)
    {
      if (_context.Halls == null)
      {
        return NotFound();
      }
      var hall = await _context.Halls.FindAsync(id);

      if (hall == null)
      {
        return NotFound();
      }

      return hall;
    }

    // GET: api/hall/filtered
    [HttpGet("filtered")]
    public async Task<ActionResult<IEnumerable<Reservation>>> GetFilteredReservations()
    {
      if (_context.Reservations == null)
      {
        return NotFound();
      }

      // checked startdate not after end date
      var startDate = DateTime.ParseExact(HttpContext.Request.Query["start"], "MM-dd-yyyy",System.Globalization.CultureInfo.InvariantCulture);
      var endDate = DateTime.ParseExact(HttpContext.Request.Query["end"], "MM-dd-yyyy",System.Globalization.CultureInfo.InvariantCulture);
      var minCap = Int32.Parse(HttpContext.Request.Query["minCap"]);
      var maxCap = Int32.Parse(HttpContext.Request.Query["maxCap"]);

      //nog cap inbouwen.
      List<Reservation> reservations = _context.Reservations.Where(r => r.End > startDate && r.Start > endDate).ToList();
      List<Reservation> toDelete = new List<Reservation>();
      foreach (Reservation reservation in reservations) 
      {
        reservation.capacity = _context.Halls.First(h => h.Id == reservation.HallId).Capacity;
        if (reservation.capacity < minCap || reservation.capacity > maxCap)
          toDelete.Add(reservation);
        // code block to be executed
      }
      // List<Reservation> reservations = _context.Reservations.Where(r => r.Start < endDate && r.End > startDate).ToList();

      foreach (Reservation reservation in toDelete) 
      {
        reservations.Remove(reservation);
      }

      return reservations;
    }

    // PUT: api/Hall/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHall(int id, Hall hall)
    {
      if (id != hall.Id)
      {
        return BadRequest();
      }

      _context.Entry(hall).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!HallExists(id))
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

    // POST: api/Hall
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Hall>> PostHall(Hall hall)
    {
      if (_context.Halls == null)
      {
        return Problem("Entity set 'TheaterDbContext.Halls'  is null.");
      }
      _context.Halls.Add(hall);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetHall", new { id = hall.Id }, hall);
    }

    // DELETE: api/Hall/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHall(int id)
    {
      if (_context.Halls == null)
      {
        return NotFound();
      }
      var hall = await _context.Halls.FindAsync(id);
      if (hall == null)
      {
        return NotFound();
      }

      _context.Halls.Remove(hall);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool HallExists(int id)
    {
      return (_context.Halls?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
