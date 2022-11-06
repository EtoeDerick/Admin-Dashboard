using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.Lessons;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
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
    public class LessonsController: ControllerBase
    {
        private readonly ILessonsRepository _db;
        public LessonsController(ILessonsRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Lesson>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetConstant(int id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Lesson>> Puts(int id, Lesson lesson)
        {
            return await _db.Update(id, lesson);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lesson>> Post(Lesson lesson)
        {
            var newConstant = await _db.Create(lesson);

            return CreatedAtAction(nameof(GetConstant), new { id = newConstant.Id }, newConstant);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeleteConstant(int id)
        {
            await _db.Delete(id);
        }
    }
}
