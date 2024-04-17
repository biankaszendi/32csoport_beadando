using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogExplorer;
using BlogExplorer.Data;
using WebApplication1.Models;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteTopics>>> GetFavoriteTopics()
        {
            return await _context.FavoriteTopics.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteTopics>> GetFavoriteTopic(int id)
        {
            var favoriteTopic = await _context.FavoriteTopics.FindAsync(id);

            if (favoriteTopic == null)
            {
                return NotFound();
            }

            return favoriteTopic;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteTopic(int id, FavoriteTopics favoriteTopic)
        {
            if (id != favoriteTopic.UserId)
            {
                return BadRequest();
            }

            _context.Entry(favoriteTopic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteTopicExists(id))
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

        [HttpPost]
        public async Task<ActionResult<FavoriteTopics>> PostFavoriteTopic(FavoriteTopics favoriteTopic)
        {
            _context.FavoriteTopics.Add(favoriteTopic);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FavoriteTopicExists(favoriteTopic.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFavoriteTopic", new { id = favoriteTopic.user_id }, favoriteTopic);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteTopic(int id)
        {
            var favoriteTopic = await _context.FavoriteTopics.FindAsync(id);
            if (favoriteTopic == null)
            {
                return NotFound();
            }

            _context.FavoriteTopics.Remove(favoriteTopic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteTopicExists(int id)
        {
            return _context.FavoriteTopics.Any(e => e.user_id == id);
        }
    }
}