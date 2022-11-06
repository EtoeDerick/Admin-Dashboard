using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Votes
{
    public class VotesRepository : ControllerBase, IVotesRepository
    {
        private readonly ApplicationDbContext _context;

        public VotesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //User liking and unliking a comment
        public async Task<Vote> Create(Vote vote)
        {
            var voted = new Vote();
            if (!VoteExists(vote.ConversationId, vote.UserId))
            {
                voted = null;
                vote.IsLiked = true;
                _context.Votes.Add(vote);
            }
            else
            {
                voted = await _context.Votes.SingleAsync(v => v.ConversationId == vote.ConversationId && v.UserId == vote.UserId);
                voted.Conversation = null;

                voted.IsLiked = !voted.IsLiked;
                voted.IsUnliked = !voted.IsUnliked;
                                
                voted.Date = DateTime.Now;

                _context.Entry(voted).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                
                Console.WriteLine("Error", ex.Message);
            }

            if(voted == null)
            {
                return vote;
            }

            return voted;
        }

        public async Task<Conversation> CreateConversationFromMobileApp(Conversation conversation)
        {
            _context.Conversations.Add(conversation);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                Console.WriteLine("Error", ex.Message);
            }

            return conversation;
        }

        public async Task<IEnumerable<Vote>> Get()
        {
            return await _context.Votes.ToListAsync();
        }

        //Getting replies for a specific discussion thread! INPUT PARAMETER
        //discussionid = discussion replyid; the forum it happend: forumid and the subject concerned: subjectid
        public async Task<IEnumerable<ForumDiscussionPageDto>> GetReplies(int id, int forumId, int subjectId, int pageNumber)
        {
            var pageLength = 20;
            var conversations =  await _context.Conversations.Where(c => c.IsAReply == true && c.ReplyId == id && c.Id != id 
            && c.DiscussionForumId == forumId && c.SubjectId == subjectId && c.IsNotApproved == false).Skip((pageNumber - 1) * pageLength).Take(pageLength).ToListAsync();

            var replies = new List<ForumDiscussionPageDto>();
            foreach(var c in conversations)
            {
                var reply = new ForumDiscussionPageDto()
                {
                    Id = c.Id,
                    SubjectId = c.SubjectId,
                    UserId = c.UserId,
                    UserName = _context.AppUsers.Any(a => a.Id == c.UserId) ? _context.AppUsers.Single(a => a.Id == c.UserId).UserName : "Anonymous",
                    ReplyId = c.ReplyId,
                    Duration = getDuration(c.Date),
                    MessageTitle = c.MessageTitle,
                    MessageDescription = c.MessageDescription,
                    ReplyCount = conversations.Count(c => c.ReplyId == id),
                    LikeCount = await _context.Votes.CountAsync(v => v.IsLiked),
                    Date = c.Date
                };

                replies.Add(reply);
            }

            return replies.OrderByDescending(r => r.Date);
        }

        public async Task<IEnumerable<ForumDiscussionPageDto>> GetConversationsByForumId(int id, int subjectId, int pageNumber)
        {
            var pageLength = 20;
            var conversations = await _context.Conversations.Where(c => c.DiscussionForumId == id && c.SubjectId == subjectId && c.IsAReply == false && c.IsNotApproved == false)
                //.OrderByDescending(x => x.Date)
                .Skip((pageNumber - 1 )* pageLength).Take(pageLength).ToListAsync();

            var replies = new List<ForumDiscussionPageDto>();
            foreach (var c in conversations)
            {
                var reply = new ForumDiscussionPageDto()
                {
                    Id = c.Id,
                    SubjectId = c.SubjectId,
                    UserId = c.UserId,
                    UserName = _context.AppUsers.Any(a => a.Id == c.UserId)? _context.AppUsers.Single(a => a.Id == c.UserId).UserName : "Anonymous",
                    ReplyId = c.ReplyId,
                    Duration = getDuration(c.Date),
                    MessageTitle = c.MessageTitle,
                    MessageDescription = c.MessageDescription,
                    ReplyCount = conversations.Count(cur => cur.ReplyId == c.ReplyId),
                    LikeCount = await _context.Votes.CountAsync(v => v.IsLiked && v.ConversationId == c.Id),
                    Date = c.Date
                };

                replies.Add(reply);
            }

            return replies.OrderByDescending(r => r.Date);
        }

        private string getDuration(DateTime date)
        {
            string duration = "";
            var now = DateTime.Now;

            var timeSpanInSeconds = Convert.ToInt64( Math.Abs((now - date).TotalSeconds));
            var timeSpanInMins = Convert.ToInt64(Math.Abs((now - date).TotalMinutes));
            var timeSpanInHours = Convert.ToInt64(Math.Abs((now - date).TotalHours));
            var timeSpanInDays = Convert.ToInt64(Math.Abs((now - date).TotalDays));

            if (timeSpanInSeconds < 60)
            {
                //Duration in seconds
                duration = (timeSpanInSeconds/60).ToString() + "sec ago";
            }
            else if (timeSpanInMins < 60 )
            {
                duration = (timeSpanInMins).ToString() + "min ago";
            }
            else if (timeSpanInHours < 24)
            {
                duration = timeSpanInHours.ToString() + "hr ago";
            }
            else if (timeSpanInDays < 7)
            {
                duration = timeSpanInDays.ToString() + "wk ago";
            }
            else if (timeSpanInDays < 30)
            {
                duration = timeSpanInDays. ToString() + "mn ago";
            }
            else if (timeSpanInDays > 364)
            {
                //duration = (timeSpanInDays / 364).ToString() + "min ago";
                duration = (timeSpanInDays  / 364).ToString() + "yr ago";
            }

            return duration;
        }

        private bool VoteExists(int id, string userid)
        {
            return _context.Votes.Any(v => v.ConversationId == id && v.UserId == userid);
        }

        
    }
}
