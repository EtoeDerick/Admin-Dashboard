using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories
{
    public interface IExaminationClientRepository
    {
        Task<IEnumerable<SubjectDto>> Get(string ExaminationId, string userId);
        Task<ActionResult<AnnouncementDto>> GetAnnouncement(bool IsActive = true);
        //Task<Examination> Create(Examination exam);
        //Task<ActionResult<Examination>> Update(Examination examination);
        //Task Delete(string id);
    }

    
}
