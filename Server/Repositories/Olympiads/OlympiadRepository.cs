using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Dtos.mcq;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Olympiads
{

    public class OlympiadRepository : ControllerBase, IOlympiadRepository 
    {
        private readonly ApplicationDbContext _context;

        public OlympiadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<PendingOlympiadsDto>> GetPendingPublicQuizes(string userid)
        {
            //Get all pastpapers that are: quizes, expired
            //var now = DateTime.UtcNow.AddHours(1);
            var pastpapers = await _context.PastPapers.Where(p => p.IsApproved && p.Status == "Basic" && p.IsQuiz ).ToListAsync();
            //Don't display quizes with status different from Basic

            var pendingOlympiads = new List<PendingOlympiadsDto>();

            foreach (var p in pastpapers)
            {
                var score = 0;
                if (!string.IsNullOrEmpty(userid))
                {
                    score = await _context.UserProgressions.CountAsync(u => u.PastPaperId == p.Id && u.UserId == userid && u.AnswerStatus == 1);
                }

                var participants = await _context.UserProgressions.Where(pp => pp.PastPaperId == p.Id).ToListAsync();

                var allparticipants = participants.GroupBy(x => x.UserId);

                var users = 0;
                foreach (var u in allparticipants)
                {
                    users = users + 1;
                }

                var startTime = p.WrittenDate;
                var endTime = p.WrittenDate.AddMinutes(p.DurationInMinutes);

                var t = DateTime.UtcNow.AddHours(1);
                var olym = new PendingOlympiadsDto()
                {
                    Id = p.Id,
                    OlympiadTitle = p.Title,
                    WrittenDate = p.WrittenDate,
                    DurationInMinutes = p.DurationInMinutes,
                    QuestionCount = await _context.MCQs.CountAsync(m => m.PastPaperId == p.Id),
                    //Score = score, //Get userScore for the given quiz,
                    IsAttempted = await _context.UserProgressions.AnyAsync(u => u.PastPaperId == p.Id && u.UserId == userid),
                    ParticipantsCount = users,//Count the number of users participating in the quiz
                    IsOngoing = false,
                    FirstPrice = p.Url,
                    CourseOutline = p.Thumbnail
                };
                
                if ((t - startTime).TotalMinutes > 0 && (t - endTime).TotalMinutes <= 0)
                {
                    olym.IsOngoing = true;
                    olym.QuizCode = p.QuizPassCode;
                    olym.QuizStatusDto = await GetQuizStatus(p.QuizPassCode, userid);
                    pendingOlympiads.Add(olym);
                }
                else if ( (endTime - t).TotalSeconds > 0)
                {
                    //olym.QuizStatusDto.QuizCode = p.QuizPassCode;
                    olym.QuizStatusDto = await GetQuizStatus(p.QuizPassCode, userid);
                    pendingOlympiads.Add(olym);
                }
            }
            return pendingOlympiads;
        }
        public async Task<List<AllOlympiadsDto>> GetAllPublicQuizes(string userid)
        {
            //Get all pastpapers that are: quizes, expired
            var t = DateTime.UtcNow.AddHours(1);
            var pastpapers = await _context.PastPapers.Where(p => p.IsApproved && p.Status == "Basic" && p.IsQuiz && t.CompareTo(p.WrittenDate) > 0).ToListAsync();
            //Don't display quizes with status different from Basic

            var pastOlympiads = new List<AllOlympiadsDto>();

            foreach(var p in pastpapers)
            {
                var score = await _context.UserProgressions.CountAsync(x => x.PastPaperId == p.Id && x.UserId == userid && x.AnswerStatus == 1);
                //int score = u.Count(up => up.PastPaperId == pastpaperId && /*up.UserId == u.Key &&*/ up.AnswerStatus == 1);
                var participants = await _context.UserProgressions.Where(x => x.PastPaperId == p.Id).ToListAsync();

                var allparticipants = participants.GroupBy(x => x.UserId);

                var olym = new AllOlympiadsDto()
                {
                    Id = p.Id,
                    OlympiadTitle = p.Title,
                    WrittenDate = p.WrittenDate,
                    DurationInMinutes = p.DurationInMinutes,
                    QuestionCount = await _context.MCQs.CountAsync(m => m.PastPaperId == p.Id),
                    Score = score, //Get userScore for the given quiz,
                    IsAttempted = await _context.UserProgressions.AnyAsync(u => u.PastPaperId == p.Id && u.UserId == userid),
                    ParticipantsCount = allparticipants.Count()   //Count the number of users participating in the quiz
                };
                
                var t1 = p.WrittenDate;
                var t2 = t1.AddMinutes(p.DurationInMinutes);

                //var diff1 = (t - t1).TotalSeconds;
                //var diff2 = (t - t2).TotalSeconds;
                //var diff3 = (t2 - t1).TotalSeconds;

                if ((t - t2).TotalSeconds > 0 && (t2 - t1).TotalSeconds > 0)
                {
                    pastOlympiads.Add(olym);
                }
                
            }
            return pastOlympiads;
        }

        public async Task<IEnumerable<MCQDto>> GetOlympiadMCQByQuizCode(string quizcode, string userid)
        {
            var pastpapers = await _context.PastPapers.Where(p => p.QuizPassCode == quizcode).ToListAsync();

            
            if (pastpapers.Count == 0 )
            {
                var questions = new List<MCQDto>();
                return questions;
                
            }
            var usp = await _context.UserProgressions.ToListAsync();
            var hasParticipated = await _context.UserProgressions.AnyAsync(u => u.PastPaperId == pastpapers.ElementAt(0).Id && u.UserId == userid);

            if (hasParticipated)
            {
                var questions = new List<MCQDto>();
                return questions;

            }

            //var mcqs = await _context.MCQs.Where(m => m.PastPaperId == pastpapers.ElementAt(0).Id).ToListAsync();

            var mcqDtos = new List<MCQDto>();

            var mcqs = await _context.MCQs.Where(m => m.PastPaperId == pastpapers.ElementAt(0).Id).AsNoTracking()
                .Include(m => m.Options.OrderBy(o => o.Id)).AsNoTracking()
                .Include(m => m.OptionImageUrl.OrderBy(o => o.Id)).AsNoTracking()
                .Include(m => m.Answers).AsNoTracking()
                .ToListAsync();

            foreach (var m in mcqs)
            {
                var mcqdto = new MCQDto()
                {
                    Id = m.Id,
                    Question = m.Question,
                    Instruction = m.Instruction,
                    QuestionImageUrl = m.QuestionImageUrl,
                    Answer = m.Answer,
                    JustificationText = m.JustificationText,
                    JustificationImageUrl = m.JustificationImageUrl,
                    TopicId = m.TopicId,
                    Position = m.Position,
                    PastPaperId = m.PastPaperId
                };

                //var mOptions = await _context.Option.Where(o => o.MCQId == m.Id).ToListAsync();

                //mOptions = mOptions.OrderBy(x => x.Id).ToList();

                if (m.Options.Any())
                {
                    var options = new List<string>();
                    //m.Options = m.Options.OrderBy(o => o.Id).ToList();
                    var opts = new List<MCQOption>();

                    var opA = new MCQOption();
                    var opB = new MCQOption();
                    var opC = new MCQOption();
                    var opD = new MCQOption();
                    var opE = new MCQOption();

                    foreach (var o in m.Options)
                    {
                        //options.Add(o.mcqOption);
                        switch (o.mcqOption[0])
                        {
                            case 'A':
                                opA.Id = o.Id;
                                opA.Option = o.mcqOption;
                                break;
                            case 'B':
                                opB.Id = o.Id;
                                opB.Option = o.mcqOption;
                                break;
                            case 'C':
                                opC.Id = o.Id;
                                opC.Option = o.mcqOption;
                                break;
                            case 'D':
                                opD.Id = o.Id;
                                opD.Option = o.mcqOption;
                                break;
                            case 'E':
                                opE.Id = o.Id;
                                opE.Option = o.mcqOption;
                                break;
                        }
                    }

                    mcqdto.MCQOptions.Add(opA);
                    mcqdto.MCQOptions.Add(opB);
                    mcqdto.MCQOptions.Add(opC);
                    //mcqdto.MCQOptions.Add(opD);

                    if (m.Options.Count == 4)
                    {
                        mcqdto.MCQOptions.Add(opD);
                    }

                    if (m.Options.Count == 5)
                    {
                        mcqdto.MCQOptions.Add(opE);
                    }
                }

                if (m.OptionImageUrl.Any())
                {
                    var options = new List<string>();
                    //m.Options = m.Options.OrderBy(o => o.Id).ToList();
                    var opts = new List<MCQOptionImage>();

                    foreach (var o in m.OptionImageUrl)
                    {
                        options.Add(o.OptionImgUrl);

                        var op = new MCQOptionImage()
                        {
                            Id = o.Id,
                            OptionImageUrl = o.OptionImgUrl
                        };

                        opts.Add(op);
                    }
                    mcqdto.MCQOptionImages.AddRange(opts);
                }

                if (m.Answers.Any())
                {
                    var options = new List<int>();
                    foreach (var o in m.Answers)
                    {
                        options.Add(o.Ans);
                    }

                    mcqdto.Answers.AddRange(options);
                }

                mcqDtos.Add(mcqdto);
            }


            return mcqDtos.OrderBy(m => m.Position);

            //return mcqDtos;
        }

        public async Task<QuizStatusDto> GetQuizStatus(string quizcode, string userid)
        {
            //var hasParticipated = await _context.UserQuizzes.AnyAsync(u => u.QuizId == quizcode && u.UserId == userid);
            var pastpapers = await _context.PastPapers.Where(p => p.QuizPassCode == quizcode).ToListAsync();

            if (pastpapers.Count == 0)
            {
                return null;
            }
            var p = pastpapers.ElementAt(0);

            var quizStatus = new QuizStatusDto()
            {
                Title = p.Title,
                IsApproved = p.IsApproved,
                IsFree = p.IsFree,
                Quantity = p.Quantity,
                WrittenDate = p.WrittenDate,
                DurationInMinutes = p.DurationInMinutes,
                Visibility = p.Visibility,
                QuizNumber = p.QuizNumber,
                IsRightWrong = p.IsRightWrong,
                QuizOwnerId = p.QuizOwnerId,
                SubjectId = p.SubjectID,
                PastPaperId = p.Id,
                HasParticipated = await _context.UserProgressions.AnyAsync(u => u.PastPaperId == p.Id && u.UserId == userid)
            };
            var startTime = quizStatus.WrittenDate; //quiz start time
            var endTime = quizStatus.WrittenDate.AddMinutes(quizStatus.DurationInMinutes); //quiz end time.

            //FOR ONLINE LIVE USE CASE
            var t = DateTime.UtcNow.AddHours(1); //Current time the user wants to take the quiz

            //For local Development.
            //var t = DateTime.Now;//Comment this out before deploying online.

            if(DateTime.Compare(t, startTime) < 0)
            {
                quizStatus.Status = -1; //The quiz is coming in the future

            }else if(DateTime.Compare(t, startTime) >= 0 && DateTime.Compare(t, endTime) <= 0)
            {
                quizStatus.Status = 0;
            }
            else
            {
                quizStatus.Status = 1;
            }
            
            return quizStatus;
        }

        public async Task<List<QuizResultDto>> GetQuizResultByPaperId(string pastpaperId)
        {
            var quizResultStats = new List<QuizResultDto>();
            var participants = await _context.UserProgressions.Where(pp => pp.PastPaperId == pastpaperId).ToListAsync();
            var allparticipants = participants.GroupBy(x => x.UserId);
            //Count the number of users
            var numberOfParticipants = allparticipants.Count();   //current user was exempted from the query       
            
            foreach (var u in allparticipants)
            {
                //var ups =  await _context.UserProgressions.Where(up => up.PastPaperId == pastpaperId && up.UserId == u.Key && up.AnswerStatus == 1).ToListAsync();
                
                int score = u.Count(up => up.PastPaperId == pastpaperId && /*up.UserId == u.Key &&*/ up.AnswerStatus == 1);

                var addresses = await _context.Addresses.Where(a => a.UserId == u.Key).ToListAsync();
                var address = new Admin.Shared.Models.Address();
                if (addresses.Count > 0)
                    address = addresses.ElementAt(0);
                var quizStats = new QuizResultDto
                {
                    Username = address.UserName,
                    UserId = address.UserId,
                    TotalParticipants = numberOfParticipants,
                    //Price = _context.PastPapers.Single(p => p.Id == pastpaperId).Url,
                    Phone = address.Phone,
                    RegionOfOrigin = address.RegionOfOrigin,
                    Town = address.Town,
                    School = address.School,
                    //Rank = await getUserRankById(pastpaperId, u.Key, score),
                    Score = score,
                    PastPaperId = pastpaperId
                };
                
               /* if(quizStats.Rank == 1)
                {
                    quizStats.IsWinner = true;
                    quizStats.Price = _context.PastPapers.Single(p => p.Id == pastpaperId).Url;
                }*/
                quizResultStats.Add(quizStats);
            }

            var sortedByRank = quizResultStats.OrderByDescending(x => x.Score).ThenBy(y => y.Username);
            int rank = 1;
            foreach(var s in sortedByRank)
            {
                s.Rank = rank;
                if(rank == 1)
                {
                    s.IsWinner = true;
                    s.Price = _context.PastPapers.Single(p => p.Id == pastpaperId).Url;
                }
                rank = rank + 1;
            }

            return sortedByRank.ToList();
        }

        
    }
}
