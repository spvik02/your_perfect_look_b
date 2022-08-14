using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yplBackend.Model;

namespace yplBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppusersController : ControllerBase
    {
        private readonly ypldbContext _context;

        public AppusersController(ypldbContext context)
        {
            _context = context;
        }

        // GET: api/Appusers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appuser>>> GetAppusers()
        {
            return await _context.Appusers.ToListAsync();
        }

        //Authorize for secure Api by require token

        // GET: api/Appusers/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Appuser>> GetAppuser(string id)
        {
            var appuser = await _context.Appusers.FindAsync(id);

            if (appuser == null)
            {
                return NotFound();
            }

            return appuser;
        }

        // PUT: api/Appusers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppuser(string id, Appuser appuser)
        {
            if (id != appuser.IdUser)
            {
                return BadRequest();
            }

            //var appuserBefore = await _context.Appusers.FindAsync(id);
            //if (appuserBefore == null)
            //{
            //    return NotFound();
            //}
            //appuser.Email = appuserBefore.Email;

            _context.Entry(appuser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppuserExists(id))
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

        // POST: api/Appusers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Appuser>> PostAppuser(Appuser appuser)
        {
            _context.Appusers.Add(appuser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppuserExists(appuser.IdUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAppuser", new { id = appuser.IdUser }, appuser);
        }

        // DELETE: api/Appusers/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAppuser(string id)
        {
            var appuser = await _context.Appusers.FindAsync(id);
            if (appuser == null)
            {
                return NotFound();
            }

            _context.Appusers.Remove(appuser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppuserExists(string id)
        {
            return _context.Appusers.Any(e => e.IdUser == id);
        }
    }
}
