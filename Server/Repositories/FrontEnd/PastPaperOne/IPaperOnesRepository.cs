using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.PaperOne
{
    public interface IPaperOnesRepository
    {
        //Task<IEnumerable<Admin.Shared.Models.Constants>> Get();
        Task<IEnumerable<PaperOnePastPaperDto>> Get(int subjectId, string userId);
        //Task<Admin.Shared.Models.Constants> Create(Admin.Shared.Models.Constants constants);
        //Task<ActionResult<Admin.Shared.Models.Constants>> Update(int id, Admin.Shared.Models.Constants constants);
        //Task<ActionResult> Delete(int id);
    }

    
}
