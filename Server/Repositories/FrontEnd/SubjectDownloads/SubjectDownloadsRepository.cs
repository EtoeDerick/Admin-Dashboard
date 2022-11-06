using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.SubjectDownloads
{
    public class SubjectDownloadsRepository : ControllerBase, ISubjectDownloadsRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectDownloadsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserSubject> GetSubjectInfoBeforeDownload(int subjectId, string userId)
        {
            var usersubjects = await _context.UserSubjects.Where(us => us.SubjectId == subjectId && us.AppUserId == userId).AsNoTracking()
                .Include(u => u.Subject).ThenInclude(s => s.Topics).AsNoTracking()
                .ToListAsync();

            if (usersubjects.Count == 0)
                return null;
            var usrsub = usersubjects.ElementAt(0);

            usrsub.AppUser = null;
            usrsub.Subject.Chapters = null;
            usrsub.Subject.Examination = null;
            usrsub.Subject.MCQS = null;
            usrsub.Subject.PastPapers = null;
            usrsub.Subject.Tutorials = null;
            usrsub.Subject.UserSubjects = null;
            usrsub.AppUserId = null;

            return usrsub;
        }

        public async Task<bool> CreatedUpdatePastPaperIsDownloaded(string pastpaperId, string userId)
        {
            if(_context.DownloadTrackingTables.Any(u => u.UserId == userId && u.PastPaperId == pastpaperId))
            {
                var d =  _context.DownloadTrackingTables.Single(u => u.UserId == userId && u.PastPaperId == pastpaperId);

                d.IsDownloaded = !d.IsDownloaded;

                //Update DownloadTracking record
                _context.Entry(d).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("Error: ", ex);
                }
                return false;
            }
            else
            {
                var downloadtracking = new DownloadTrackingTable()
                {
                    UserId = userId,
                     IsDownloaded = true,
                      Date = DateTime.Now,
                       PastPaperId = pastpaperId,
                       ObjectId = pastpaperId
                };

                //create downloadtracking record
                _context.DownloadTrackingTables.Add(downloadtracking);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("Error", ex.Message);
                }
            }
            
            

            return true;
        }
    }
}
