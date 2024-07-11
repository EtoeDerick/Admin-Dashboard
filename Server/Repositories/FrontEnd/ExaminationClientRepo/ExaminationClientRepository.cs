using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories
{
    public class ExaminationClientRepository : ControllerBase, IExaminationClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ExaminationClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubjectDto>> Get(string ExaminationId, string userId)
        {
            var count = 0;
            var subjectDtos = new List<SubjectDto>();
            var subjects = await _context.Subjects.Where(s => s.ExaminationId == ExaminationId).ToListAsync();

            bool isSubjectFree = false;
            //If subjectId was found, get subjectStatus: whether subject is FREE
            

            if (subjects.Any())
            {
                foreach(var subject in subjects)
                {
                    var insturctors = string.Empty;
                    //Get Instructors for the given subject
                    var instructSubjects = await _context.InstructorSubject.Where(ins => ins.SubjectId == subject.Id).Include(i => i.Instructor).ToListAsync();
                    foreach(var insub in instructSubjects)
                    {
                        if(count == 0)
                        {
                            insturctors = insturctors + insub.Instructor.Name;
                        }
                        else
                        {
                            insturctors = insturctors + ", " + insub.Instructor.Name;
                        }                        
                        count++;
                    }                    
                    //Performing mapping between Subject and SubjectDto 
                    var subjectDto = new SubjectDto()
                    {
                        ExaminationId = subject.ExaminationId,
                        SubjectId = subject.Id,
                        SubjectTitle = subject.Title,
                        MonthlyPrice = subject.MonthlyPrice,
                        AnnualPrice = Decimal.ToInt32(subject.Price),
                        Description = subject.Description,
                        ImageUrl = subject.ImageUrl,
                        BackgroundImageUrl = subject.MarqueeImageUrl,
                        AppUserId = userId,
                        Ratings = "4.3", //Review this later
                        Category = subject.Category,
                        VideoPreviewUrl = subject.VideoPreviewUrl,
                        IsPaper1ContentAvailable = subject.IsPaper1ContentAvailable,
                        IsPaper2ContentAvailable = subject.IsPaper2ContentAvailable,
                        IsPaper3ContentAvailable = subject.IsPaper3ContentAvailable,
                        IsTutorialContentAvailable = subject.IsTutorialContentAvailable,
                        Instructors = insturctors,
                        IsNotEnroll = !(await _context.UserSubjects.AnyAsync(u => u.SubjectId == subject.Id && u.AppUserId == userId && u.IsDeleted == false))
                    };

                    
                    //1. Ge the payment Status for this subject 
                    if (_context.UserSubjects.Any(u => u.AppUserId == userId && u.SubjectId == subject.Id))
                    {
                        var us = _context.UserSubjects.Single(u => u.AppUserId == userId && u.SubjectId == subject.Id);
                        

                        var consumptionDuration = DateTime.UtcNow - us.EnrollmentDate;

                       if ( consumptionDuration.TotalDays > us.Duration  )
                       {
                            subjectDto.PaymentStatus = "Expired";
                        }
                        else
                        {
                            subjectDto.PaymentStatus = "Paid";
                        }
                    }
                    else
                    {
                        if (subjectDto.IsNotEnroll)
                        {
                            subjectDto.PaymentStatus = "NoAccountFound";
                        }
                        else
                        {
                            subjectDto.PaymentStatus = "NotEnrolled";
                        }
                    }
                    if (subject.Id > 0)
                    {
                        isSubjectFree = _context.Subjects.Single(s => s.Id == subject.Id).IsFree;
                    }

                    if (isSubjectFree)
                    {
                        subjectDto.PaymentStatus = "OBC";
                    }

                    subjectDtos.Add(subjectDto);
                }
            }
            else
            {
                NotFound();
            }

            return subjectDtos;
        }

        

        public async Task<ActionResult<AnnouncementDto>> GetAnnouncement(bool IsActive = true)
        {
            var announcement = new AnnouncementDto();
            var announcements = await _context.Announcements.Where(a => a.IsActive == true).ToListAsync();
            if (announcements.Any())
            {
                announcement.NumberOfDaysToExamination = announcements.ElementAt(0).NumberOfDaysToExamination;
                announcement.ExaminationId = announcements.ElementAt(0).ExaminationId;
                announcement.ExaminationTitle = announcements.ElementAt(0).ExaminationTitle;
                announcement.ExamDaysLeftBgColor = announcements.ElementAt(0).ExamDaysLeftBgColor;
                announcement.AnnouncementTitle = announcements.ElementAt(0).AnnouncementTitle;
                announcement.AnnouncementDescription = announcements.ElementAt(0).AnnouncementDescription;
                announcement.Label1Sub1 = announcements.ElementAt(0).Label1Sub1;
                announcement.Label1Sub2 = announcements.ElementAt(0).Label1Sub2;
                announcement.Label1Sub3 = announcements.ElementAt(0).Label1Sub3;
                announcement.Label2Sub1 = announcements.ElementAt(0).Label2Sub1;
                announcement.Label2Sub2 = announcements.ElementAt(0).Label2Sub2;
                announcement.Label2Sub3 = announcements.ElementAt(0).Label2Sub3;
                announcement.HowToUseOgaBookVideoUrl = announcements.ElementAt(0).HowToUseOgaBookVideoUrl;
                announcement.VideoTitle = announcements.ElementAt(0).VideoTitle;
                announcement.EmailContact = announcements.ElementAt(0).EmailContact;
                announcement.Line1ContactWithWhatsApp = announcements.ElementAt(0).Line1ContactWithWhatsApp;
                announcement.Line2Contact = announcements.ElementAt(0).Line2Contact;
                announcement.UpdateFeatures = announcements.ElementAt(0).UpdateFeatures;

                var writtenDate = _context.Examinations.Single(e => e.Id == announcements.ElementAt(0).ExaminationId.ToString()).WrittenOn;
                //var writtenDate = new DateTime(2022, 7, 20);
                var durationLength = Math.Abs( (int)(writtenDate - DateTime.Now).TotalDays);

                //if(durationLength < 0) //Replaced with Math.Abs();
                //{
                //    durationLength = durationLength * -1;
                //}

                announcement.NumberOfDaysToExamination = durationLength;

                //From Examination Controller
                announcement.StartDate = writtenDate;

                //From Announcement Controller
                announcement.EndDate = announcements.ElementAt(0).Date;
            }
            else
            {
                return NotFound();
            }
            return announcement;
        }

    }
}
