using Admin.Server.Repositories.FrontEnd.Quizes;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PastPapersQuizesController: ControllerBase
    {
        private readonly IPastPapersQuizesRepository _db;
        public PastPapersQuizesController(IPastPapersQuizesRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<PastPaper>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/PastPapersQuizes/595_0_23
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizResultsDto>> GetPastPaper(string id)
        {
            /*var userId = "";
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userId = claim.Value;
            }*/
            return await _db.Get(id /*, userId*/);
        }
    }
}
