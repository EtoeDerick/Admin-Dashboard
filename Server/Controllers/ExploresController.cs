using Admin.Server.Repositories.FrontEnd.Explores;
using Admin.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExploresController : ControllerBase
    {
        private readonly IExploreRepository _db;
        public ExploresController(IExploreRepository exploreRepository)
        {
            _db = exploreRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ExamCategoryGroup>> GetAllExamsByCategory(string categoryType)
        {
            return await _db.GetAllExaminationsByCategory(categoryType);
        }
    }
}
