using Admin.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.ExamCategory
{
    public class ExamCategoryRepository : ControllerBase, IExamCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.ExamCategory> Create(Shared.Models.ExamCategory examCategory)
        {
            _context.ExamCategories.Add(examCategory);
            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateException ex)
            {
                if (_context.ExamCategories.Any(e => e.Id == examCategory.Id))
                {
                    throw;
                }
                Console.WriteLine("Error: ", ex.Message);

                return new Shared.Models.ExamCategory();
            }

            return examCategory;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var examCat = await Get(id);
            if(examCat == null)
            {
                return NotFound();
            }

            _context.ExamCategories.Remove(examCat);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IEnumerable<Shared.Models.ExamCategory>> Get()
        {
            return await _context.ExamCategories.ToListAsync();
        }

        public async Task<Shared.Models.ExamCategory> Get(int id)
        {
            var ex = await _context.ExamCategories.FindAsync(id);
            ex.Examinations = null;
            return ex;
        }

        public async Task<ActionResult<Shared.Models.ExamCategory>> Update(int id, Shared.Models.ExamCategory examCategory)
        {
            //if(!_context.ExamCategories.Any(e => e.Id == id))
            //{
            //    return null;
            //}
            //_context.ExamCategories.Add(examCategory);
            examCategory.Id = id;

            _context.Entry(examCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateException ex)
            {
                if(_context.ExamCategories.Any(e=>e.Id == examCategory.Id))
                {
                    throw;
                }
                Console.WriteLine("Error: ", ex.Message);
            }

            return examCategory;
        }
    }
}
