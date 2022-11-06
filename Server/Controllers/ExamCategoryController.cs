using Admin.Client.Pages.Backend.Examinations;
using Admin.Server.Repositories.ExamCategory;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ControllerBase
    {
        private readonly IExamCategoryRepository _db;

        public ExamCategoryController( IExamCategoryRepository  exam)
        {
            _db = exam;
        }
        [HttpGet]
        public async Task<IEnumerable<ExamCategory>> GetExamCategories()
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            //var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            //if (claim != null)
            //{
            //    var userid = claim.Value;
            //}
            //var objectId = HttpContext.User.Claims;
            //var name = HttpContext.User.FindFirstValue("name");


            return await _db.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamCategory>> GetExamCategoryById(int id)
        {
            return await _db.Get(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExamCategory>> Puts(int id, ExamCategory examCategory)
        {
            return await _db.Update(id, examCategory);
        }

        [HttpPost]
        public async Task<ActionResult<ExamCategory>> PostExaminationCategory(ExamCategory examCategory)
        {
            var newExamination = await _db.Create(examCategory);

            return CreatedAtAction(nameof(GetExamCategories), new { id = newExamination.Id }, newExamination);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _db.Delete(id);
        }
    }
}
