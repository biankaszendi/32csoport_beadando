using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogBeadando.Data;
using BlogBeadando.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavTopicsController : ControllerBase
    {
        private readonly DataContext _context;

        public FavTopicsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/FavTopics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavTopic>>> GetFavoTopics()
        {
            return await _context.FavTopics.ToListAsync();
        }

        // GET: api/FavTopics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavTopic>> GetFavTopic(int id)
        {
            var favTopic = await _context.FavTopics.FindAsync(id);

            if (favTopic == null)
            {
                return NotFound();
            }

            return favTopic;
        }

        // PUT: api/FavTopics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavTopic(int id, FavTopic favTopic)
        {
            if (id != favTopic.UserId)
            {
                return BadRequest();
            }

            _context.Entry(favTopic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavTopicExists(id))
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

        // POST: api/FavTopics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavTopic>> PostFavTopic(FavTopic favTopic)
        {
            _context.FavTopics.Add(favTopic);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FavTopicExists(favTopic.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFavTopic", new { id = favTopic.UserId }, favTopic);
        }

        // DELETE: api/FavTopics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavTopic(int id)
        {
            var favTopic = await _context.FavTopics.FindAsync(id);
            if (favTopic == null)
            {
                return NotFound();
            }

            _context.FavTopics.Remove(favTopic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavTopicExists(int id)
        {
            return _context.FavTopics.Any(e => e.UserId == id);
        }
    }
}