using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.UserQuiz
{
    public interface IUserQuizRepository
    {
        Task<IEnumerable<Admin.Shared.Models.UserQuiz>> GetUserQuizzes();
        Task Create(string pastpaperId, string userid, string username, int score);
        
    }
}
