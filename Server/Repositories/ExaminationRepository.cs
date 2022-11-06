using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories
{
    public class ExaminationtRepository : ControllerBase, IExaminationRepository
    {
        private readonly ApplicationDbContext _context;

        public ExaminationtRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Examination>> Get()
        {
            return await _context.Examinations.ToListAsync();
        }

        public async Task<Examination> Get(string id)
        {
            var examination = await _context.Examinations.FindAsync(id);

            if (examination == null)
            {
                return null;
            }

            return examination;
        }

        public async Task<ActionResult<Examination>> Update(Examination examination)
        {
            _context.Entry(examination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExaminationExists(examination.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return examination;
        }
        public async Task<Examination> Create(Examination examination)
        {
            _context.Examinations.Add(examination);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExaminationExists(examination.Id))
                {
                 
                    throw;
                }
            }

            return examination;
        }

        public async Task Delete(string id)
        {
            var examToDelete = await _context.Examinations.FindAsync(id);
            _context.Examinations.Remove(examToDelete);
            await _context.SaveChangesAsync();
        }

        private bool ExaminationExists(string id)
        {
            return _context.Examinations.Any(e => e.Id == id);
        }
    }
}
