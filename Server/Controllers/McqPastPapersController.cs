using Admin.Server.Repositories.FrontEnd.McqPastpaper;
using Admin.Shared.Dtos;
using Admin.Shared.Dtos.mcq;
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
    public class McqPastPapersController : ControllerBase
    {
        private IMcqPastpersRepository _db;
        static readonly string[] scopeRequiredByApi = new string[] { "Api.Read", "Api.ReadWrite" };
        public McqPastPapersController(IMcqPastpersRepository db)
        {
            _db = db;
        }
        //Get mcqs by pastpaperId
        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<MCQDto>> GetMcqsUsingPastpaperID(string id)
        {
            var userid = string.Empty;
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
                //userid = "c5c7c95b-227a-4b9f-9c7d-d13446f52a49";
            }

            return await _db.Get(id, userid);
        }

        //get mcqs by topicId
        [HttpDelete("{id}")]
        public async Task<IEnumerable<MCQDto>> GetMcqsUsingTopicID(int id)
        {
            var userid = string.Empty;
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
            }
            return await _db.GetMcqsByTopics(id, userid);
        }

        [HttpPut("{id}")]
        public async Task<IEnumerable<TopicsDto>> GetTopicDtosForGivenSubjectID(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var userid = string.Empty;
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
            }

            return await _db.GetTopicDtosForGivenSubjectID(id, userid);
        }
    }
}
