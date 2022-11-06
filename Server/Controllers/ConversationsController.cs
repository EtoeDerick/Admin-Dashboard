using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.Conversations;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ConversationsController: ControllerBase
    {
        private readonly IConversationsRepository _db;
        public ConversationsController(IConversationsRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Conversation>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conversation>> GetConstant(int id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Conversation>> Puts(int id, Conversation conversation)
        {
            return await _db.Update(id, conversation);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conversation>> Post(Conversation conversation)
        {
            var newConstant = await _db.Create(conversation);

            return CreatedAtAction(nameof(GetConstant), new { id = newConstant.Id }, newConstant);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _db.Delete(id);
        }
    }
}
