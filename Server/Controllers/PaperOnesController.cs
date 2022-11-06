using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.FrontEnd.PaperOne;
using Admin.Shared.Dtos;
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
    public class PaperOnesController : ControllerBase
    {
        private readonly IPaperOnesRepository _db;
        static readonly string[] scopeRequiredByApi = new string[] { "Api.Read"};
        public PaperOnesController(IPaperOnesRepository db)
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
        public async Task<IEnumerable<PaperOnePastPaperDto>> GetPapersForSubjectWithID(int id)
        {
            var userId = string.Empty;
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userId = claim.Value;
            }
            return await _db.Get(id, userId);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<ActionResult<Constants>> Puts(int id, Constants constants)
        //{
        //    return await _db.Update(id, constants);
        //}

        //// POST: api/Examinations
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
