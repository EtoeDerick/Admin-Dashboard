using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.QuizAwards
{
    public interface IQuizAwardsRepository
    {
        Task<List<Admin.Shared.Dtos.AwardComment>> Create(string message, string userid);
        Task<ActionResult<List<AwardComment>>> GetAllApprovedQuizAwardsConversations();
        Task<ActionResult<QuizAward>> CreateQuizAward(QuizAward quizAward);
        Task<ActionResult<QuizAward>> GetQuizAwardWithPastPaperId(string pastpaperId);
        Task<ActionResult<QuizAward>> UpdateQuizAwardWithPastPaperId(string pastpaperId, Admin.Shared.Models.QuizAward quizAward);
        Task<ActionResult<int>> Delete(int id);
        Task<ActionResult<List<QuizAward>>> GetAllQuizesAwarded();
    }
}
