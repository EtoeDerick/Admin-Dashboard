using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Tutorials
{
    public interface ITutorialsRepository
    {
        //Task<IEnumerable<Admin.Shared.Models.Constants>> Get();
        Task<SubjectOverViewDto> Get(int id);
        Task<IEnumerable<Video>> GetVideosForGivenLessonID(int id);
        //Task<ActionResult<Admin.Shared.Models.Constants>> Update(int id, Admin.Shared.Models.Constants constants);
        //Task<ActionResult> Delete(int id);
    }

    
}
