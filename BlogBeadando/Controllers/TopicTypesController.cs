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
    public class TopicTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public TopicTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TopicTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicType>>> GetTopicTypes()
        {
            return await _context.TopicTypes.ToListAsync();
        }

        // GET: api/TopicTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicType>> GetTopicType(int id)
        {
            var topicType = await _context.TopicTypes.FindAsync(id);

            if (topicType == null)
            {
                return NotFound();
            }

            return topicType;
        }

        // PUT: api/TopicTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopicType(int id, TopicType topicType)
        {
            if (id != topicType.TopicTypeId)
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

        // POST: api/TopicTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TopicType>> PostTopicType(TopicType topicType)
        {
            _context.TopicTypes.Add(topicType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopicType", new { id = topicType.TopicTypeId }, topicType);
        }

        // DELETE: api/TopicTypes/5
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
            return _context.TopicTypes.Any(e => e.TopicTypeId == id);
        }
    }
}