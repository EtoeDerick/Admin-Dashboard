using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Constants
{
    public interface IConstantsRepository
    {
        Task<IEnumerable<Admin.Shared.Models.Constants>> Get();
        Task<Admin.Shared.Models.Constants> Get(int id);
        Task<Admin.Shared.Models.Constants> Create(Admin.Shared.Models.Constants constants);
        Task<ActionResult<Admin.Shared.Models.Constants>> Update(int id, Admin.Shared.Models.Constants constants);
        Task<ActionResult> Delete(int id);
    }

    
}
