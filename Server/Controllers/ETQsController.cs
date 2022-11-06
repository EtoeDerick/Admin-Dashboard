using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.FrontEnd.ETQ;
using Admin.Server.Repositories.FrontEnd.PaperOne;
using Admin.Shared.Dtos;
using Admin.Shared.Models.ETQ;
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
    public class ETQsController : ControllerBase
    {
        private readonly IETQRepository _db;
        static readonly string[] scopeRequiredByApi = new string[] { "Api.Read"};
        public ETQsController(IETQRepository db)
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
        public async Task<EssayPaperNTopicsDto> GetPapersForSubjectWithID(int id)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<EssayTypeQuestion>>> Puts(string id)
        {
            return await _db.GetEssayTypeQuestionCollection(id);
        }

        //// POST: api/Examinations
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IEnumerable<EssayTypeQuestion>> GetETQByTopicNumberAndPaperNumber(int id, int paperNumber)
        {
            return  await _db.GetETQByTopicNumberAndPaperNumber(id, paperNumber);
        }

        //// DELETE: api/Examinations/5
        //[HttpDelete("{id}")]
        //public async Task DeleteConstant(int id)
        //{
        //    await _db.Delete(id);
        //}
    }
}
