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
            bool isSubjectFree = false;
            //var subjectStatus = string.Empty;
            var paymentStatus = string.Empty;

            //If subjectId was found, get subjectStatus: whether subject is FREE
            if (subjectId > 0)
            {
                isSubjectFree = _context.Subjects.Single(s => s.Id == subjectId).IsFree;
            }


            var p1s = new List<PaperOnePastPaperDto>();

            //Get all pastpapers using subjectId
            var pastpapers = await _context.PastPapers
                            .Where(p => p.SubjectID == subjectId && (p.PaperNumber <= 1 || p.PaperNumber >= 4) && p.IsApproved && p.IsQuiz == false)
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
                    CorrectAnsweredCount = string.IsNullOrEmpty(userId) ? 0 : await _context.UserProgressions.CountAsync(up => up.PastPaperId == p.Id && (up.PaperNumber == 1 || up.PaperNumber >= 4) && up.UserId == userId),
                    IsDownloaded = _context.DownloadTrackingTables.Any(d => d.PastPaperId == p.Id && d.IsDownloaded) ? true : false,
                    DownloadSize = p.DownloadSize,
                    Status = p.Status
                };

                pastpaper.IsNotDownloaded = !pastpaper.IsDownloaded;

                pastpaper.WrongAnswerCount = pastpaper.Quantity - pastpaper.CorrectAnsweredCount;

                var us = new List<UserSubject>();
                if (!string.IsNullOrEmpty(userId))
                {
                    us = await _context.UserSubjects.Where(u => u.SubjectId == subjectId && u.AppUserId == userId).ToListAsync();
                }
                
                //User Enrollment exist
                if (!string.IsNullOrEmpty(userId) && us.Count > 0)
                {
                    var usersubject = us.ElementAt(0);

                    //var today = DateTime.Now;
                    //var span = today.Subtract(usersubject.EnrollmentDate.AddDays(usersubject.Duration));
                    //var span = usersubject.EnrollmentDate.AddDays(usersubject.Duration) - DateTime.UtcNow;
                    if (isSubjectFree && (pastpaper.Status.ToLower() != "free" || pastpaper.Status.ToLower() != "paid"))
                    {
                        pastpaper.Status = "OBC";
                        pastpaper.StatusColor = "OrangeRed";
                    }else if (!HasExpired(usersubject.EnrollmentDate, usersubject.Duration))
                    {
                        pastpaper.Status = "Paid";
                        pastpaper.StatusColor = "#da9100";
                    }
                    else //subject use has expired
                    {                        
                        if (pastpaper.Status == "Free")
                        {
                            pastpaper.StatusColor = "LimeGreen";
                            //pastpaper.Status = "Free";
                        }
                        else if (pastpaper.Status == "Basic")
                        {
                            pastpaper.StatusColor = "DodgerBlue";
                            //pastpaper.Status = "Basic";
                        }
                        else
                        {
                            //Status = "Premium"
                            pastpaper.StatusColor = "#da9100";
                            pastpaper.Status = "Premium";
                        }
                    }
                    
                    
                }
                else //UserAccount doesn't exist
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
                        pastpaper.Status = "Premium";
                    }
                }
               
                
                

                

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

        public bool HasExpired(DateTime enrollmentDate, int durationInDays)
        {
            // Calculate the expiration date
            DateTime expirationDate = enrollmentDate.AddDays(durationInDays);

            // Check if the current date is after the expiration date
            return DateTime.Now > expirationDate;
        }

    }
}
