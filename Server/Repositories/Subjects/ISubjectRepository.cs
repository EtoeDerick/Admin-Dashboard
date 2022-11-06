using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> Get();
        Task<Subject> Get(int id);
        Task<Subject> Create(Subject subject);
        Task<ActionResult<Subject>> Update(int id, Subject subject);
        Task Delete(int id);
    }

    
}
