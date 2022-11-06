using Admin.Server.Repositories.Constants;
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
    public class ConstantsController: ControllerBase
    {
        private readonly IConstantsRepository _db;
        public ConstantsController(IConstantsRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Constants>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Constants>> GetConstant(int id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Constants>> Puts(int id, Constants constants)
        {
            return await _db.Update(id, constants);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Constants>> PostTopic(Constants constants)
        {
            var newConstant = await _db.Create(constants);

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
