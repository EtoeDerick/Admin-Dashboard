using Admin.Server.Repositories.Olympiads;
using Admin.Shared.Dtos;
using Admin.Shared.Dtos.mcq;
using Admin.Shared.Models;
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
    public class OlympiadsController  : ControllerBase
    {
        private readonly IOlympiadRepository _db;
        static readonly string[] scopeRequiredByApi = new string[] { "Api.Read" };

        public OlympiadsController(IOlympiadRepository db)
        {
            _db = db;
        }

        [HttpGet("{quizcode}")]
        public async Task<IEnumerable<MCQDto>> GetMCQs(string quizcode)
        {
            var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
            }
            return await _db.GetOlympiadMCQByQuizCode(quizcode, userid);
        }

        [HttpGet()]
        public async Task<QuizStatusDto> GetQuizState(string quizcode)
        {
            var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
            }
            return await _db.GetQuizStatus(quizcode, userid);
        }

        [HttpPut()]
        public async Task<List<AllOlympiadsDto>> GetAllPastPublicOlympiads()
        {
            var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
            }
            /*else
            {
                userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";
            }*/
            
            return await _db.GetAllPublicQuizes(userid);
        }


        //Used to display the statistics for all user
        [HttpPut("{pastpaperId}")]
        public async Task<List<QuizResultDto>> GetQuizStatisWithId(string pastpaperId)
        {
            var userid = string.Empty;

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

            return await _db.GetQuizResultByPaperId(pastpaperId);
        }


        [HttpPost()]
        public async Task<List<PendingOlympiadsDto>> GetPendingPublicOlympiads()
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
                //userid = "f5bd6f4d-4622-4f74-8a4f-b31ed008c572";
            }

            return await _db.GetPendingPublicQuizes(userid);
        }
    }
}
