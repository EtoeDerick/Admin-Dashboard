using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.LessonsByChapterId
{
    public class LessonsByChapterIdRepository : ControllerBase, ILessonsByChapterIdRepository
    {
        private readonly ApplicationDbContext _context;

        public LessonsByChapterIdRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> GetChaptersBySubjectId(int id)
        {
            return await _context.Lessons.Where(c => c.ChapterId == id).ToListAsync();
        }
    }
}
