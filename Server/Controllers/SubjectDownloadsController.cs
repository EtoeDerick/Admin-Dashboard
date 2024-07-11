using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.FrontEnd.SubjectDownloads;
using Admin.Shared.Dtos;
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
    public class SubjectDownloadsController : ControllerBase
    {
        private readonly ISubjectDownloadsRepository _db;
        public SubjectDownloadsController(ISubjectDownloadsRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        //[HttpGet]
        //public async Task<IEnumerable<Constants>> GetAll()
        //{
        //    return await _db.Get();
        //}

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubject>> GetConstant(int id, string userId=null)
        {

            var userid = userId;
            if (string.IsNullOrEmpty(userid))
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    userId = claim.Value;
                }
            }

            return await _db.GetSubjectInfoBeforeDownload(id, userid);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> CreateOrUpdateDownlaodTrackingTable(string id, string userId=null)
        {
            var userid = userId;
            if (string.IsNullOrEmpty(userid))
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    userId = claim.Value;
                }
            }

            return await _db.CreatedUpdatePastPaperIsDownloaded(id, userid);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Constants>> PostTopic(Constants constants)
        //{
        //    var newConstant = await _db.Create(constants);

        //    return CreatedAtAction(nameof(GetConstant), new { id = newConstant.Id }, newConstant);
        //}

        //// DELETE: api/Examinations/5
        //[HttpDelete("{id}")]
        //public async Task DeleteConstant(int id)
        //{
        //    await _db.Delete(id);
        //}
    }
}
