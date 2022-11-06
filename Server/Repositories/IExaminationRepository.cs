using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories
{
    public interface IExaminationRepository
    {
        Task<IEnumerable<Examination>> Get();
        Task<Examination> Get(string id);
        Task<Examination> Create(Examination exam);
        Task<ActionResult<Examination>> Update(Examination examination);
        Task Delete(string id);
    }

    
}
