using Admin.Server.Repositories.FrontEnd.UserQuiz;
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
    public class UserQuizController: ControllerBase
    {
        private readonly IUserQuizRepository _db;
        static readonly string[] scopeRequiredByApi = new string[] { "Api.Read", "Api.ReadWrite" };
        public UserQuizController(IUserQuizRepository db)
        {
            _db = db;
        }
        
        [HttpGet("{pastpaperId}")]
        public async Task PostUserQuiz(string pastpaperId, int score, string userId=null)
        {
            var userid = userId;
            var username = string.Empty;

            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var claimName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name");

            if (claim != null)
            {
                var claimType = claim.Type;

                if (string.IsNullOrEmpty(userId))
                {
                    userid = claim.Value;
                }
            }

            if (claimName != null)
            {
                var claimNameType = claim.Type;

                username = claimName.Value;
            }

            await _db.Create(pastpaperId, userid, username, score);
        }
    }
}
