using Admin.Server.Repositories.FrontEnd.QuizAwards;
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
    public class QuizawardsController : ControllerBase
    {
        private readonly IQuizAwardsRepository _db;
        static readonly string[] scopeRequiredByApi = new string[] { "Api.Read", "Api.ReadWrite" };
        public QuizawardsController(IQuizAwardsRepository db)
        {
            _db = db;
        }

        //From Clients
        [HttpGet]
        public async Task<ActionResult<List<AwardComment>>> GetConversationFromQuizAward()
        {
            var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
            }
            else
            {
               // userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";
            }
            return await _db.GetAllApprovedQuizAwardsConversations();
        }
        //post a comment from quiz award
        [HttpPost("{message}")]
        public async Task<ActionResult<List<AwardComment>>> CreateConversationFromQuizAward(string message)
        {
            var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
            }
            else
            {
               // userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";
            }
            return await _db.Create(message, userid);
        }

        //from ADMIN
        [HttpPost]
        public async Task<ActionResult<Admin.Shared.Models.QuizAward>> CreateQuizAwardByAdmin(Admin.Shared.Models.QuizAward quizAward)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
           
            return await _db.CreateQuizAward(quizAward);
        }
        [HttpGet("{pastpaperId}")]
        public async Task<ActionResult<Admin.Shared.Models.QuizAward>> GetQuizAwardWithPastPaperIdByAdmin(string pastpaperId)
        {
            //var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            //var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            //if (claim != null)
            //{
            //    userid = claim.Value;
            //}
            //else
            //{
            //    userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";
            //}
            return await _db.GetQuizAwardWithPastPaperId(pastpaperId);
        }
        
        [HttpPut("{pastpaperId}")]
        public async Task<ActionResult<Admin.Shared.Models.QuizAward>> UpdateQuizAwardWithPastPaperIdByAdmin(string pastpaperId, Admin.Shared.Models.QuizAward quizAward)
        {
            //var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            //var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            //if (claim != null)
            //{
            //    userid = claim.Value;
            //}
            //else
            //{
            //    userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";
            //}
            return await _db.UpdateQuizAwardWithPastPaperId(pastpaperId, quizAward);
        }


        //Gell all past quizAwards
        [HttpPut]
        public async Task<ActionResult<List<Admin.Shared.Models.QuizAward>>> getAllQuizAwards()
        {
            //var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            //var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            //if (claim != null)
            //{
            //    userid = claim.Value;
            //}
            //else
            //{
            //    userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";
            //}
            return await _db.GetAllQuizesAwarded();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delte(int id)
        {
            //var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            //var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            //if (claim != null)
            //{
            //    userid = claim.Value;
            //}
            //else
            //{
            //    userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";
            //}
            return await _db.Delete(id);
        }
    }
}
