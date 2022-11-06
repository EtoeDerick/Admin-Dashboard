using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Mcqs
{
    public interface IMCQRepository
    {
        Task<IEnumerable<MCQ>> Get();
        Task<IEnumerable<MCQ>> GetMCQByPastPaperId(string id);
        Task<ActionResult<MCQ>> Update(int id, MCQ mcq);
        Task<ActionResult<MCQ>> Create(MCQ mcq);
        Task<ActionResult> Delete(int id);

        Task<MCQ> Get(int id);
    }

    
}
