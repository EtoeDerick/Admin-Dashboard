using Admin.Server.Repositories.GcePastPaperPdfs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Shared.Dtos;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GcepastpaperspdfdownloadsController : ControllerBase
    {
        private readonly IGcePastPaperPdfsRepository _db;
        public GcepastpaperspdfdownloadsController(IGcePastPaperPdfsRepository db)
        {
            _db = db;
        }

        //SubjectId: id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PdfDownloadDtoGroupedByYear>>> GetAll(int id)
        {
            return await _db.Get(id);
        }
    }
}
