using Admin.Server.Data;
using Admin.Server.Repositories.ExamSubjects;
using Admin.Shared.Dtos;
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
    public class ExamSubjectsController : ControllerBase
    {
        private readonly IExamSubjectRepository _db;

        public ExamSubjectsController(IExamSubjectRepository db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IEnumerable<McqPastPaperFormDto>> GetExamsSubjects()
        {
            return await _db.GetAllexamSubjects();
        }


        [HttpGet("{id}")]
        public async Task<IEnumerable<Subject>> GetSubjects(string id)
        {
            return await _db.Get(id);
        }
    }
}
