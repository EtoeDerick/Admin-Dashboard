using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.UserProgression
{
    public interface IUserProgressionRepository
    {
        Task<int> Get(int subjectId, string pastpaperId, int pastpaperNumber, int topicNumber, int questionPosition, string paperYear, string userid, int answerStatus = 3);
        Task<IEnumerable<Admin.Shared.Models.UserProgression>> GetUserProgressions();
        Task<QuizRankDto> GetQuizResponse(string pastpaperId, int score, string userid, string successfulSolutions);
        Task<List<Admin.Shared.Models.UserProgression>> GetQuizSubmitted(string pastpaperId);
    }    
}
