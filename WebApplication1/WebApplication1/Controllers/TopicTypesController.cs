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
    public class TopicTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public TopicTypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicTypes>>> GetTopicTypes()
        {
            return await _context.TopicTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicTypes>> GetTopicType(int id)
        {
            var topicType = await _context.TopicTypes.FindAsync(id);

            if (topicType == null)
            {
                return NotFound();
            }

            return topicType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopicType(int id, TopicTypes topicType)
        {
            if (id != topicType.Id)
            {
                return BadRequest();
            }

            _context.Entry(topicType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicTypeExists(id))
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
        public async Task<ActionResult<TopicTypes>> PostTopicType(TopicTypes topicType)
        {
            _context.TopicTypes.Add(topicType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopicType", new { id = topicType.Id }, topicType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopicType(int id)
        {
            var topicType = await _context.TopicTypes.FindAsync(id);
            if (topicType == null)
            {
                return NotFound();
            }

            _context.TopicTypes.Remove(topicType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TopicTypeExists(int id)
        {
            return _context.TopicTypes.Any(e => e.Id == id);
        }
    }
}