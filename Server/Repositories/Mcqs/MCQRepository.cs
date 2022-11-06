using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Mcqs
{
    public class MCQRepository : ControllerBase, IMCQRepository
    {
        private readonly ApplicationDbContext _context;

        public MCQRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MCQ>> Get()
        {
            return await _context.MCQs.ToListAsync();
        }

        public async Task<IEnumerable<MCQ>> GetMCQByPastPaperId(string id)
        {
            var mcqs = await _context.MCQs.Where(m => m.PastPaperId == id).ToListAsync();

            if (mcqs == null)
            {
                return null;
            }

            return mcqs;
        }

        public async Task<MCQ> Get(int id)
        {
            var mcq = await _context.MCQs.FindAsync(id);

            if (mcq == null)
            {
                return null;
            }

            return mcq;
        }

        public async Task<ActionResult<MCQ>> Update(int id, MCQ mcq)
        {
            //subject.Examination = null;
            

            _context.Entry(mcq).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!McqExists(mcq.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return mcq;
        }
        public async Task<ActionResult<MCQ>> Create(MCQ mcq)
        {
            mcq.PastPaper = null;
            
            
            _context.MCQs.Add(mcq);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (McqExists(mcq.Id))
                {
                 
                    throw;
                }
            }

            return mcq;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var mcqToDelete = await Get(id);

            if (mcqToDelete == null)
            {
                return NotFound();
            }
            _context.MCQs.Remove(mcqToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool McqExists(int id)
        {
            return _context.MCQs.Any(e => e.Id == id);
        }
    }
}
