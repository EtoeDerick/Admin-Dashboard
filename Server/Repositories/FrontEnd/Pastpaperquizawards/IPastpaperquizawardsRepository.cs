using Admin.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Pastpaperquizawards
{
    public interface IPastpaperquizawardsRepository
    {
        Task<ActionResult<IEnumerable<Admin.Shared.Models.QuizAwardDto>>> GetAllQuizesAwarded();
        Task<ActionResult<QuizResultDto>> GetWinnerId(string pastpaperId);
        Task<ActionResult<PriceAwardPageDto>> GetWinnerInfo(string pastpaperId, string userid);
    }
}
