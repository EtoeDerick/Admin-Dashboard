using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories
{
    public interface IPastPaperRepository
    {
        Task<IEnumerable<PastPaper>> Get();
        Task<PastPaper> Get(string id);
        Task<PastPaper> Create(PastPaper pastPaper);
        Task<ActionResult<PastPaper>> Update(string id, PastPaper pastPaper);
        Task<ActionResult> Delete(string id);
        Task<List<PastPaper>> GetPastPapersBySubjectId(int id);
    }

    
}
