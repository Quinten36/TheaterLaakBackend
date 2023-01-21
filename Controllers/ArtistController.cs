using System;
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
    public class ArtistController : ControllerBase
    {
        private readonly TheaterDbContext _context;

        public ArtistController(TheaterDbContext context)
        {
            _context = context;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            return await _context.Artists.ToListAsync();
        }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // GET: api/artist/byBand/:id
        [HttpGet("byBand/{id}")]
        public async Task<ActionResult<List<Artist>>> GetArtistByBand(int id)
        { 
            List<Artist> aa = new List<Artist>();
          if (_context.Artists == null)
          {
              return NotFound();
          }
            //Get all the artists whole belongs to the group that has been requested
            foreach (Group a in await _context.Groups.Include("Artists").Where(g => g.Id == id).ToListAsync())
            {
                aa = a.Artists.ToList();
            }

            if (aa == null)
            {
                return NotFound();
            }

            return aa;
        }

        // PUT: api/Artist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            if (id != artist.Id)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
          if (_context.Artists == null)
          {
              return Problem("Entity set 'TheaterDbContext.Artists'  is null.");
          }
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // POST: api/Artist/lijst
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("lijst")]
        public async Task<ActionResult<Artist>> PostArtist(List<Artist> artists)
        {
          if (_context.Artists == null)
          {
              return Problem("Entity set 'TheaterDbContext.Artists'  is null.");
          }
          // do Lookup sruff
          foreach (var i in artists) {
            Console.log(i);
          }
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            if (_context.Artists == null)
            {
                return NotFound();
            }
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return (_context.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
