using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.DownloadPdf
{
    public interface IDownloadPdfRepository
    {
        Task<IEnumerable<Admin.Shared.Models.Downloadpdf>> GetAll();
        Task<Admin.Shared.Models.Downloadpdf> Get(string id);
        Task<Admin.Shared.Models.Downloadpdf> Create(Admin.Shared.Models.Downloadpdf downloadpdf);
        Task<ActionResult<Admin.Shared.Models.Downloadpdf>> Update(string id, Admin.Shared.Models.Downloadpdf downloadpdf);
        Task<ActionResult> Delete(string id);
    }
}
