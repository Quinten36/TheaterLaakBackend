using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Contexts;
using TheaterLaakBackend.Models;
using Microsoft.AspNetCore.Authorization;

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
            [FromQuery(Name = "title")] string? title,
            [FromQuery(Name = "artist")] string? artist,
            [FromQuery(Name = "description")] string? description,
            [FromQuery(Name = "startDate")] string? startDateSearch,
            [FromQuery(Name = "endDate")] string? endDateSearch,
            [FromQuery(Name = "costMax")] string? costMaxSearch,
            [FromQuery(Name = "sort")] string? sortField
            )
        {
            var queryBuilder = _context.Programs.Include(program => program.Group).ThenInclude(group => group.Artists);
            if (costMaxSearch != null) queryBuilder.Include(program => program.Shows);
            var query = queryBuilder.AsQueryable();

            if (title != null) query = query.Where(program => program.Title.Contains(title));
            if (artist != null) query = query.Where(program => program.Group.Artists.Any(a => a.Name.Contains(artist)));
            if (description != null) query = query.Where(program => program.Description.Contains(description));
            
            if (startDateSearch != null)
            {
                try
                {
                    query = query.Where(program => program.BeginDate.AddMonths(1).Date <= DateTime.Parse(startDateSearch));
                }
                catch (FormatException e)
                {
                    // Silently ignore date parsing errors since this can only occur when tampering with the frontend
                    Console.WriteLine("Incorrect start date format");
                    Console.WriteLine(e);
                }
            }

            if (endDateSearch != null)
            {
                try
                {
                    query = query.Where(program => program.EndDate.Date >= DateTime.Parse(endDateSearch));
                }
                catch (FormatException e)
                {
                    // Silently ignore date parsing errors since this can only occur when tampering with the frontend
                    Console.WriteLine("Incorrect end date format");
                    Console.WriteLine(e);
                }
            }

            if (costMaxSearch != null) {
                try
                {
                    query = query.Where(program => program.Shows.Any(show =>
                        show.FirstClassPrice <= Convert.ToDouble(costMaxSearch) ||
                        show.SecondClassPrice <= Convert.ToDouble(costMaxSearch) ||
                        show.ThirdClassPrice <= Convert.ToDouble(costMaxSearch)));
                }
                catch (FormatException e)
                {
                    // Silently ignore date parsing errors since this can only occur when tampering with the frontend
                    Console.WriteLine("Incorrect max costs format");
                    Console.WriteLine(e);
                }

            }

            query = sortField switch
            {
                "datum" => query.OrderBy(program => program.BeginDate),
                "kostenLaagHoog" => query.OrderBy(program => program.Shows.Min(show => show.ThirdClassPrice)),
                "kostenHoogLaag" => query.OrderBy(program => program.Shows.Max(show => show.FirstClassPrice)),
                _ => query
            };
            var programs = await query.ToListAsync();
            
            programs.ForEach(program => program.Group.Artists.ForEach(artist => artist.Groups = new List<Group>()));

            return programs;
            //TODO: deze functie misschien in een andere call?
            //TODO: datum kan niet lager zijn dan vandaag
            //TODO: characters moeten geescaped worden
        }

        // GET: api/Program/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TheaterLaakBackend.Models.Program>> GetProgram(int id)
        {
          if (_context.Programs == null)
          {
              return NotFound();
          }
          var program = await _context.Programs.Where(p => p.Id == id)
            .Include(p => p.Shows)
            .Include(p => p.Genres)
            .Include(p => p.Group)
            .ThenInclude(g => g.Artists)
            .FirstOrDefaultAsync();

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
