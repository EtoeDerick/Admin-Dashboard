using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Instructors
{
    public class InstructorRepository : ControllerBase, IInstructorRepository
    {
        private readonly ApplicationDbContext _context;

        public InstructorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Admin.Shared.Models.Instructor> Create(Admin.Shared.Models.Instructor instructor)
        {
            _context.Instructors.Add(instructor);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (InstructorExists(instructor.Id))
                {

                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return instructor;
        }

        public async Task<ActionResult> Delete(string id)
        {
            var instructor = await _context.Instructors.FindAsync(id);

            if(instructor == null)
            {
                return NotFound();
            }

            //Actual Deleting takes place
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IEnumerable<Admin.Shared.Models.Instructor>> Get()
        {
            //Display a list of all instructors
            return await _context.Instructors.ToListAsync();
        }

        public async Task<Admin.Shared.Models.Instructor> Get(string id)
        {
            var instructors = await _context.Instructors.Where(i => i.Id == id)
                .Include(ins => ins.InstructorSubjects).ToListAsync();

            foreach(var ins in instructors)
            {
                foreach(var i in ins.InstructorSubjects)
                {
                    i.Subject = null;
                }
            }

            // return await _context.Instructors.FindAsync(id);
            return instructors.ElementAt(0);
        }

        public async Task<ActionResult<Admin.Shared.Models.Instructor>> Update(string id, Admin.Shared.Models.Instructor instructor)
        {
            if(id != instructor.Id)
            {
                return BadRequest();
            }

            _context.Entry(instructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(instructor.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return instructor;
        }

        private bool InstructorExists(string id)
        {
            return _context.Instructors.Any(i => i.Id == id);
        }
    }
}
