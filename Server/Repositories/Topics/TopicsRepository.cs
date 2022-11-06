using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Topics
{
    public class TopicsRepository : ControllerBase, ITopicsRepository
    {
        private readonly ApplicationDbContext _context;

        public TopicsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topic>> Get()
        {
            var topics = await _context.Topic.ToListAsync();

            foreach(var topic in topics)
            {
                topic.Subject = null;
            }

            return topics;
        }

        public async Task<Topic> Get(int id)
        {
            var topic = await _context.Topic.FindAsync(id);
            topic.Subject = null;
            if (topic == null)
            {
                return null;
            }

            return topic;
        }

        public async Task<ActionResult<Topic>> Update(int id, Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            
            
            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(topic.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return topic;
        }
        public async Task<Topic> Create(Topic topic)
        {
            topic.Subject = null;
            _context.Topic.Add(topic);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (TopicExists(topic.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return topic;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var topicToDelete = await Get(id);

            if (topicToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.Topic.Remove(topicToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TopicExists(int id)
        {
            return _context.Topic.Any(e => e.Id == id);
        }

        
    }
}
