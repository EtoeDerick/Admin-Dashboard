using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Videos
{
    public interface IVideosRepository
    {
        Task<IEnumerable<Video>> Get();
        Task<Video> Get(int id);
        Task<Video> Create(Video video);
        Task<ActionResult<Video>> Update(int id, Video video);
        Task<ActionResult> Delete(int id);
        Task<IEnumerable<Video>> GetVideosBySubjectId(int id);
    }

    
}
