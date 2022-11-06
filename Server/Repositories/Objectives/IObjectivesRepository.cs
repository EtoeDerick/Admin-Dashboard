using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Objectives
{
    public interface IObjectivesRepository
    {
        Task<IEnumerable<Objective>> Get();
        Task<Objective> Get(int id);
        Task<Objective> Create(Objective objective);
        Task<ActionResult<Objective>> Update(int id, Objective objective);
        Task<ActionResult> Delete(int id);
    }

    
}
