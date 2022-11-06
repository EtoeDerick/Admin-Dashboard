using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Tutorials
{
    public class TutorialsRepository : ControllerBase, ITutorialsRepository
    {
        private readonly ApplicationDbContext _context;

        public TutorialsRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<SubjectOverViewDto> Get(int id)
        {
            
            var chapters = await _context.Chapters.Where(c => c.SubjectId == id).AsNoTracking()
                .Include(c => c.ChapterObjectives).AsNoTracking()
                .Include(c => c.Lessons).ThenInclude(l => l.Videos).AsNoTracking()
                .Include(c => c.Lessons).ThenInclude(l => l.Downloads).AsNoTracking()
                .ToListAsync();

            var subject = _context.Subjects.Single(s => s.Id == id);

            var subjectov = new SubjectOverViewDto()
            {
                SubjectId = id,
                SubjectTitle = subject.Title,
                Description = subject.Description,
                VideoPreviewUrl = subject.VideoPreviewUrl,
                IsVideoUrlPresent = string.IsNullOrEmpty(subject.VideoPreviewUrl)? false : true,
                EnrollmentCount = await _context.UserSubjects.CountAsync(u => u.SubjectId == id),
                Chapters = chapters
            };

            subjectov.IsVideoUrlAbsent = !subjectov.IsVideoUrlPresent;

            return subjectov;
        }

        public async Task<IEnumerable<Video>> GetVideosForGivenLessonID(int id)
        {
            var videos = await _context.Videos.Where(v => v.LessonId == id).ToListAsync();
            return videos.OrderBy(v => v.Position);
        }
    }
}
