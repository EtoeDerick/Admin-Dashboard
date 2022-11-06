using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.UserSubjects
{
    public class UserSubjectRepository : ControllerBase, IUserSubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public UserSubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserSubject>> Get()
        {
            return await _context.UserSubjects.ToListAsync();
        }

        public async Task<UserSubject> Get(int id)
        {
            var userSubjects = await _context.UserSubjects.FindAsync(id);

            if (userSubjects == null)
            {
                return null;
            }

            return userSubjects;
        }

        public async Task<ActionResult<UserSubject>> Update(int id, UserSubject userSubject, string userId)
        {
            //subject.Examination = null;
            

            _context.Entry(userSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(userSubject.SubjectId, userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return userSubject;
        }
        public async Task<UserSubject> Create(UserSubject userSubject, string userId)
        {
            
            _context.UserSubjects.Add(userSubject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubjectExists(userSubject.SubjectId, userId))
                {
                 
                    throw;
                }
            }

            return userSubject;
        }

        public async Task Delete(int id)
        {
            var subjectToDelete = await _context.Subjects.FindAsync(id);
            _context.Subjects.Remove(subjectToDelete);
            await _context.SaveChangesAsync();
        }

        private bool SubjectExists(int id, string userid)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
