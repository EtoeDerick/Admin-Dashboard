using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.PaperOne
{
    public class PaperOnesRepository : ControllerBase, IPaperOnesRepository
    {
        private readonly ApplicationDbContext _context;

        public PaperOnesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Admin.Shared.Models.Constants>> Get()
        //{
        //    var constants = await _context.Constants.ToListAsync();


        //    return constants;
        //}

        public async Task<IEnumerable<PaperOnePastPaperDto>> Get(int subjectId, string userId)
        {
            var p1s = new List<PaperOnePastPaperDto>();
            
            //ToDo: Check UserSubject Validity.

            var pastpapers = await _context.PastPapers
                            .Where(p => p.SubjectID == subjectId && (p.PaperNumber <= 1 || p.PaperNumber >= 4) && p.IsApproved)
                            .OrderBy(x => x.PaperYear).ThenBy(y => y.PaperNumber).ToListAsync();

            foreach(var p in pastpapers)
            {
                var pastpaper = new PaperOnePastPaperDto()
                {
                    Id = p.Id,
                    SubjectId = p.SubjectID,
                    SubjectTitle = _context.Subjects.Single(s => s.Id == p.SubjectID).Title,
                    Year = p.PaperYear,
                    //Quantity = p.Quantity,
                    Quantity = await _context.MCQs.CountAsync(m => m.PastPaperId == p.Id),
                    Title = p.Title,
                    CorrectAnsweredCount = !string.IsNullOrEmpty(userId) ? await _context.UserProgressions.CountAsync(up => up.PastPaperId == p.Id && (up.PaperNumber == 1 || up.PaperNumber >= 4) && up.UserId == userId) : 0,
                    IsDownloaded = _context.DownloadTrackingTables.Any( d => d.PastPaperId == p.Id && d.IsDownloaded) ?  true : false,
                    DownloadSize = p.DownloadSize,
                    Status = p.Status
                };
                pastpaper.IsNotDownloaded = !pastpaper.IsDownloaded;




                pastpaper.WrongAnswerCount = pastpaper.Quantity - pastpaper.WrongAnswerCount;

                var us = new List<UserSubject>();
                if (!string.IsNullOrEmpty(userId))
                {
                    us = await _context.UserSubjects.Where(u => u.SubjectId == subjectId && u.AppUserId == userId).ToListAsync();
                }

                if (!string.IsNullOrEmpty(userId) && us.Count > 0)
                {
                    var usersubject = us.ElementAt(0);
                    var span = usersubject.EnrollmentDate.AddDays(usersubject.Duration) - DateTime.Now;

                    if (span.TotalSeconds < 1)
                    {
                        if (pastpaper.Status == "Free")
                        {
                            pastpaper.StatusColor = "LimeGreen";
                        }
                        else if (pastpaper.Status == "Basic")
                        {
                            pastpaper.StatusColor = "DodgerBlue";
                        }
                        else
                        {
                            pastpaper.StatusColor = "#da9100";
                        }
                    }
                    else if (pastpaper.Status == "Free")
                    {
                        pastpaper.StatusColor = "LimeGreen";
                    }
                    else
                    {
                        pastpaper.Status = "Paid";
                        pastpaper.StatusColor = "#8fbc8f"; //Dark Green
                    }

                }
                else
                {
                    if (pastpaper.Status == "Free")
                    {
                        pastpaper.StatusColor = "LimeGreen";
                    }
                    else if (pastpaper.Status == "Basic")
                    {
                        pastpaper.StatusColor = "DodgerBlue";
                    }
                    else
                    {
                        pastpaper.StatusColor = "#da9100";
                    }
                }

                //if (pastpaper.Status == "Free")
                //{
                //    pastpaper.StatusColor = "LimeGreen";
                //}
                //else if (pastpaper.Status == "Basic")
                //{
                //    pastpaper.StatusColor = "DodgerBlue";
                //}
                //else
                //{
                //    pastpaper.StatusColor = "#da9100";
                //}


                if (pastpaper.CorrectAnsweredCount == 0)
                {
                    pastpaper.IsRed = true;
                }else if(pastpaper.Quantity == pastpaper.CorrectAnsweredCount)
                {
                    pastpaper.IsGreen = true;
                }
                else
                {
                    pastpaper.IsYellow = true;
                }

                if(pastpaper.SubjectId > 400)
                {
                    pastpaper.Month = "June";
                }
                else
                {
                    pastpaper.Month = "Sept.";
                }
                p1s.Add(pastpaper);
            }

            return p1s;
        }

        
        
    }
}
