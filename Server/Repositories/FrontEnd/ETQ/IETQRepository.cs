using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Admin.Shared.Models.ETQ;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.ETQ
{
    public interface IETQRepository
    {
        //Task<IEnumerable<Admin.Shared.Models.Constants>> Get();
        Task<EssayPaperNTopicsDto> Get(int subjectId, string userId);
        Task<ActionResult<IEnumerable<EssayTypeQuestion>>> GetEssayTypeQuestionCollection(string id);
        Task<IEnumerable<EssayTypeQuestion>> GetETQByTopicNumberAndPaperNumber(int id, int paperNumber);
        //Task<Admin.Shared.Models.Constants> Create(Admin.Shared.Models.Constants constants);
        //Task<ActionResult<Admin.Shared.Models.Constants>> Update(int id, Admin.Shared.Models.Constants constants);
        //Task<ActionResult> Delete(int id);
    }

    
}
