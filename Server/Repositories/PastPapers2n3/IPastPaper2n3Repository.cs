using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories
{
    public interface IPastPaper2n3Repository
    {
        Task<IEnumerable<PastPaper>> Get();
        Task<PastPaper> Get(string id);
        Task<PastPaper> Create(PastPaper pastPaper);
        Task<ActionResult<PastPaper>> Update(string id, PastPaper pastPaper);
        Task<ActionResult> Delete(string id);
    }

    
}
