using Admin.Shared.Dtos;
using Admin.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.ExamSubjects
{
    public interface IExamSubjectRepository
    {
        Task<IEnumerable<McqPastPaperFormDto>> GetAllexamSubjects();
        Task<IEnumerable<Subject>> Get(string examId);

        
    }
}
