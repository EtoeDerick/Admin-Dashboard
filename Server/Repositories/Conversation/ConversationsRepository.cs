using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Conversations
{
    public class ConversationsRepository : ControllerBase, IConversationsRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Conversation>> Get()
        {
            var conversations = await _context.Conversations.ToListAsync();


            return conversations.OrderBy(c => c.Date);
        }

        public async Task<Conversation> Get(int id)
        {
            return await _context.Conversations.FindAsync(id);
        }

        public async Task<ActionResult<Conversation>> Update(int id, Conversation conversation)
        {
            if (id != conversation.Id)
            {
                return BadRequest();
            }

            
            
            _context.Entry(conversation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConstantsExists(conversation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return conversation;
        }
        public async Task<Conversation> Create(Conversation conversation)
        {

            _context.Conversations.Add(conversation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ConstantsExists(conversation.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return conversation;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var constantsToDelete = await Get(id);

            if (constantsToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.Conversations.Remove(constantsToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ConstantsExists(int id)
        {
            return _context.Conversations.Any(e => e.Id == id);
        }

        
    }
}
