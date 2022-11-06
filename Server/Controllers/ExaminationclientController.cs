using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System.Security.Claims;
using Admin.Shared.Dtos;
using System.Diagnostics;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ExaminationclientController : ControllerBase
    {
        IExaminationClientRepository _exam;
        //private readonly ILogger<ExaminationsController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API

        
        public ExaminationclientController(IExaminationClientRepository exam)
        {
            _exam = exam;
        }

        [HttpGet]
        public async Task<ActionResult<AnnouncementDto>> GetAnnouncement()
        {
            return await _exam.GetAnnouncement();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<SubjectDto>> GetSubjectDtosForGivenExamination(string id)
        {

            string userId = string.Empty;
            if(HttpContext != null && HttpContext.User != null)
            {
                try
                {
                    var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                    if (claim != null)
                    {
                        userId = claim.Value;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                
            }
            
            var subjectDtos = await _exam.Get(id, userId);

            return subjectDtos;
        }



        // GET: api/Examinations/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Examination>> GetExamination(string id)
        //{
        //    return await _exam.Get(id);
        //}

        //// PUT: api/Examinations/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IEnumerable<SubjectDto>> PutExamination(string id)
        //{
        //    var subjectDtos= await _exam.Get(id);
            
        //    return subjectDtos;
        //}

        //// PUT: api/Examinations/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut]
        //public async Task<ActionResult<Examination>> PutExam(Examination examination)
        //{

        //    return await _exam.Update(examination);
        //}

        //// POST: api/Examinations
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Examination>> PostExamination(Examination examination)
        //{
        //    var newExamination = await _exam.Create(examination);

        //    return CreatedAtAction(nameof(GetExaminations), new { id = newExamination.Id }, newExamination);
        //}

        //// DELETE: api/Examinations/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteExamination(string id)
        //{
        //    var examToDelete = await  _exam.Get(id);

        //    if (examToDelete == null)
        //    {
        //        return NotFound();
        //    }

        //    await _exam.Delete(examToDelete.Id);

        //    return Ok();
        //}

    }
}
