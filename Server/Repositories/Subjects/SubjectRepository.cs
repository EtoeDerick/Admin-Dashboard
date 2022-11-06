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
    public class SubjectRepository : ControllerBase, ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> Get()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> Get(int id)
        {
            var subjects = await _context.Subjects.FindAsync(id);

            if (subjects == null)
            {
                return null;
            }

            return subjects;
        }

        public async Task<ActionResult<Subject>> Update(int id, Subject subject)
        {
            //subject.Examination = null;
            subject.InstructorSubjects = null;
            subject.MCQS = null;
            subject.PastPapers = null;
            subject.Topics = null;
            subject.Tutorials = null;
            subject.UserSubjects = null;
            subject.Examination.Id = subject.ExaminationId;
            subject.Examination.Subjects = null;

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(subject.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return subject;
        }
        public async Task<Subject> Create(Subject subject)
        {
            subject.Examination = null;
            _context.Subjects.Add(subject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubjectExists(subject.Id))
                {
                 
                    throw;
                }
            }

            return subject;
        }

        public async Task Delete(int id)
        {
            var subjectToDelete = await _context.Subjects.FindAsync(id);
            _context.Subjects.Remove(subjectToDelete);
            await _context.SaveChangesAsync();
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
