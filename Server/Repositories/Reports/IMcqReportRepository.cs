using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Reports
{
    public interface IMcqReportRepository
    {
        Task<IEnumerable<MCQReport>> Get();
        Task<MCQReport> Get(int id);
        Task<MCQReport> Create(MCQReport userreport);
        Task<ActionResult<MCQReport>> Update(int id, MCQReport userreport);
        Task<ActionResult> Delete(int id);
    }

    
}
