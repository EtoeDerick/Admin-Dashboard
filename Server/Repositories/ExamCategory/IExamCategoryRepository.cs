using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.ExamCategory
{
    public interface IExamCategoryRepository
    {
        Task<IEnumerable<Admin.Shared.Models.ExamCategory>> Get();
        Task<Admin.Shared.Models.ExamCategory> Get(int id);
        Task<Admin.Shared.Models.ExamCategory> Create(Admin.Shared.Models.ExamCategory constants);
        Task<ActionResult<Admin.Shared.Models.ExamCategory>> Update(int id, Admin.Shared.Models.ExamCategory examCategory);
        Task<ActionResult> Delete(int id);
    }
}
