using System;
using System.Text;
// using System.Web.Http;
using System.Web;
using System.Net;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Contexts;
using TheaterLaakBackend.Models;


namespace TheaterLaakBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationController : ControllerBase
	{
		private readonly TheaterDbContext _context;

		public ReservationController(TheaterDbContext context)
		{
				_context = context;
		}

		// GET: api/Reservation
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
		{
			if (_context.Reservations == null)
			{
				return NotFound();
			}
			return await _context.Reservations.ToListAsync();
		}

		// GET: api/reservation/filtered/
		[HttpGet("filtered")]
		public async Task<ActionResult<IEnumerable<Reservation>>> GetFilteredReservations()
		{
			if (_context.Reservations == null)
			{
				return NotFound();
			}

			var startDate = DateTime.ParseExact(HttpContext.Request.Query["start"], "MM-dd-yyyy",System.Globalization.CultureInfo.InvariantCulture);
			var endDate = DateTime.ParseExact(HttpContext.Request.Query["end"], "MM-dd-yyyy",System.Globalization.CultureInfo.InvariantCulture);

			List<Reservation> reservations = _context.Reservations.Where(r => r.Start > startDate && r.End < endDate).ToList();

			return reservations;
		}

		// GET: api/Reservation/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Reservation>> GetReservation(int id)
		{
			if (_context.Reservations == null)
			{
				return NotFound();
			}
			var reservation = await _context.Reservations.FindAsync(id);

			if (reservation == null)
			{
				return NotFound();
			}

			return reservation;
		}

		// PUT: api/Reservation/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutReservation(int id, Reservation reservation)
		{
			if (id != reservation.Id)
			{
				return BadRequest();
			}

			_context.Entry(reservation).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ReservationExists(id))
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

		// POST: api/Reservation
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
		{
			if (_context.Reservations == null)
			{
				return Problem("Entity set 'TheaterDbContext.Reservations'  is null.");
			}
			_context.Reservations.Add(reservation);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
		}

		// DELETE: api/Reservation/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReservation(int id)
		{
			if (_context.Reservations == null)
			{
				return NotFound();
			}
			var reservation = await _context.Reservations.FindAsync(id);
			if (reservation == null)
			{
				return NotFound();
			}

			_context.Reservations.Remove(reservation);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ReservationExists(int id)
		{
			return (_context.Reservations?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
