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
    public class ProgramController : ControllerBase
    {
        private readonly TheaterDbContext _context;

        public ProgramController(TheaterDbContext context)
        {
            _context = context;
        }

        // GET: api/Program
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TheaterLaakBackend.Models.Program>>> GetPrograms()
        {
          if (_context.Programs == null)
          {
              return NotFound();
          }
            return await _context.Programs.ToListAsync();
        }

        // GET: api/Program/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TheaterLaakBackend.Models.Program>> GetProgram(int id)
        {
          if (_context.Programs == null)
          {
              return NotFound();
          }
            var program = await _context.Programs.FindAsync(id);

            if (program == null)
            {
                return NotFound();
            }

            return program;
        }

        // PUT: api/Program/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgram(int id, TheaterLaakBackend.Models.Program program)
        {
            if (id != program.Id)
            {
                return BadRequest();
            }

            _context.Entry(program).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramExists(id))
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

        // POST: api/Program
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TheaterLaakBackend.Models.Program>> PostProgram(TheaterLaakBackend.Models.Program program)
        {
          if (_context.Programs == null)
          {
              return Problem("Entity set 'TheaterDbContext.Programs'  is null.");
          }
            _context.Programs.Add(program);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgram", new { id = program.Id }, program);
        }

        // DELETE: api/Program/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            if (_context.Programs == null)
            {
                return NotFound();
            }
            var program = await _context.Programs.FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            _context.Programs.Remove(program);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramExists(int id)
        {
            return (_context.Programs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
