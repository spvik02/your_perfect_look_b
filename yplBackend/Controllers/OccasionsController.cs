using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yplBackend.Model;

namespace yplBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccasionsController : ControllerBase
    {
        private readonly ypldbContext _context;

        public OccasionsController(ypldbContext context)
        {
            _context = context;
        }

        // GET: api/Occasions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Occasion>>> GetOccasions()
        {
            return await _context.Occasions.ToListAsync();
        }

        // GET: api/Occasions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Occasion>> GetOccasion(int id)
        {
            var occasion = await _context.Occasions.FindAsync(id);

            if (occasion == null)
            {
                return NotFound();
            }

            return occasion;
        }

        // PUT: api/Occasions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOccasion(int id, Occasion occasion)
        {
            if (id != occasion.IdOccasion)
            {
                return BadRequest();
            }

            _context.Entry(occasion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OccasionExists(id))
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

        // POST: api/Occasions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Occasion>> PostOccasion(Occasion occasion)
        {
            _context.Occasions.Add(occasion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOccasion", new { id = occasion.IdOccasion }, occasion);
        }

        // DELETE: api/Occasions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOccasion(int id)
        {
            var occasion = await _context.Occasions.FindAsync(id);
            if (occasion == null)
            {
                return NotFound();
            }

            _context.Occasions.Remove(occasion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OccasionExists(int id)
        {
            return _context.Occasions.Any(e => e.IdOccasion == id);
        }
    }
}
