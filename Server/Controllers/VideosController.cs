using Admin.Server.Repositories.Chapters;
using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.Videos;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class VideosController : ControllerBase
    {
        private readonly IVideosRepository _db;
        public VideosController(IVideosRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Video>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetConstant(int id)
        {
            return await _db.Get(id);

        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Video>> Puts(int id, Video video)
        {
            return await _db.Update(id, video);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Video>> Post(Video video)
        {
            var newConstant = await _db.Create(video);

            return CreatedAtAction(nameof(GetConstant), new { id = newConstant.Id }, newConstant);
        }
        [HttpPost("{id}")]
        public async Task<IEnumerable<Video>> Post(int id)
        {
            return await _db.GetVideosBySubjectId(id);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeleteConstant(int id)
        {
            await _db.Delete(id);
        }
    }
}
