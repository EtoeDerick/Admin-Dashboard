using Admin.Server.Data;
//using Admin.Server.Migrations;
using Admin.Server.Repositories.FrontEnd.QuizAwards;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Pastpaperquizawards
{
    public class PastpaperquizawardsRepository : IPastpaperquizawardsRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IQuizAwardsRepository _db;
        public PastpaperquizawardsRepository(ApplicationDbContext context, IQuizAwardsRepository db)
        {
            _context = context;
            _db = db;
        }
        public async Task<ActionResult<IEnumerable<Admin.Shared.Models.QuizAwardDto>>> GetAllQuizesAwarded()
        {
            var papers = new List<QuizAwardDto>();
            var quizAwards = await _context.QuizAwards.ToListAsync();
            if (quizAwards.Any())
            {
                foreach(var p in quizAwards)
                {
                    var quiz = new QuizAwardDto()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        PastPaperId = p.PastPaperId,
                       // PastPaperTitle = _context.PastPapers.Single(x => x.Id == p.PastPaperId).Title,
                        StudentImageUrl = p.StudentImageUrl,
                        AwardedDate = p.AwardedDate,
                        Description = p.Description
                    };
                    var pastpapers = await _context.PastPapers.Where(x => x.Id == p.PastPaperId).ToListAsync();
                    if(pastpapers.Any())
                    {
                        quiz.PastPaperTitle = pastpapers.ElementAt(0).Title;
                    }
                    papers.Add(quiz);
                }
            }

            return papers;
        }

        public async Task<ActionResult<QuizResultDto>> GetWinnerId(string pastpaperId)
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
                    TotalParticipants = numberOfParticipants,
                    //Price = _context.PastPapers.Single(p => p.Id == pastpaperId).Url,
                    Phone = address.Phone,
                    RegionOfOrigin = address.RegionOfOrigin,
                    Town = address.Town,
                    School = address.School,
                    //Rank = await getUserRankById(pastpaperId, u.Key, score),
                    Score = score,
                    UserId = u.Key,
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
            foreach (var s in sortedByRank)
            {
                s.Rank = rank;
                if (rank == 1)
                {
                    s.IsWinner = true;
                    s.Price = _context.PastPapers.Single(p => p.Id == pastpaperId).Url;
                }
                rank = rank + 1;
            }

            return sortedByRank.ElementAt(0);
        }

        public async Task<ActionResult<PriceAwardPageDto>> GetWinnerInfo(string pastpaperId, string userid)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == userid);
            var award = await _context.QuizAwards.FirstOrDefaultAsync(x => x.UserId == userid && x.PastPaperId == pastpaperId);

            var awardPagInfo = new PriceAwardPageDto()
            {
                StudentName = address.UserName,
                School = address.School,
                ImageUrl = award.StudentImageUrl,
                Description = award.Description,
                AwardDate = award.AwardedDate.ToString("dddd, dd MMMM yyyy")
            };
            var conversations = _context.Conversations.Where(c => c.DiscussionForumId == -10).Select(conversation => new AwardComment
            {
                UserName = _context.Addresses.Single(a => a.UserId == conversation.UserId).UserName,
                Message = conversation.MessageDescription,
                DurationTimeSpan = DateTime.UtcNow - conversation.Date,
                Date = conversation.Date
            });

            var allConversations = await conversations.OrderByDescending(x => x.Date).ThenBy(y => y.UserName).ToListAsync();

            foreach (var c in allConversations)
            {
                if (string.IsNullOrEmpty(c.UserName))
                {
                    c.UserName = "Anonymous User";
                }

                c.UserNameInitialLetter = c.UserName.Substring(0, 1);


                var seconds = (int)c.DurationTimeSpan.TotalSeconds % 60;
                var mins = (int)c.DurationTimeSpan.TotalMinutes % 60;
                var hours = (int)c.DurationTimeSpan.TotalHours;
                var days = (int)c.DurationTimeSpan.TotalDays;
                var months = (int)days > 30 ? days % 30 : 0;
                var years = (int)days > 364 ? days % 365 : 0;

                if (years > 0)
                {
                    c.Duration = years.ToString() + " years ago";
                }
                else if (months > 0)
                {
                    c.Duration = months.ToString() + " months ago";
                }
                else if (days > 0)
                {
                    c.Duration = days.ToString() + " days ago";
                }
                else if (hours > 0)
                {
                    c.Duration = hours.ToString() + " hours ago";
                }
                else if (mins > 0)
                {
                    c.Duration = mins.ToString() + " minutes ago";
                }
                else
                {
                    c.Duration = seconds.ToString() + " seconds ago";
                }

            }

            awardPagInfo.AwardComments.AddRange(allConversations);
            return awardPagInfo ;
        }
    }
}
