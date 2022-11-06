using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.FrontEnd.Tutorials;
using Admin.Shared.Dtos;
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
    public class TutorialsController: ControllerBase
    {
        private readonly ITutorialsRepository _db;
        public TutorialsController(ITutorialsRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<SubjectOverViewDto> GetTutorialsBySubjectId(int id)
        {
            return await _db.Get(id);
        }

        [HttpPut("{id}")]
        public async Task<IEnumerable<Video>> GetTutorialsByLessonId(int id)
        {
            return await _db.GetVideosForGivenLessonID(id);
        }


    }
}
