using Admin.Server.Repositories.LessonsByChapterId;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
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
    public class LessonsByChapterIdController : ControllerBase
    {
        private readonly ILessonsByChapterIdRepository _db;
        public LessonsByChapterIdController(ILessonsByChapterIdRepository db)
        {
            _db = db;
        }

        //Get a list of Chapters using subjectId
        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<Lesson>> GetConstant(int id)
        {
            return await _db.GetChaptersBySubjectId(id);
        }

        
    }
}
