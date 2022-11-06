using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Videos
{
    public class VideosRepository : ControllerBase, IVideosRepository
    {
        private readonly ApplicationDbContext _context;

        public VideosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Video>> Get()
        {
            var chapters = await _context.Videos.ToListAsync();


            return chapters;
        }

        public async Task<Video> Get(int id)
        {
            return await _context.Videos.FindAsync(id);
        }

        public async Task<ActionResult<Video>> Update(int id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest();
            }

            
            
            _context.Entry(video).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(video.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return video;
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }

        public async Task<Video> Create(Video video)
        {

            _context.Videos.Add(video);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (VideoExists(video.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return video;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var constantsToDelete = await Get(id);

            if (constantsToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.Videos.Remove(constantsToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IEnumerable<Video>> GetVideosBySubjectId(int id)
        {
            return await _context.Videos.Where(c => c.SubjectId == id).ToListAsync();
        }
    }
}
