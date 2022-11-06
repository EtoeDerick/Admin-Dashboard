using Admin.Server.Repositories.ChaptersBySubjectId;
using Admin.Server.Repositories.Constants;
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
    public class ChaptersBySubjectIdController : ControllerBase
    {
        private readonly IChaptersBySubjectIddRepository _db;
        public ChaptersBySubjectIdController(IChaptersBySubjectIddRepository db)
        {
            _db = db;
        }

        //Get a list of Chapters using subjectId
        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<Chapter>> GetConstant(int id)
        {
            return await _db.GetChaptersBySubjectId(id);
        }

        
    }
}
