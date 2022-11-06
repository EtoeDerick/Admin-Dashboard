using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.FrontEnd.AppUsers;
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
    public class AppUsersController: ControllerBase
    {
        private readonly IAppUsersRepository _db;
        public AppUsersController(IAppUsersRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetConstant(string id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<AppUser>> Puts(string id, AppUser user)
        {
            return await _db.Update(id, user);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppUser>> Post(AppUser user)
        {
            var newUser = await _db.Create(user);

            return CreatedAtAction(nameof(GetConstant), new { id = newUser.Id }, newUser);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeleteConstant(string id)
        {
            await _db.Delete(id);
        }
    }
}
