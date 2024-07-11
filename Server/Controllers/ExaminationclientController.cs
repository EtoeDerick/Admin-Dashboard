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

        //id: examinationId
        [HttpGet("{id}")]
        public async Task<IEnumerable<SubjectDto>> GetSubjectDtosForGivenExamination(string id, string userId=null)
        {
            string _userId = userId;
            if(string.IsNullOrEmpty(userId))
            {
                try
                {
                    var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                    if (claim != null)
                    {
                        _userId = claim.Value;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }                
            }            
            var subjectDtos = await _exam.Get(id, _userId);
            return subjectDtos;
        }

    }
}
