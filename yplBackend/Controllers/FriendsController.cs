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
    public class FriendsController : ControllerBase
    {
        private readonly ypldbContext _context;

        public FriendsController(ypldbContext context)
        {
            _context = context;
        }

        //// GET: api/Friends
        //[HttpGet("/api/Friends/GetFriendRequests/{idUser}")]
        //public async Task<ActionResult<IEnumerable<Friend>>> GetFriendRequests(String idUser)
        //{
        //    return await _context.Friends
        //        .Where(f => f.IdFriend ==idUser && f.IsAccepted == null)
        //        .ToListAsync();
        //}

        // GET: api/Friends
        [HttpGet("/api/Friends/GetFriendRequests/{idUser}")]
        public async Task<ActionResult<IEnumerable<Appuser>>> GetFriendRequests(String idUser)
        {
            return await _context.Friends
                .Where(f => f.IdFriend == idUser && f.IsAccepted == null)
                .Join(_context.Appusers, x => x.IdUser, y => y.IdUser, (x, y) => new Appuser
                {
                    IdUser = y.IdUser,
                    Name = y.Name,
                })
                .ToListAsync();
        }
        
        // GET: api/Friends
        [HttpGet("/api/Friends/GetFriendAcceptedRequests/{idUser}")]
        public async Task<ActionResult<IEnumerable<Appuser>>> GetFriendAcceptedRequests(String idUser)
        {
            return await _context.Friends
                .Where(f => f.IdFriend == idUser && f.IsAccepted == true)
                .Join(_context.Appusers, x => x.IdUser, y => y.IdUser, (x, y) => new Appuser
                {
                    IdUser = y.IdUser,
                    Name = y.Name,
                    Email = y.Email,
                })
                .ToListAsync();
        }

        // GET: api/Friends
        [HttpGet("/api/Friends/GetUserFriends/{idUser}")]
        public async Task<ActionResult<IEnumerable<Appuser>>> GetUserFriends(String idUser)
        {
            return await _context.Friends
                .Where(f => f.IdUser == idUser && f.IsAccepted == true)
                .Join(_context.Appusers, x => x.IdFriend, y => y.IdUser, (x, y) => new Appuser
                {
                    IdUser = y.IdUser,
                    Name = y.Name,
                })
                .ToListAsync();
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetFriends()
        {
            return await _context.Friends.ToListAsync();
        }

        // GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friend>> GetFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);

            if (friend == null)
            {
                return NotFound();
            }

            return friend;
        }

        // GET: api/Friends/5
        [HttpGet("/api/Friends/GetFriendResult/{idUser}/{idFriend}")]
        public async Task<ActionResult<String>> GetFriendResult(String idUser, String idFriend)
        {
            Friend friend = await _context.Friends.Where(f => f.IdUser == idUser && f.IdFriend == idFriend).FirstOrDefaultAsync();

            if (friend == null)
            {
                return "NotFound";
            }
            else
            {
                if (friend.IsAccepted == null) return "NoAction";
                else if ((bool)friend.IsAccepted) return "Accepted";
                else return "Denied";
            }
        }

        // GET: api/Friends/5
        [HttpGet("/api/Friends/GetFriendByName/{name}")]
        public async Task<ActionResult<IEnumerable<Appuser>>> GetFriendByName(String name)
        {
            return await _context.Appusers.Where(u => u.Name.Contains(name))
                .ToListAsync();
        }

        // PUT: api/Friends/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        ///api/Friends/GetFriendByName/{name}
        public async Task<IActionResult> PutFriend(int id, Friend friend)
        {
            if (id != friend.Id)
            {
                return BadRequest();
            }

            _context.Entry(friend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
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

        // PUT: api/Friends/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/api/Friends/PutFriendStatus")]
        public async Task<IActionResult> PutFriendStatus( Friend friendPut)
        {
            Friend friend = await _context.Friends.Where(f => f.IdUser == friendPut.IdUser && f.IdFriend == friendPut.IdFriend).FirstOrDefaultAsync();
            

            if (friend == null)
            {
                return BadRequest();
            }

            friend.IsAccepted = friendPut.IsAccepted;

            _context.Entry(friend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(friend.Id))
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

        // POST: api/Friends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Friend>> PostFriend(Friend friend)
        {
            _context.Friends.Add(friend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriend", new { id = friend.Id }, friend);
        }

        // DELETE: api/Friends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendExists(int id)
        {
            return _context.Friends.Any(e => e.Id == id);
        }
    }
}
