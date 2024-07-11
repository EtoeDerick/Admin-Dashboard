using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.FrontEnd.Votes;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class VotesController: ControllerBase
    {
        private readonly IVotesRepository _db;
        public VotesController(IVotesRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<Vote>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        //ConversationId as input parameter: Retrieve a list of replies as conversation.
        //id: ID of the current conversation
        //
        [HttpGet("{id}")]
        public async Task<IEnumerable<ForumDiscussionPageDto>> GetConversationReplies(int id, int forumId, int subjectId, int pageNumber)
        {
            return await _db.GetReplies(id, forumId, subjectId, pageNumber);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IEnumerable<ForumDiscussionPageDto>> GetConversationForSubjectByForumID(int id, int subjectId, int pageNumber, string userId=null)
        {
            return await _db.GetConversationsByForumId(id, subjectId, pageNumber);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vote>> Post (int conversationId, string userId = null)
        {
            var userid = userId;
            if (string.IsNullOrEmpty(userId))
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    userid = claim.Value;
                }
            }
            
            var vote = new Vote()
            {
                ConversationId = conversationId,
                UserId = userid
            };

            var voted = await _db.Create(vote);

            return voted;
        }

        [HttpPost("{id}")]
        public async Task<Conversation> CreateClientConversationAsync(int id, int subjectId, string title, string description, bool isReply = false, int replyId = 0, string userId=null)
        {
            var userid = userId;
            if (string.IsNullOrEmpty(userId))
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    userid = claim.Value;
                }
            }
            
            var conversation = new Conversation()
            {
                DiscussionForumId = id,
                SubjectId = subjectId,
                UserId = userid,
                Date = DateTime.Now,
                MessageTitle = title,
                MessageDescription = description,
                IsAReply = isReply,
                ReplyId = replyId,
                Votes = null
            };

            return await _db.CreateConversationFromMobileApp(conversation);
        }

        // DELETE: api/Examinations/5
        //[HttpDelete("{id}")]
        //public async Task DeleteConstant(int id)
        //{
        //    await _db.Delete(id);
        //}
    }
}
