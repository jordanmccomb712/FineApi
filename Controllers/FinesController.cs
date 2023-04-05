using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinesApi.Models;

namespace FineApi.Controllers
{
    [Route("api/Fines")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private readonly FineContext _context;

        public FinesController(FineContext context)
        {
            _context = context;
            _context.Fines.Add(new Fine ("zcff412dvXFD", "Johnson", 123.50 ,new DateOnly(2022, 12 ,31), false ));
            _context.Fines.Add(new Fine ("GRxsd751Jfda", "Smith", 146.00 ,new DateOnly(2021, 6 ,15), false ));
            _context.Fines.Add(new Fine ("kcSF76FW9dA2", "McDonald", 220.15 ,new DateOnly(2021, 6 ,2), false ));
            _context.Fines.Add(new Fine ("jfmc62FHWm1G", "Black", 120.75 ,new DateOnly(2023, 1 ,14), false ));
            _context.Fines.Add(new Fine ("MVU37fnf7FAv", "Simmons", 180.00 ,new DateOnly(2022, 3 ,21), false ));
            _context.Fines.Add(new Fine ("XgWID73djw12", "Roth", 157.50 ,new DateOnly(2022, 8 ,17), false ));
            _context.SaveChangesAsync();

        }

        // GET: api/Fines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fine>>> GetFines()
        {
          if (_context.Fines == null)
          {
              return NotFound();
          }
            return await _context.Fines.ToListAsync();
        }

        // GET: api/Fines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fine>> GetFine(string id)
        {
          if (_context.Fines == null)
          {
              return NotFound();
          }
            var fine = await _context.Fines.FindAsync(id);

            if (fine == null)
            {
                return NotFound();
            }

            return fine;
        }

        // PUT: api/Fines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFine(string id, Fine fine)
        {
            if (id != fine.FineID)
            {
                return BadRequest();
            }

            _context.Entry(fine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FineExists(id))
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

        // POST: api/Fines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fine>> PostFine(Fine fine)
        {
          if (_context.Fines == null)
          {
              return Problem("Entity set 'FineContext.Fines'  is null.");
          }
            _context.Fines.Add(fine);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FineExists(fine.FineID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetFine), new { id = fine.FineID }, fine);
        }

        // DELETE: api/Fines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFine(string id)
        {
            if (_context.Fines == null)
            {
                return NotFound();
            }
            var fine = await _context.Fines.FindAsync(id);
            if (fine == null)
            {
                return NotFound();
            }

            _context.Fines.Remove(fine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FineExists(string id)
        {
            return (_context.Fines?.Any(e => e.FineID == id)).GetValueOrDefault();
        }
    }
}
