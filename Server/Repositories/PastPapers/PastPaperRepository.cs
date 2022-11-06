using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Subjects
{
    public class PastPaperRepository : ControllerBase, IPastPaperRepository
    {
        private readonly ApplicationDbContext _context;

        public PastPaperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PastPaper>> Get()
        {
            return await _context.PastPapers.Where(p => p.PaperNumber <= 1 || p.PaperNumber > 3).ToListAsync();
        }

        public async Task<PastPaper> Get(string id)
        {
            var pastPaper = await _context.PastPapers.FindAsync(id);

            if (pastPaper == null)
            {
                return null;
            }

            return pastPaper;
        }

        public async Task<ActionResult<PastPaper>> Update(string id, PastPaper pastPaper)
        {
            if (id != pastPaper.Id)
            {
                return BadRequest();
            }

            pastPaper.Questions = null;
            //pastPaper.Videos = null;
            
            _context.Entry(pastPaper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastPaperExists(pastPaper.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return pastPaper;
        }
        public async Task<PastPaper> Create(PastPaper pastPaper)
        {
            //pastPaper.Videos = null;
            pastPaper.Subject = null;
            
            _context.PastPapers.Add(pastPaper);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PastPaperExists(pastPaper.Id))
                {
                 
                    throw;
                }
            }

            return pastPaper;
        }

        public async Task<ActionResult> Delete(string id)
        {
            var pastPaperToDelete = await Get(id);

            if (pastPaperToDelete == null)
            {
                return NotFound();
            }
            //1. Delete all matching mcqs
            await DeleteMcqWithPastPaperId(id);

            //2. Delete the actual past paper record
            _context.PastPapers.Remove(pastPaperToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PastPaperExists(string id)
        {
            return _context.PastPapers.Any(e => e.Id == id);
        }

        public async Task<ActionResult> DeleteMcqWithPastPaperId(string pastpaperId)
        {
            var mcqsToBeDeleted = await _context.MCQs.Where(m => m.PastPaperId == pastpaperId).ToListAsync();

            if (mcqsToBeDeleted.Any())
            {
                foreach(var mcq in mcqsToBeDeleted)
                {
                    _context.MCQs.Remove(mcq);                    
                }
                await _context.SaveChangesAsync();
            }
            //var pastPaperToDelete = await Get(id);

            //if (pastPaperToDelete == null)
            //{
            //    return NotFound();
            //}
            

            return Ok();
        }

        public async Task<List<PastPaper>> GetPastPapersBySubjectId(int id)
        {
            var pastpapers = await _context.PastPapers.Where(p => p.SubjectID == id && p.IsApproved).ToListAsync();

            foreach(var pastpaper in pastpapers)
            {
                pastpaper.Subject = null;
                pastpaper.Questions = null;
                pastpaper.EssayTypeQuestions = null;
                
            }

            return pastpapers;
        }
    }
}
