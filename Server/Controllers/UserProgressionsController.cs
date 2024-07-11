using Admin.Server.Repositories.FrontEnd.UserProgression;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserProgressionsController : ControllerBase
    {
        IUserProgressionRepository _db;
        static readonly string[] scopeRequiredByApi = new string[] { "Api.Read", "Api.ReadWrite" };
        public UserProgressionsController(IUserProgressionRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Admin.Shared.Models.UserProgression>> GetUserProgressions()
        {
            return await _db.GetUserProgressions();
        }

        //Create a record for submitted Quiz
        [HttpGet("{id}")]
        public async Task postSolutionfromQuiz(int id, string pastpaperId, int pastpaperNumber, int topicNumber, int questionPosition, string paperYear, int answerStatus = 3, string userId=null)
        {
            var userid = userId;

            if (string.IsNullOrEmpty(userid))
            {
                HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    userid = claim.Value;
                }
            }

            await _db.Get(id, pastpaperId, pastpaperNumber, topicNumber, questionPosition, paperYear, userid, answerStatus);

        }
        [HttpPost("{pastpaperId}")]
        public async Task<QuizRankDto> postSolutionfromQuiz(string pastpaperId, int score, [FromBody]string content, string userId=null)
        {
            var userid = userId;

            if (string.IsNullOrEmpty(userId))
            {
                HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    userid = claim.Value;
                }
            }
            //userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";

            var resp = await _db.GetQuizResponse(pastpaperId, score, userid, content);

            return resp;
        }
        [HttpDelete("{pastPaperId}")]
        public async Task<List<UserProgression>> GetUserProgressions(string pastpaperId)
        {
            return await _db.GetQuizSubmitted(pastpaperId);
        }
    }
}
