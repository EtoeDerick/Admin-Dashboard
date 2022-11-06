using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Conversations
{
    public interface IConversationsRepository
    {
        Task<IEnumerable<Conversation>> Get();
        Task<Conversation> Get(int id);
        Task<Conversation> Create(Conversation conversation);
        Task<ActionResult<Conversation>> Update(int id, Conversation conversation);
        Task<ActionResult> Delete(int id);
    }

    
}
