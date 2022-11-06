using Admin.Server.Repositories;
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
    public class PastPapers2n3Controller: ControllerBase
    {
        private readonly IPastPaper2n3Repository _db;
        public PastPapers2n3Controller(IPastPaper2n3Repository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<PastPaper>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PastPaper>> GetPastPaper(string id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<PastPaper>> Puts(string id, PastPaper pastPaper)
        {

            return await _db.Update(id, pastPaper);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PastPaper>> PostSubject(PastPaper pastPaper)
        {
            
            var newPastPaper = await _db.Create(pastPaper);

            return CreatedAtAction(nameof(GetPastPaper), new { id = newPastPaper.Id }, newPastPaper);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeletePastPaper(string id)
        {
            await _db.Delete(id);
        }
    }
}
