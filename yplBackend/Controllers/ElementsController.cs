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
    public class ElementsController : ControllerBase
    {
        private readonly ypldbContext _context;

        public ElementsController(ypldbContext context)
        {
            _context = context;
        }

        // GET: api/Elements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Element>>> GetElements()
        {
            return await _context.Elements.ToListAsync();
        }

        // GET: api/ElementsByCategory/5
        [HttpGet("/api/ElementsByCategory/{id}")]
        public async Task<ActionResult<IEnumerable<Element>>> GetElementsByCategory(int id)
        {
            return await _context.Elements
                .Where(p => p.IdCategory == id)
                .ToListAsync();
        }

        // GET: api/ElementsByCategoryAndUser/5/dfjgnjfklsmkfmklsmflsks
        [HttpGet("/api/ElementsByCategoryAndUser/{categoryId}/{userId}")]
        public async Task<ActionResult<IEnumerable<Element>>> GetElementsByCategoryAndUser(int categoryId, String userId)
        {
            return await _context.Elements
                .Where(p => p.IdCategory == categoryId && p.IdUser == userId)
                .ToListAsync();
        }

        [HttpGet("/api/SearchElements/{userId}/{categoryId}/{name}")]
        public async Task<ActionResult<IEnumerable<Element>>> SearchElements(String userId, int categoryId, String name)
        {
            if (categoryId == 6) return await _context.Elements
                .Where(p => p.IdUser == userId && ( p.NameElement.Contains(name) || p.Note.Contains(name)))
                .ToListAsync();
            else return await _context.Elements
                .Where(p => p.IdUser == userId && p.IdCategory == categoryId && (p.NameElement.Contains(name) || p.Note.Contains(name)))
                .ToListAsync();
        }

        // GET: api/Elements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Element>> GetElement(int id)
        {
            var element = await _context.Elements
                .Where(e => e.IdElement == id)
                .FirstOrDefaultAsync(); 
                
                //.FindAsync(id);

            if (element == null)
            {
                return NotFound();
            }

            return element;
        }

        // PUT: api/Elements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElement(int id, Element element)
        {
            if (id != element.IdElement)
            {
                return BadRequest();
            }

            _context.Entry(element).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElementExists(id))
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

        // POST: api/Elements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Element>> PostElement(Element element)
        {
            _context.Elements.Add(element);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElement", new { id = element.IdElement }, element);
        }

        // DELETE: api/Elements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElement(int id)
        {
            var element = await _context.Elements.FindAsync(id);
            if (element == null)
            {
                return NotFound();
            }

            _context.Elements.Remove(element);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElementExists(int id)
        {
            return _context.Elements.Any(e => e.IdElement == id);
        }
    }
}
