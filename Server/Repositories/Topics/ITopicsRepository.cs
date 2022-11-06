using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Topics
{
    public interface ITopicsRepository
    {
        Task<IEnumerable<Topic>> Get();
        Task<Topic> Get(int id);
        Task<Topic> Create(Topic topic);
        Task<ActionResult<Topic>> Update(int id, Topic topic);
        Task<ActionResult> Delete(int id);
    }

    
}
