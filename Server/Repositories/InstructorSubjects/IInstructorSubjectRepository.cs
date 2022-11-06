using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.InstructorSubjects
{
    public interface IInstructorSubjectRepository
    {
        Task<IEnumerable<InstructorSubject>> Get();
        Task<InstructorSubject> Get(int subjectId, string instructorId);
        Task<InstructorSubject> Create(InstructorSubject instructorSubject);
        Task<ActionResult<InstructorSubject>> Update(InstructorSubject instructorSubject);
        Task<ActionResult> Delete(int subjectId, string instructorId);
    }

    
}
