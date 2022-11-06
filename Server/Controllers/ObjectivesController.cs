using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.Lessons;
using Admin.Server.Repositories.Objectives;
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
    public class ObjectivesController: ControllerBase
    {
        private readonly IObjectivesRepository _db;
        public ObjectivesController(IObjectivesRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Objective>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Objective>> GetConstant(int id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Objective>> Puts(int id, Objective objective)
        {
            return await _db.Update(id, objective);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Objective>> Post(Objective objective)
        {
            var newConstant = await _db.Create(objective);

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
