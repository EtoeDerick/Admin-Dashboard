using Admin.Server.Repositories.Announcements;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncemntRepository _db;

       // static readonly string[] scopeRequiredByApi = new string[] { "Api.Read", "Api.ReadWrite" };
        public AnnouncementsController(IAnnouncemntRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Announcement>> GetAll()
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Announcement>> Puts(int id, Announcement announcement)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _db.Update(id, announcement);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Announcement>> PostAnnouncement(Announcement announcement)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var newAnnouncement = await _db.Create(announcement);

            return CreatedAtAction(nameof(GetAnnouncement), new { id = newAnnouncement.Id }, newAnnouncement);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeleteAnnouncement(int id)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            await _db.Delete(id);
        }
    }

}
