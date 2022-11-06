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

namespace Admin.Server.Repositories.FrontEnd.McqPastpaper
{
    public class McqPastpersRepository : ControllerBase, IMcqPastpersRepository
    {
        private readonly ApplicationDbContext _context;

        public McqPastpersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MCQDto>> Get(string pastpaperId, string userId)
        {
            var mcqDtos = new List<MCQDto>();

            var pastPaper = await _context.PastPapers.SingleAsync(p => p.Id == pastpaperId);
            if(pastPaper.Status == "Free" || pastPaper.Status == "Basic" || await IsPastPaperValid(pastPaper.SubjectID, userId))
            {
                var mcqs = await _context.MCQs.Where(m => m.PastPaperId == pastpaperId).AsNoTracking()
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
                        PastPaperId = m.PastPaperId,
                        VideoUrl = m.VideoUrl,
                        IsAnsweredCorrectly = !string.IsNullOrEmpty(userId) ? await _context.UserProgressions.AnyAsync(u => u.PastPaperId == m.PastPaperId && u.UserId == userId && u.QuestionPosition == m.Position) : false
                    };

                    /*if (!string.IsNullOrEmpty(userId))
                    {
                        mcqdto.IsAnsweredCorrectly = await _context.UserProgressions.AnyAsync(u => u.PastPaperId == m.PastPaperId && u.UserId == userId && u.QuestionPosition == m.Position);
                    }*/


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
                            //options.Add(o.mcqOption);//Grab the first letter : A, B, C, D, E -----> foreeach option and Initialize the various options
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

                        if (m.Options.Count >= 4)
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
            }

            return mcqDtos;
        }

        public async Task<IEnumerable<MCQDto>> GetMcqsByTopics(int topicId, string userId)
        {
            var mcqDtos = new List<MCQDto>();

            var mcqs = await _context.MCQs.Where(m => m.TopicId == topicId).AsNoTracking()
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
                    VideoUrl = m.VideoUrl,
                    PastPaperId = m.PastPaperId
                };

                if (!string.IsNullOrEmpty(userId))
                {
                    mcqdto.IsAnsweredCorrectly = await _context.UserProgressions.AnyAsync(u => u.PastPaperId == m.PastPaperId && u.UserId == userId && u.QuestionPosition == m.Position);
                }

                mcqdto.ExamYear = m.PastPaperId.Substring(m.PastPaperId.Length - 4);

                if (m.Options.Any())
                {
                    //m.Options = m.Options.OrderBy(o => o.Id).ToList();
                    var opts = new List<MCQOption>();

                    var opA = new MCQOption();
                    var opB = new MCQOption();
                    var opC = new MCQOption();
                    var opD = new MCQOption();
                    var opE = new MCQOption();

                    var options = new List<string>();
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

                    if (m.Options.Count >= 4)
                    {
                        mcqdto.MCQOptions.Add(opD);
                    }

                    if (m.Options.Count == 5)
                    {
                        mcqdto.MCQOptions.Add(opE);
                    }

                    //mcqdto.Options.AddRange(options);
                }

                if (m.OptionImageUrl.Any())
                {
                    //m.OptionImageUrl = m.OptionImageUrl.OrderBy(o => o.Id).ToList();
                    var options = new List<string>();
                    foreach (var o in m.OptionImageUrl)
                    {
                        options.Add(o.OptionImgUrl);
                    }

                    mcqdto.OptionImageUrl.AddRange(options);
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

            var questions = mcqDtos.OrderBy(m => m.ExamYear).ToList();

            return questions;
        }

        public async Task<IEnumerable<TopicsDto>> GetTopicDtosForGivenSubjectID(int id, string userid)
        {
            var topicDtos = new List<TopicsDto>();

            var topics = await _context.Topic.Where(t => t.SubjectId == id).OrderBy(o => o.TopicNum).AsNoTracking()
                .Include(t => t.Subject).AsNoTracking()
                .ToListAsync();            

            foreach (var t in topics)
            {
                var topic = new TopicsDto()
                {
                    Id = t.Id,
                    SubjectId = t.SubjectId,
                    TopicNumber = t.TopicNum,
                    Title = t.Title,
                    SubjectTitle = t.Subject.Title,
                    TotalQuestionCount = await _context.MCQs.CountAsync(m => m.SubjectId == id && m.TopicId == t.TopicNum),
                    //PassedQuestionCount = await _context.UserProgressions.CountAsync(up => up.SubjectId == id && up.TopicNum == t.TopicNum && up.PaperNumber < 2 && up.UserId == userid)
                };

                if (string.IsNullOrEmpty(userid))
                {
                    topic.PassedQuestionCount = await _context.UserProgressions.CountAsync(up => up.SubjectId == id && up.TopicNum == t.TopicNum && (up.PaperNumber < 2 || up.PaperNumber >= 4));

                    topic.FailedQuestionCount = topic.TotalQuestionCount - topic.PassedQuestionCount;

                    var percentage = Math.Round((topic.PassedQuestionCount / topic.TotalQuestionCount) * 100, 2);
                    topic.PercentageCoverage = percentage;
                }
                else
                {
                    topic.PassedQuestionCount = 0; //await _context.UserProgressions.CountAsync(up => up.SubjectId == id && up.TopicNum == t.TopicNum && up.PaperNumber < 2 && up.UserId == userid);
                    topic.PercentageCoverage = 0;
                }
                

                topicDtos.Add(topic);
            }
            return topicDtos;
        }

        async Task<bool> IsPastPaperValid(int subjectId, string userid)
        {
            bool isValid = false;

            var us = await _context.UserSubjects.Where(u => u.AppUserId == userid && u.SubjectId == subjectId).ToListAsync();
            if (us.Any())
            {
                var ust = us.ElementAt(0);

                var d1 = ust.ExpiryDate - ust.EnrollmentDate;

                var d2 = ust.EnrollmentDate - ust.EnrollmentDate.AddDays(ust.Duration);

                if(d1.TotalSeconds > 1 || d2.TotalSeconds > 1)
                {
                    return true;
                }
            }


            return isValid;
        }

    }
}
