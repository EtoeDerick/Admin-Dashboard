using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Lessons
{
    public interface ILessonsRepository
    {
        Task<IEnumerable<Lesson>> Get();
        Task<Lesson> Get(int id);
        Task<Lesson> Create(Lesson lesson);
        Task<ActionResult<Lesson>> Update(int id, Lesson lesson);
        Task<ActionResult> Delete(int id);
    }

    
}
