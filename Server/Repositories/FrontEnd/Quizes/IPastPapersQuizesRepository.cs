using Admin.Shared.Dtos;
using Admin.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Quizes
{
    public interface IPastPapersQuizesRepository
    {
        Task<IEnumerable<PastPaper>> Get();
        Task<QuizResultsDto> Get(string id);
    }
}
