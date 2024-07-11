using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.FrontEnd.McqReports;
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
    public class MobileMcqReportsController : ControllerBase
    {
        private readonly IMcqReportsRepository _db;
        public MobileMcqReportsController(IMcqReportsRepository db)
        {
            _db = db;
        }


        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<ActionResult<bool>> PostReport(int id, string pastPaperId, int subjectId, string report, int questionPosition, string userId=null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    userId = claim.Value;
                }
            }
            var objectId = HttpContext.User.Claims;

            var mcqReport = new MCQReport()
            {
                McqId = id,
                PastPaperId = pastPaperId,
                SubjectId = subjectId,
                Report = report,
                QuestionPosition = questionPosition, 
                UserId = userId,
                ReportDate = DateTime.Now,
                IsReported = true
            };

            return await _db.Create(mcqReport);
        }

    }
}
