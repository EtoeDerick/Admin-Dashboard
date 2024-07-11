using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Admin.Shared.Models.ETQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.ETQ
{
    public class ETQRepository : ControllerBase, IETQRepository
    {
        private readonly ApplicationDbContext _context;

        public ETQRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public async Task<EssayPaperNTopicsDto> Get(int subjectId, string userId)
        {
            //var p2s = new List<PaperOnePastPaperDto>();
            var p2n3s = new List<PastPaper2n3Dto>();

            bool isSubjectFree = false;
            //If subjectId was found, get subjectStatus: whether subject is FREE
            if (subjectId > 0)
            {
                isSubjectFree = _context.Subjects.Single(s => s.Id == subjectId).IsFree;
            }

            var pastpapers = await _context.PastPapers.Where(p => p.SubjectID == subjectId && (p.PaperNumber == 2 || p.PaperNumber == 3) ).ToListAsync();
            var subjectTitle = _context.Subjects.Single(s => s.Id == subjectId).Title;
            foreach (var p in pastpapers)
            {
                var p2n3 = new PastPaper2n3Dto()
                {
                    Id = p.Id,
                   SubjectId = p.SubjectID,
                    SubjectTitle = subjectTitle,
                    Year = p.PaperYear,
                    NumberOfQuestions = p.Quantity,
                    CorrectAnsweredCount = await _context.UserProgressions.CountAsync(up => up.PastPaperId == p.Id && (p.PaperNumber == 2 || p.PaperNumber == 3)),
                    IsDownloaded = _context.DownloadTrackingTables.Any(d => d.PastPaperId == p.Id && d.IsDownloaded) ? true : false,
                    DownloadSize = p.DownloadSize,
                    PastPaperTitle = p.Title,
                    PaperNumber = p.PaperNumber,
                    TotalVideoDownloadSizeAndCount = p.Url,
                    Status = p.Status
                };
                p2n3.IsNotDownloaded = !p2n3.IsDownloaded;

                var us = new List<UserSubject>();
                if (!string.IsNullOrEmpty(userId))
                {
                    us = await _context.UserSubjects.Where(u => u.SubjectId == subjectId && u.AppUserId == userId).ToListAsync();
                }

                if (!string.IsNullOrEmpty(userId) && us.Count > 0)
                {
                    var usersubject = us.ElementAt(0);
                    var span = usersubject.EnrollmentDate.AddDays(usersubject.Duration) - DateTime.UtcNow;

                    if (span.TotalSeconds < 1)
                    {
                        if (p2n3.Status == "Free")
                        {
                            p2n3.StatusColor = "LimeGreen";
                        }
                        else if (p2n3.Status == "Basic")
                        {
                            p2n3.StatusColor = "DodgerBlue";
                        }
                        else
                        {
                            p2n3.StatusColor = "#da9100";
                        }
                    }
                    else if (p2n3.Status == "Free")
                    {
                        p2n3.StatusColor = "LimeGreen";
                    }
                    else
                    {
                        p2n3.Status = "Paid";
                        p2n3.StatusColor = "#8fbc8f"; //Dark Green
                    }

                }
                else
                {
                    if (p2n3.Status == "Free")
                    {
                        p2n3.StatusColor = "LimeGreen";
                    }
                    else if (p2n3.Status == "Basic")
                    {
                        p2n3.StatusColor = "DodgerBlue";
                    }
                    else
                    {
                        p2n3.StatusColor = "#da9100";
                    }
                }

                if (isSubjectFree)
                {
                    p2n3.Status = "OBC";
                    p2n3.StatusColor = "OrangeRed";
                }

                if (p2n3.CorrectAnsweredCount == 0)
                {
                    p2n3.IsRed = true;
                }
                else if (p2n3.NumberOfQuestions == p2n3.CorrectAnsweredCount)
                {
                    p2n3.IsGreen = true;
                }
                else
                {
                    p2n3.IsYellow = true;
                }

                if (p2n3.SubjectId > 400)
                {
                    p2n3.Month = "June";
                }
                else
                {
                    p2n3.Month = "Sept.";
                }
                p2n3s.Add(p2n3);
                                
            }

            var topics = await _context.Topic.Where(t => t.SubjectId == subjectId).ToListAsync();
            var etqDtos = new List<ETQTopicsDto>();
            if (topics.Any())
            {
                foreach(var t in topics)
                {
                    var topic = new ETQTopicsDto()
                    {
                        Id = t.Id,
                        SubjectId = subjectId,
                        TopicNumber = t.TopicNum,
                        PaperNumber = t.IsAlsoP3Topic ? 3 : 2,
                        Title = t.Title,
                        SubjectTitle = subjectTitle,
                        CorrectAnsweredCount = await _context.UserProgressions.CountAsync(up => up.TopicNum == t.TopicNum && (up.PaperNumber == 2 || up.PaperNumber == 3))                        
                    };
                    
                    //Get count of questions for each topic
                    var k = 0;
                    var counts = await _context.EssayTypeQuestions.Include(e => e.Questions).AsNoTracking().ToListAsync();
                    foreach(var c in counts)
                    {
                        foreach(var d in c.Questions)
                        {
                            if(d.TopicId == topic.TopicNumber)
                            {
                                k = k + 1;
                                break;
                            }
                        }
                    }

                    topic.NumberOfQuestions = k;

                    if (topic.CorrectAnsweredCount == 0)
                    {
                        topic.IsRed = true;
                    }
                    else if (topic.NumberOfQuestions == topic.CorrectAnsweredCount)
                    {
                        topic.IsGreen = true;
                    }
                    else
                    {
                        topic.IsYellow = true;
                    }

                    if (topic.SubjectId > 400)
                    {
                        topic.Month = "June";
                    }
                    else
                    {
                        topic.Month = "Sept.";
                    }
                    etqDtos.Add(topic);
                }
            }

            var p2n3withTopicsDto = new EssayPaperNTopicsDto()
            {
                ETQTopicsDtos = etqDtos,
                PastPaper2N3Dtos = p2n3s
            };

            return p2n3withTopicsDto;
        }

        public async Task<ActionResult<IEnumerable<EssayTypeQuestion>>> GetEssayTypeQuestionCollection(string id)
        {
            var etqs = await _context.EssayTypeQuestions.Where(e => e.PastPaperId == id).OrderBy(o => o.Position).AsNoTracking()
                .Include(e => e.Questions.OrderBy(x => x.Position)).ThenInclude(q => q.QuestionSolution.OrderBy(o => o.Position)).OrderBy(o => o.Position).AsNoTracking()
                .Include(e => e.Questions.OrderBy(x => x.Position)).ThenInclude(q => q.SubQuestions.OrderBy(o => o.Position)).ThenInclude(s => s.Solution.OrderBy(x => x.Position)).OrderBy(o => o.Position).AsNoTracking()
                .ToListAsync();

            var ETQuestions = new List<EssayTypeQuestion>();

            foreach(var e in etqs)
            {
                var etq = new EssayTypeQuestion()
                {
                    Id = e.Id,
                    HasUniqueSolution = e.HasUniqueSolution,
                    TotalMarks = e.TotalMarks,
                    Position = e.Position,
                    Introduction = e.Introduction,
                    ImageUrlAfterIntroduction = e.ImageUrlAfterIntroduction,
                    ImageUrlBeforeIntroduction = e.ImageUrlBeforeIntroduction,
                    VideoUrl = e.VideoUrl,
                    PastPaperId = e.PastPaperId,
                    Questions = new List<Question>()
                };

                var Questions = e.Questions.OrderBy(x => x.Position).ToList();

                foreach (var q in Questions)
                {
                    q.QuestionSolution = q.QuestionSolution.OrderBy(o => o.Position).ToList();
                    q.SubQuestions = q.SubQuestions.OrderBy(o => o.Position).ToList();
                    
                    foreach(var sq in q.SubQuestions)
                    {
                        sq.Solution = sq.Solution.OrderBy(o => o.Position).ToList();
                    }

                    etq.Questions.Add(q);
                }

                ETQuestions.Add(etq);
            }



            return await Task.FromResult(ETQuestions);
        }

        public async Task<IEnumerable<EssayTypeQuestion>> GetETQByTopicNumberAndPaperNumber(int id, int paperNumber)
        {
            var etqdtos = new List<EssayTypeQuestion>();
            var essayTypeQuestions = new List<EssayTypeQuestion>();

            var pastpapers = await _context.PastPapers.Where(p => p.PaperNumber == paperNumber).AsNoTracking()
                .Include(p => p.EssayTypeQuestions)
                .ToListAsync();

            foreach(var p in pastpapers)
            {
                //ets: contains ONLY a list of etqs with the given paperNumber
                var ets = await _context.EssayTypeQuestions.Where(e => e.PastPaperId == p.Id).OrderBy(o => o.Position).AsNoTracking()
                .Include(e => e.Questions.Where(q => q.TopicId == id).OrderBy(o => o.Position)).ThenInclude(q => q.QuestionSolution).OrderBy(o => o.Position).AsNoTracking()
                .Include(e => e.Questions.Where(q => q.TopicId == id).OrderBy(o => o.Position)).ThenInclude(q => q.SubQuestions).ThenInclude(s => s.Solution).OrderBy(o => o.Position).AsNoTracking()
                .ToListAsync();                

                if (ets.Any())
                {
                    etqdtos.AddRange(ets);
                }
            }
                        
            foreach(var eq in etqdtos)
            {
                if (eq.Questions.Any())
                {
                    essayTypeQuestions.Add(eq);
                }               
            }

            var ETQuestions = new List<EssayTypeQuestion>();

            foreach (var e in essayTypeQuestions)
            {
                var etq = new EssayTypeQuestion()
                {
                    Id = e.Id,
                    HasUniqueSolution = e.HasUniqueSolution,
                    TotalMarks = e.TotalMarks,
                    Position = e.Position,
                    Introduction = e.Introduction,
                    ImageUrlAfterIntroduction = e.ImageUrlAfterIntroduction,
                    ImageUrlBeforeIntroduction = e.ImageUrlBeforeIntroduction,
                    VideoUrl = e.VideoUrl,
                    PastPaperId = e.PastPaperId,
                    Questions = new List<Question>()
                };

                var Questions = e.Questions.OrderBy(x => x.Position).ToList();

                foreach (var q in Questions)
                {
                    q.QuestionSolution = q.QuestionSolution.OrderBy(o => o.Position).ToList();
                    q.SubQuestions = q.SubQuestions.OrderBy(o => o.Position).ToList();

                    foreach (var sq in q.SubQuestions)
                    {
                        sq.Solution = sq.Solution.OrderBy(o => o.Position).ToList();
                    }

                    etq.Questions.Add(q);
                }

                ETQuestions.Add(etq);
            }


            return await Task.FromResult(ETQuestions);
        }

       
    }
}
