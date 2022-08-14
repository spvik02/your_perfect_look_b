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
    public class CombinationsController : ControllerBase
    {
        private readonly ypldbContext _context;

        public CombinationsController(ypldbContext context)
        {
            _context = context;
        }

        // GET: api/Combinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Combination>>> GetCombinations()
        {
            return await _context.Combinations.ToListAsync();
        }

        // GET: api/Combinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Combination>> GetCombination(int id)
        {
            var combination = await _context.Combinations.FindAsync(id);

            if (combination == null)
            {
                return NotFound();
            }

            return combination;
        }

        // GET: api/CombinationsByCategoryAndUser/5/dfjgnjfklsmkfmklsmflsks
        [HttpGet("/api/CombinationsByOccasionAndUser/{occasionId}/{userId}")]
        public async Task<ActionResult<IEnumerable<Combination>>> GetCombinationsByOccasionAndUser(int occasionId, String userId)
        {
            return await _context.Combinations
                .Where(p => p.IdOccasion == occasionId && p.IdUser == userId)
                .Include(c => c.ElementInCombinations)
                //.ThenInclude(e=>e.IdElementNavigation)
                .ToListAsync();
        }

        // GET: api/CombinationsByCategoryAndUser/5/dfjgnjfklsmkfmklsmflsks
        [HttpGet("/api/CombinationsByIdCombination/{combiId}")]
        public async Task<ActionResult<IEnumerable<Element>>> GetCombinationsByIdCombination(int combiId)
        {
            return await _context.ElementInCombinations.Where(x => x.IdCombination == combiId)
                .Join(_context.Elements, x => x.IdElement, y => y.IdElement,
                (x, y) => new Element
                {
                    IdElement = y.IdElement,
                    NameElement = y.NameElement,
                    IdCategory = y.IdCategory,
                    Price = y.Price,
                    Note = y.Note,
                    PicPath = y.PicPath,
                    Pic = y.Pic,
                    IdUser = y.IdUser
                }).ToListAsync();

            //return await _context.Elements.Where(x => x.ElementInCombinations.SingleOrDefault(y => y.IdCombination == combiId) != null).ToListAsync();        

        }

        [HttpGet("/api/Combinations/GetCombinationsOfFriendsByUserId/{idUser}")]
        public async Task<ActionResult<IEnumerable<Combination>>> GetCombinationsOfFriendsByUserId(String idUser)
        {
            return await _context.Friends
                .Where(f => f.IdUser == idUser && f.IsAccepted == true)
                .Join(_context.Combinations, x => x.IdFriend, y => y.IdUser,
                (x, y) => new Combination
                {
                    IdCombination = y.IdCombination,
                    NameCombination = y.NameCombination,
                    Note = y.Note,
                    IdOccasion = y.IdOccasion,
                    IdUser = y.IdUser,
                    IsPosted = y.IsPosted
                }).Where(c => c.IsPosted == true).ToListAsync();
        }

        [HttpGet("/api/Combinations/GetCombinationsWithElement/{idElement}")]
        public async Task<ActionResult<IEnumerable<Combination>>> GetCombinationsWithElement(int idElement)
        {
            return await _context.ElementInCombinations
                .Where(e => e.IdElement == idElement)
                //.Select(e=>e.IdCombination).Distinct()
                .Join(_context.Combinations, x => x.IdCombination, y => y.IdCombination,
                (x, y) => new Combination
                {
                    IdCombination = y.IdCombination,
                    NameCombination = y.NameCombination,
                    Note = y.Note,
                    IdOccasion = y.IdOccasion,
                    IdUser = y.IdUser,
                    IsPosted = y.IsPosted
                }).ToListAsync();
        }

        [HttpGet("/api/Combinations/GetFriendPostedCombination/{userId}")]
        public async Task<ActionResult<IEnumerable<Combination>>> GetFriendPostedCombination(String userId)
        {
            return await _context.Combinations
                .Where(p => p.IdUser == userId && p.IsPosted == true)
                .ToListAsync();
        }

        // PUT: api/Combinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombination(int id, Combination combination)
        {
            if (id != combination.IdCombination)
            {
                return BadRequest();
            }

            _context.Entry(combination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CombinationExists(id))
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/api/Combinations/PutCombinationStatus")]
        public async Task<IActionResult> PutCombinationStatus(Combination combinationPut)
        {
            Combination combination = await _context.Combinations.Where(c => c.IdCombination == combinationPut.IdCombination).FirstOrDefaultAsync();

            if (combination == null)
            {
                return BadRequest();
            }

            combination.IsPosted = combinationPut.IsPosted;

            _context.Entry(combination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CombinationExists(combination.IdCombination))
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

        // POST: api/Combinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Combination>> PostCombination(Combination combination)
        {
            _context.Combinations.Add(combination);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCombination", new { id = combination.IdCombination }, combination);
        }

        // DELETE: api/Combinations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombination(int id)
        {
            var combination = await _context.Combinations.FindAsync(id);
            if (combination == null)
            {
                return NotFound();
            }

            _context.Combinations.Remove(combination);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CombinationExists(int id)
        {
            return _context.Combinations.Any(e => e.IdCombination == id);
        }
    }
}
