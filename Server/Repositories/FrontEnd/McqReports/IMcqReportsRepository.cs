using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.McqReports
{
    public interface IMcqReportsRepository
    {
        Task<bool> Create(MCQReport report);
    }

    
}
