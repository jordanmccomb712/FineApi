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
    [Route("api/[controller]")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private readonly FineContext _context;

        public FinesController(FineContext context)
        {
            _context = context;
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
