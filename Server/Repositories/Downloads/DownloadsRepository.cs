using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Downloads
{
    public class DownloadsRepository : ControllerBase, IDownloadsRepository
    {
        private readonly ApplicationDbContext _context;

        public DownloadsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Download>> Get()
        {
            var chapters = await _context.VideoDownloads.ToListAsync();


            return chapters;
        }

        public async Task<Download> Get(int id)
        {
            return await _context.VideoDownloads.FindAsync(id);
        }

        public async Task<ActionResult<Download>> Update(int id, Download download)
        {
            if (id != download.Id)
            {
                return BadRequest();
            }

            
            
            _context.Entry(download).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DownloadExists(download.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return download;
        }

        private bool DownloadExists(int id)
        {
            return _context.VideoDownloads.Any(e => e.Id == id);
        }

        public async Task<Download> Create(Download download)
        {

            _context.VideoDownloads.Add(download);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (DownloadExists(download.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return download;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var constantsToDelete = await Get(id);

            if (constantsToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.VideoDownloads.Remove(constantsToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IEnumerable<Download>> GetVideosBySubjectId(int id)
        {
            return await _context.VideoDownloads.Where(c => c.SubjectId == id).ToListAsync();
        }
    }
}
