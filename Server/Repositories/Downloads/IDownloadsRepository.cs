using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Downloads
{
    public interface IDownloadsRepository
    {
        Task<IEnumerable<Download>> Get();
        Task<Download> Get(int id);
        Task<Download> Create(Download download);
        Task<ActionResult<Download>> Update(int id, Download download);
        Task<ActionResult> Delete(int id);
        Task<IEnumerable<Download>> GetVideosBySubjectId(int id);
    }

    
}
