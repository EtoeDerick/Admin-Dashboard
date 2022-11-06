using Admin.Shared.Dtos;
using Admin.Shared.Dtos.mcq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Olympiads
{
    public interface IOlympiadRepository
    {
        Task<IEnumerable<MCQDto>> GetOlympiadMCQByQuizCode(string quizcode, string userId = null);
        Task<QuizStatusDto> GetQuizStatus(string quizcode, string userId = null);
        Task<List<AllOlympiadsDto>> GetAllPublicQuizes(string userid);
        Task<List<PendingOlympiadsDto>> GetPendingPublicQuizes(string userid);
        Task<List<QuizResultDto>> GetQuizResultByPaperId(string pastpaperId);
    }
}
