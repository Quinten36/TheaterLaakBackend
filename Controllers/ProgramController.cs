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
        public async Task<ActionResult<IEnumerable<TheaterLaakBackend.Models.Program>>> GetPrograms(
            [FromQuery(Name = "title")] string? titleArtistSearch,
            [FromQuery(Name = "omschrijving")] string? omschrijvingSearch,
            //TODO: wat moet er gezocht worden op omschrijving?
            [FromQuery(Name = "startdate")] string? startDateSearch,
            [FromQuery(Name = "endDate")] string? endDateSearch,
            [FromQuery(Name = "costs")] string? costsSearch,
            [FromQuery(Name = "page")] int? page,
            [FromQuery(Name = "sort")] string? sortField
            )
        {

            var query = _context.Programs
                .Include(program => program.Genres)
                .Include(program => program.Group)
                .ThenInclude(group => group.Artists )
                .AsQueryable();

            if (titleArtistSearch != null) query = query.Where
                (program => 
                    program.Group.Artists.Any(artist => artist.Name.Contains(titleArtistSearch) ||
                    program.Title.Contains(titleArtistSearch))
                );

            var a = _context.Programs;


            var programs = await query.ToListAsync();
                // .Include(program => program.Genres)
                // .Include(program => program.Group).ThenInclude(group => group.Artists )
                // .ToListAsync();
            programs.ForEach(program => program.Genres?.ForEach(genre => genre.Programs = new List<Models.Program>()));
            programs.ForEach(program => program.Group.Artists.ForEach(artist => artist.Groups = new List<Group>()));

            return programs;
            //TODO: deze functie misschien in een andere call?
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
        
        // SEARCH: api/Program/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TheaterLaakBackend.Models.Program>>> SearchProgram([FromQuery] string searchTerm)
        {
            var filteredPrograms = await _context.Programs.Where(program => program.Title.Contains(searchTerm)).ToListAsync();
            return filteredPrograms;
        }



    }


}
