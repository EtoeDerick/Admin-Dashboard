using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Lessons
{
    public class LessonsRepository : ControllerBase, ILessonsRepository
    {
        private readonly ApplicationDbContext _context;

        public LessonsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> Get()
        {
            var lessons = await _context.Lessons.ToListAsync();


            return lessons;
        }

        public async Task<Lesson> Get(int id)
        {
            return await _context.Lessons.FindAsync(id);
        }

        public async Task<ActionResult<Lesson>> Update(int id, Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return BadRequest();
            }

            
            
            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConstantsExists(lesson.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return lesson;
        }
        public async Task<Lesson> Create(Lesson lesson)
        {

            _context.Lessons.Add(lesson);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ConstantsExists(lesson.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return lesson;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var constantsToDelete = await Get(id);

            if (constantsToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.Lessons.Remove(constantsToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ConstantsExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }

        
    }
}
