using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.SubjectDownloads
{
    public interface ISubjectDownloadsRepository
    {
        //Task<IEnumerable<Admin.Shared.Models.Constants>> Get();
        Task<UserSubject> GetSubjectInfoBeforeDownload(int subjectId, string userId);
        Task<bool> CreatedUpdatePastPaperIsDownloaded(string pastpaperId, string userId);
        //Task<ActionResult<Admin.Shared.Models.Constants>> Update(int id, Admin.Shared.Models.Constants constants);
        //Task<ActionResult> Delete(int id);
    }

    
}
