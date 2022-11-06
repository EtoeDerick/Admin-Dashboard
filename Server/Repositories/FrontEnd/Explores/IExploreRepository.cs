using Admin.Shared.Dtos;
using Admin.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Explores
{
    public interface IExploreRepository
    {
        Task<IEnumerable<ExamCategoryGroup>> GetAllExaminationsByCategory(string categoryType);
    }
}
