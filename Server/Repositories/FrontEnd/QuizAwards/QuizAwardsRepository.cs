using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.QuizAwards
{
    public class QuizAwardsRepository : IQuizAwardsRepository
    {
        private readonly ApplicationDbContext _context;
        public QuizAwardsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<AwardComment>> Create(string message, string userid)
        {
            var conversation = new Conversation() 
            {
                Date = DateTime.UtcNow,
                MessageTitle = "QuiComments",
                MessageDescription = message,
                UserId = userid,
                DiscussionForumId = -10,
                SubjectId = 10
            };

            _context.Conversations.Add(conversation);
            try
            {
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
            }
            //var conversations = await _context.Conversations.Where(x => x.DiscussionForumId == -10).ToListAsync();

            var conversations = _context.Conversations.Where(c => c.DiscussionForumId == -10).Select(conversation => new AwardComment
            {
                UserName = _context.Addresses.Single(a => a.UserId == conversation.UserId).UserName,
                Message = conversation.MessageDescription,
                DurationTimeSpan = DateTime.UtcNow - conversation.Date,
                Date = conversation.Date
            }) ;

            var allConversations = await conversations.OrderByDescending(x => x.Date).ThenBy(y => y.UserName).ToListAsync();

            foreach (var c in allConversations)
            {
                if (string.IsNullOrEmpty(c.UserName))
                {
                    c.UserName = "Anonymous User";
                }

                c.UserNameInitialLetter = c.UserName.Substring(0, 1);


                var seconds = (int) c.DurationTimeSpan.TotalSeconds % 60;
                var mins = (int) c.DurationTimeSpan.TotalMinutes % 60;
                var hours = (int) c.DurationTimeSpan.TotalHours;
                var days = (int) c.DurationTimeSpan.TotalDays;
                var months = (int) days > 30 ? days % 30 : 0;
                var years = (int) days > 364 ? days % 365 : 0;

                if(years > 0)
                {
                    c.Duration = years.ToString() + " years ago";
                }
                else if(months > 0)
                {
                    c.Duration = months.ToString() + " months ago";
                }else if( days > 0)
                {
                    c.Duration = days.ToString() + " days ago";
                }else if(hours > 0)
                {
                    c.Duration = hours.ToString() + " hours ago";
                }else if( mins > 0)
                {
                    c.Duration = mins.ToString() + " minutes ago";
                }else
                {
                    c.Duration = seconds.ToString() + " seconds ago";
                }

            }

            return allConversations;
        }

        public async Task<ActionResult<QuizAward>> CreateQuizAward(QuizAward quizAward)
        {
            _context.QuizAwards.Add(quizAward);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
            }

            return quizAward;
        }

        public async Task<ActionResult<List<AwardComment>>> GetAllApprovedQuizAwardsConversations()
        {
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

            return allConversations;
        }

        public async Task<ActionResult<QuizAward>> GetQuizAwardWithPastPaperId(string pastpaperId)
        {
            var quizAwards = await _context.QuizAwards.Where(x => x.PastPaperId == pastpaperId).ToListAsync();
            var quizAward = quizAwards.ElementAt(0);
            return quizAward;
        }

        public async Task<ActionResult<Admin.Shared.Models.QuizAward>> UpdateQuizAwardWithPastPaperId(string pastpaperId, QuizAward quizAward)
        {
            //var quizAwards = await _context.QuizAwards.Where(x => x.PastPaperId == pastpaperId).ToListAsync();

            if (pastpaperId != quizAward.PastPaperId)
            {
                quizAward.Description = "";
                quizAward.PastPaperId = "";
                return quizAward;
            }
            quizAward.PastPaperId = pastpaperId;

            _context.Entry(quizAward).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
            }



            return quizAward;
        }

        public async Task<ActionResult<int>> Delete(int id)
        {
            var quizToDelete = await _context.QuizAwards.FindAsync(id);

            if ( quizToDelete == null)
            {
                return 0;
            }

            //2. Delete the actual past paper record
            _context.QuizAwards.Remove(quizToDelete);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<ActionResult<List<QuizAward>>> GetAllQuizesAwarded()
        {
            return await _context.QuizAwards.ToListAsync();
        }
    }
}
