using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.InstructorSubjects
{
    public class InstructorSubjectRepository : ControllerBase, IInstructorSubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public InstructorSubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstructorSubject>> Get()
        {
            var instructorSubjects = await _context.InstructorSubject.ToListAsync();

            foreach(var ins in instructorSubjects)
            {
                ins.Instructor = null;
                ins.Subject = null;
                
            }

            return instructorSubjects;
        }

        public async Task<InstructorSubject> Get(int subjectId, string instructorId)
        {
            var instructSubj =  _context.InstructorSubject.Single(ins => ins.SubjectId == subjectId && ins.InstructorId == instructorId);
            instructSubj.Instructor = null;
            instructSubj.Subject = null;

            return instructSubj;
        }

        public async Task<ActionResult<InstructorSubject>> Update(InstructorSubject instructorSubject)
        {
            
            if (!InstructorSubjectExists(instructorSubject.SubjectId, instructorSubject.InstructorId))
            {
                return BadRequest();
            }

            
            
            _context.Entry(instructorSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorSubjectExists(instructorSubject.SubjectId, instructorSubject.InstructorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return instructorSubject;
        }
        public async Task<InstructorSubject> Create(InstructorSubject instructorSubject)
        {
            instructorSubject.Subject = null;
            instructorSubject.Instructor = null;

            _context.InstructorSubject.Add(instructorSubject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (InstructorSubjectExists(instructorSubject.SubjectId, instructorSubject.InstructorId))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return instructorSubject;
        }

        public async Task<ActionResult> Delete(int subjectId, string instructorId)
        {
            var instructorSubjectToDelete = await Get(subjectId, instructorId);

            instructorSubjectToDelete.Subject = null;
            instructorSubjectToDelete.Instructor = null;

            if (instructorSubjectToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.InstructorSubject.Remove(instructorSubjectToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool InstructorSubjectExists(int subjectId, string instructorId)
        {
            return _context.InstructorSubject.Any(e => e.SubjectId == subjectId && e.InstructorId == instructorId);
        }

        
    }
}
