using Admin.Server.Repositories.FrontEnd.Pastpaperquizawards;
using Admin.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PastQuizAwardsController : ControllerBase
    {
        private readonly IPastpaperquizawardsRepository _db;
        public PastQuizAwardsController(IPastpaperquizawardsRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin.Shared.Models.QuizAwardDto>>> GetAll()
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _db.GetAllQuizesAwarded();
        }

        //GET the USERID of the quiz with pastpaperId
        [HttpGet("{pastpaperId}")]
        public async Task<ActionResult<QuizResultDto>> GetAll(string pastpaperId)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _db.GetWinnerId(pastpaperId);
        }

        [HttpPut("{pastpaperId}")]
        public async Task<ActionResult<PriceAwardPageDto>> GetWinnerPageInfo(string pastpaperId, string userid)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _db.GetWinnerInfo(pastpaperId, userid);
        }

    }
}
