using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Chapters
{
    public interface IChaptersRepository
    {
        Task<IEnumerable<Chapter>> Get();
        Task<Chapter> Get(int id);
        Task<Chapter> Create(Chapter chapter);
        Task<ActionResult<Chapter>> Update(int id, Chapter chapter);
        Task<ActionResult> Delete(int id);
        Task<IEnumerable<Chapter>> GetChaptersBySubjectId(int id);
    }

    
}
