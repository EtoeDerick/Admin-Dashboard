using Admin.Server.Repositories.Instructors;
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
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorRepository _db;
        public InstructorsController(IInstructorRepository db)
        {
            _db = db;
        }
        // GET: api/Instructors
        [HttpGet]
        public async Task<IEnumerable<Admin.Shared.Models.Instructor>> GetAllInstructors()
        {
            return await _db.Get();
        }
        // GET: api/Instructors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin.Shared.Models.Instructor>> GetInstructor(string id)
        {
            return await _db.Get(id);
        }
        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Instructor>> Puts(string id, Instructor instructor)
        {
            return await _db.Update(id, instructor);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Instructor>> PostInstructor(Instructor instructor)
        {
            var newInstructor = await _db.Create(instructor);

            return CreatedAtAction(nameof(GetInstructor), new { id = newInstructor.Id }, newInstructor);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeleteInstructor(string id)
        {
            await _db.Delete(id);
        }
    }
}
