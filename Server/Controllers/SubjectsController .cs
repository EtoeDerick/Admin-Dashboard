using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Server.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _db;

        public SubjectsController(ISubjectRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Subject>> Puts(int id, Subject Subject)
        {
            
            if (id != Subject.Id)
            {
                return BadRequest();
            }

            return await _db.Update(id, Subject);
        }
        
        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject Subject)
        {
            var newsubject = await _db.Create(Subject);

            return CreatedAtAction(nameof(GetSubject), new { id = newsubject.Id }, newsubject);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExamination(int id)
        {
            var examToDelete = await _db.Get(id);

            if (examToDelete == null)
            {
                return NotFound();
            }

            await _db.Delete(examToDelete.Id);

            return Ok();
        }

    }
}
