using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.ChaptersBySubjectId
{
    public class ChaptersBySubjectIdRepository : ControllerBase, IChaptersBySubjectIddRepository
    {
        private readonly ApplicationDbContext _context;

        public ChaptersBySubjectIdRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chapter>> GetChaptersBySubjectId(int id)
        {
            return await _context.Chapters.Where(c => c.SubjectId == id).ToListAsync();
        }
    }
}
