using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Votes
{
    public interface IVotesRepository
    {
        //Task<IEnumerable<Admin.Shared.Models.Constants>> Get();
        
        Task<Vote> Create(Vote vote);
        Task<IEnumerable<Vote>> Get();
        Task<IEnumerable<ForumDiscussionPageDto>> GetReplies(int id, int forumId, int subjectId, int pageNumber);
        Task<IEnumerable<ForumDiscussionPageDto>> GetConversationsByForumId(int id, int subjectId, int pageNumber);
        Task<Conversation> CreateConversationFromMobileApp(Conversation conversation);
        //Task<ActionResult<Admin.Shared.Models.Constants>> Update(int id, Admin.Shared.Models.Constants constants);
        //Task<ActionResult> Delete(int id);
    }

    
}
