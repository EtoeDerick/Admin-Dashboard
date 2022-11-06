using Admin.Server.Repositories.Mcqs;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCQsController : ControllerBase
    {
        private readonly IMCQRepository _db;
        public MCQsController(IMCQRepository db)
        {
            _db = db;
        }
        // GET: api/<MCQsController>
        [HttpGet]
        public async Task<IEnumerable<MCQ>> GetAll()
        {
            return await _db.Get();
        }

        // GET api/<MCQsController>/5
        //[HttpGet("{id}")]
        public async Task<ActionResult<MCQ>> GetMCQ(int id)
        {
            return await _db.Get(id);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<MCQ>> GetMCQs(string id)
        {
            return await _db.GetMCQByPastPaperId(id);
        }
        // POST api/<MCQsController>
        [HttpPost]
        public async Task<ActionResult<MCQ>> Post(MCQ mcq)
        {
            var newMCQ = await _db.Create(mcq);
            return CreatedAtAction(nameof(GetMCQ), new { id = newMCQ.Value.Id }, newMCQ);
        }

        // PUT api/<MCQsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MCQsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
