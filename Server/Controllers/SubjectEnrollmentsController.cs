using Admin.Server.Repositories.FrontEnd.SubjectEnrollment;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class SubjectEnrollmentsController: ControllerBase
    {
        private readonly ISubjectEnrollmentRepository _db;

        //static readonly string[] scopeRequiredByApi = new string[] { "Api.Read" };
        public SubjectEnrollmentsController(ISubjectEnrollmentRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<UserSubjectsDto>> GetAll(string userid)
        {
            //Grab userId and forward to the request
            var userId = string.Empty;

            // HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            if (string.IsNullOrEmpty(userid))
            {
                if (HttpContext != null && HttpContext.User != null)
                {
                    var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                    if (claim != null)
                    {
                        userId = claim.Value;
                    }
                }
            }
            else
            {
                userId = userid;
            }

            return await _db.GetAll(userId);
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentSubjectDto>> GetMySubjectToDashboard(int id, string userId=null)
        {
            var _userid = userId;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            try
            {
                if (string.IsNullOrEmpty(_userid))
                {
                    if (HttpContext != null && HttpContext.User != null)
                    {
                        var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                        if (claim != null)
                        {
                            _userid = claim.Value;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            
            var user = new AppUser()
            {
                Id = _userid,
                UserName = HttpContext.User.FindFirstValue("name")
            };

            return await _db.GetMeEnroll(id, user);
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<UserSubjectsDto>>> SoftDeleteMySubscription(int id, string userid="")
        {
            //Grab userId and forward to the request
            var userId = string.Empty;

            if (string.IsNullOrEmpty(userid))
            {
                // HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
                if (HttpContext != null && HttpContext.User != null)
                {
                    var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                    if (claim != null)
                    {
                        userId = claim.Value;
                    }
                }
            }
            else
            {
                userId = userid;
            }
            
            return await _db.Delete(id, userId);
        }
    }
}
