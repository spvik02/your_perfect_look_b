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
    public class RatingsController : ControllerBase
    {
        private readonly ypldbContext _context;

        public RatingsController(ypldbContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        // GET: api/Ratings/5
        [HttpGet("/api/Ratings/GetRatingByUserAndCombination/{idUser}/{idCombination}")]
        public async Task<ActionResult<int>> GetRatingByUserAndCombination(String idUser, int idCombination)
        {
            var rating = await _context.Ratings.Where(r => r.IdUser == idUser && r.IdCombination == idCombination).FirstOrDefaultAsync();

            if (rating == null)
            {
                return null;
            }

            return rating.Rating1;
        }

        [HttpGet("/api/Ratings/GetRatingByCombination/{idCombination}")]
        public async Task<ActionResult<double>> GetRatingByCombination(int idCombination)
        {
            var rating = await _context.Ratings.Where(r => r.IdCombination == idCombination)
                .Select(e => e.Rating1).AverageAsync();
                //.ToListAsync();

            if (rating == null)
            {
                return null;
            }

            return rating;
        }

        [HttpPut("/api/Ratings/CreateRating")]
        public async Task<IActionResult> CreateRating(Rating rating)
        {

            var rat = await _context.Ratings.Where(r => r.IdCombination == rating.IdCombination && r.IdUser == rating.IdUser).FirstOrDefaultAsync();
            if (rat == null)
            {
                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();
            }
            else
            {
                rat.Rating1 = rating.Rating1;
                _context.Entry(rat).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rat.IdRating))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, Rating rating)
        {
            if (id != rating.IdRating)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.IdRating }, rating);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(e => e.IdRating == id);
        }
    }
}
