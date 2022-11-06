using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.SubjectEnrollment
{
    public class SubjectEnrollmentRepository : ControllerBase, ISubjectEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectEnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserSubjectsDto>> GetAll(string userId) //Get all user enrollments
        {
            await SetIsExpiredStatus(userId);

            var userSubjectDtos = new List<UserSubjectsDto>();
            var userSubjects = await _context.UserSubjects.Where(us => us.AppUserId == userId && us.IsDeleted == false).Include(s => s.Subject).ToListAsync();            

            foreach(var us in userSubjects)
            {
                var subject = us.Subject;

                var userSubjectDto = await Get(us.SubjectId, userId);
                
                userSubjectDto.AverageProgress = GetAverageProgress(userSubjectDto.SubjectContentTypes);

                userSubjectDtos.Add(userSubjectDto);
            }


            return userSubjectDtos;
        }

        public async Task<UserSubjectsDto> Get(int subjectId, string userId) //Get user enrollment for a given subject
        {
            var count = 0;
            var insturctors = new List<SubjectInstructorDto>();

            await SetIsExpiredStatus(userId);

            //Get Instructors for the given subject
            var instructSubjects = await _context.InstructorSubject.Where(ins => ins.SubjectId == subjectId).Include(i => i.Instructor).ToListAsync();

            foreach (var insub in instructSubjects)
            {
                //var instructor = new SubjectInstructorDto() 
                //{
                //    Name = insub.Instructor.Name
                //};

                insturctors.Add(new SubjectInstructorDto()
                {
                    Id = count.ToString(),
                    Name = insub.Instructor.Name.ToUpper()
                });
                //if (count == 0)
                //{
                //    insturctors = insturctors + insub.Instructor.Name;
                //}
                //else
                //{
                //    insturctors = insturctors + ", " + insub.Instructor.Name;
                //}

                count++;
            }

            var userSubjects = await _context.UserSubjects.Where(us => us.IsDeleted == false && us.SubjectId == subjectId && us.AppUserId == userId).ToListAsync();
            

            if (userSubjects.Count < 1)
                return null;

            var us = userSubjects.ElementAt(0);
            var subject = _context.Subjects.Single(s => s.Id == subjectId);

            var userSubjectDto = new UserSubjectsDto()
            {
                IsExpired = us.IsExpired,
                Status = us.Status,
                SubjectId = us.SubjectId,
                AppUserId = us.AppUserId,
                SubjectTitle = subject.Title,
                MonthlyPrice = subject.MonthlyPrice,
                AnnualPrice = Decimal.ToInt32(subject.Price),
                TenMonthsPrice = subject.TenMonths,
                ImageUrl = subject.ImageUrl,
                IsNotEnroll = us.IsDeleted,
                IsPaper1ContentAvailable = subject.IsPaper1ContentAvailable,
                IsPaper2ContentAvailable = subject.IsPaper2ContentAvailable,
                IsPaper3ContentAvailable = subject.IsPaper3ContentAvailable,
                IsTutorialContentAvailable = subject.IsTutorialContentAvailable,
                Instructors = insturctors,
                SubjectContentTypes = await ConstructSubjectContentTypes(subjectId, userId)
            };
            
            //1. Ge the payment Status for this subject 
            if (_context.UserSubjects.Any(u => u.AppUserId == userId && u.SubjectId == subject.Id))
            {
                var usrsubj = _context.UserSubjects.Single(u => u.AppUserId == userId && u.SubjectId == subject.Id);


                var consumptionDuration = DateTime.Now - usrsubj.EnrollmentDate;

                if (consumptionDuration.TotalDays > us.Duration)
                {
                    userSubjectDto.PaymentStatus = "Expired";
                }
                else
                {
                    userSubjectDto.PaymentStatus = "Paid";
                }
            }
            else
            {
                if (userSubjectDto.IsNotEnroll)
                {
                    userSubjectDto.PaymentStatus = "NoAccountFound";
                }
                else
                {
                    userSubjectDto.PaymentStatus = "NotEnrolled";
                }
            }


            userSubjectDto.AverageProgress = GetAverageProgress(userSubjectDto.SubjectContentTypes);

            return userSubjectDto;
        }

        private string GetAverageProgress(List<SubjectContentType> subjectContentTypes)
        {
            var count = 0.0;
            var sum = 0.0;
            foreach(var s in subjectContentTypes)
            {
                count = sum + s.PercentageComplete;
                count = count + 1;
            }

            return (sum / count).ToString();
        }

        public async Task<bool> CreateEnrollment(int subjectId, string userid)
        {
            var us = new UserSubject()
            {
                Duration = 0,
                SubjectId = subjectId,
                AppUserId = userid
            };
            //Check for the existence of subject enrollment
            var usold = await _context.UserSubjects.Where(u => u.SubjectId == subjectId && u.AppUserId == userid).ToListAsync();

            //If an enrollment is found, update enrollment.isdeleted field
            if(usold.Count > 0)
            {
                us = usold.ElementAt(0);
                us.Subject = null;

                us.IsDeleted = false;
                _context.Entry(us).State = EntityState.Modified;
            }
            else //Create a new enrollment for the given user.
            {                
                us.Subject = null;
                _context.UserSubjects.Add(us);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (SubjectEnrollmentExists(us.SubjectId, us.AppUserId))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return true;
        }

        private bool SubjectEnrollmentExists(int subjectId, string appUserId)
        {
            return _context.UserSubjects.Any(us => us.SubjectId == subjectId && us.AppUserId == appUserId);
        }

        async Task<List<SubjectContentType>> ConstructSubjectContentTypes(int subjectid, string userId)
        {
            var contentTypes = new List<SubjectContentType>();

            int p1 = await _context.UserProgressions.CountAsync(up => up.SubjectId == subjectid && up.PaperNumber == 1 && up.UserId == userId);
            //var p2 = (double)await _context.UserProgressions.CountAsync(up => up.SubjectId == subjectid && up.PaperNumber == 2 && up.UserId == userId);
            //var p3 = (double)await _context.UserProgressions.CountAsync(up => up.SubjectId == subjectid && up.PaperNumber == 3 && up.UserId == userId);
            //var p4 = (double)await _context.UserProgressions.CountAsync(up => up.SubjectId == subjectid && up.PaperNumber == 4 && up.UserId == userId);

            int totalP1 = await _context.MCQs.CountAsync(m => m.SubjectId == subjectid);
            

            double NumberOfMcqsAnswered = p1;
            double TotoaNumberofMcqs = totalP1;



            double percent1 = Math.Round((NumberOfMcqsAnswered / TotoaNumberofMcqs) * 100, 1);

            var c1 = new SubjectContentType()
            {
                Id = 1,
                ContentTitle = "Paper 1",
                PercentageComplete = percent1,
                BoxViewPercentageWidth = ComputeBoxViewWidth(percent1)

            };

            //ToDo: P1, P2, Tutorial Statistics
            //Do Same for Paper 2, 3 and Tutorials

            contentTypes.Add(c1);

            return contentTypes;
        }

        private string ComputeBoxViewWidth(double percentage)
        {
            var totalWidth = 0.0;

            var percent = percentage / 100;

            totalWidth = percent * 300;

            return Math.Floor(totalWidth).ToString();
        }

        private bool UserExists(string id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }

        public async Task<EnrollmentSubjectDto> GetMeEnroll(int subjectId, AppUser user)
        {
            var enrolmtdto = new EnrollmentSubjectDto() { SubjectId = subjectId};

            if (UserExists(user.Id) ){

               if(!_context.UserSubjects.Any(us => us.SubjectId == subjectId && us.AppUserId == user.Id )) {
                    var result = await CreateEnrollment(subjectId, user.Id);
                    enrolmtdto.IsSuccessful = result;
                    if (result)
                    {
                        enrolmtdto.Message = "Successful Enrollment!";
                    }
                }
                else
                {
                    //enrolmtdto.Message = "Enrollment for this subject already exist!";
                    //USER PROBABLY UNENROLLED FROM SUBJECT LIST
                    if(_context.UserSubjects.Any(us => us.IsDeleted)){
                        
                        //ENROLL THE USER AGAIN
                        var us = await _context.UserSubjects.Where(u => u.SubjectId == subjectId && u.AppUserId == user.Id).ToListAsync();
                        var usrSubject = us.ElementAt(0);

                        usrSubject.IsDeleted = false;
                        usrSubject.Status = "RENROLLED";

                        enrolmtdto.IsSuccessful = true;
                        enrolmtdto.Message = "Re-Enrollment Successful!";

                        await UpdateEnrollment(usrSubject, enrolmtdto);
                    }else
                    {
                        enrolmtdto.IsSuccessful = false;
                        enrolmtdto.Message = "Enrollment is active and OK";
                    }                 
                                     
                }


            }else
            {
                //1. Create new User
                //2. Create new record
                var credit = 0;
                //1. Create new User
                try
                {
                    Int32.TryParse(_context.Constants.Single(c => c.Key == "credit").Value, out credit);
                }
                catch (Exception)
                {
                    credit = 0;
                }

                user.Credit = credit;

                await CreateUser(user);
                var response = await CreateEnrollment(subjectId, user.Id);

                if (response)
                {
                    enrolmtdto.IsSuccessful = true;
                    enrolmtdto.Message = "New User Recorded & Successful Enrollment!";
                }

            }


            return enrolmtdto;
        }

        public async Task<AppUser> CreateUser(AppUser user)
        {
            _context.AppUsers.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (UserExists(user.Id))
                {
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return user;
        }

        public async Task<ActionResult<IEnumerable<UserSubjectsDto>>> Delete(int id, string userId)
        {
            if (_context.UserSubjects.Any(u => u.SubjectId == id && u.AppUserId == userId))
            {
                var us = _context.UserSubjects.Single(u => u.SubjectId == id && u.AppUserId == userId);
                

                us.IsDeleted = true;
                us.Status = "UnEnrolled";

                await UpdateEnrollment(us);

            }
            else
            {
                return NotFound();
            }

            return Ok(await GetAll(userId) );
        }

        private async Task<EnrollmentSubjectDto> UpdateEnrollment(UserSubject userSubject, EnrollmentSubjectDto enrolmtdto = null)
        {
            _context.Entry(userSubject).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error", ex.Message);
                if(enrolmtdto != null)
                {
                    enrolmtdto.IsSuccessful = false;
                    enrolmtdto.Message = "Re-Enrollment Falled!";
                    return enrolmtdto;
                }
                
            }

            return enrolmtdto;
        }

        private async Task SetIsExpiredStatus(string userId)
        {
            var us = await _context.UserSubjects.Where(u => u.AppUserId == userId).ToListAsync();
            foreach(var u in us)
            {
                var span = DateTime.Now - u.EnrollmentDate.AddDays(u.Duration);
                if(span.TotalSeconds < 1)
                {
                    //Subject has expired
                    u.IsExpired = true;
                    await UpdateEnrollment(u);
                }
            }
        }
    }
}
