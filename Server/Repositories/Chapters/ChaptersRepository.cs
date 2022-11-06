using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Chapters
{
    public class ChaptersRepository : ControllerBase, IChaptersRepository
    {
        private readonly ApplicationDbContext _context;

        public ChaptersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chapter>> Get()
        {
            var chapters = await _context.Chapters.ToListAsync();


            return chapters;
        }

        public async Task<Chapter> Get(int id)
        {
            return await _context.Chapters.FindAsync(id);
        }

        public async Task<ActionResult<Chapter>> Update(int id, Chapter chapter)
        {
            if (id != chapter.Id)
            {
                return BadRequest();
            }

            
            
            _context.Entry(chapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(chapter.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return chapter;
        }

        private bool ChapterExists(int id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }

        public async Task<Chapter> Create(Chapter chapter)
        {

            _context.Chapters.Add(chapter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ChapterExists(chapter.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return chapter;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var constantsToDelete = await Get(id);

            if (constantsToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.Chapters.Remove(constantsToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IEnumerable<Chapter>> GetChaptersBySubjectId(int id)
        {
            return await _context.Chapters.Where(c => c.SubjectId == id).ToListAsync();
        }
    }
}
