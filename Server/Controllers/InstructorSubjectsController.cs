using Admin.Server.Repositories.InstructorSubjects;
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
    public class InstructorSubjectsController : ControllerBase
    {
        private readonly IInstructorSubjectRepository _db;
        public InstructorSubjectsController(IInstructorSubjectRepository db)
        {
            _db = db;
        }
        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<InstructorSubject>> GetAll()
        {
            return await _db.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorSubject>> GetInstructorsubject(int id, string instructorId)
        {
            return await _db.Get(id, instructorId);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<InstructorSubject>> Puts(InstructorSubject instructorSubject)
        {
            return await _db.Update(instructorSubject);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InstructorSubject>> PostInstructorSubject(InstructorSubject instructorSubject)
        {
            var newInstructorSubject = await _db.Create(instructorSubject);

            return CreatedAtAction(nameof(GetInstructorsubject), new { id = newInstructorSubject.SubjectId,  newInstructorSubject.InstructorId }, newInstructorSubject);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeleteInstructorSubject(int id, string instructorId)
        {
            await _db.Delete(id, instructorId);
        }
    }
}
