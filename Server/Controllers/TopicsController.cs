using Admin.Server.Repositories.Topics;
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
    public class TopicsController: ControllerBase
    {
        private readonly ITopicsRepository _db;
        public TopicsController(ITopicsRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Topic>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic(int id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Topic>> Puts(int id, Topic topic)
        {

            return await _db.Update(id, topic);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Topic>> PostTopic(Topic topic)
        {
            var newTopic = await _db.Create(topic);

            return CreatedAtAction(nameof(GetTopic), new { id = newTopic.Id }, newTopic);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeletePastPaper(int id)
        {
            await _db.Delete(id);
        }
    }
}
