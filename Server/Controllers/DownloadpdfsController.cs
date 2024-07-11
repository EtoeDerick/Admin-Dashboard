using Admin.Server.Repositories.DownloadPdf;
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
    public class DownloadpdfsController : ControllerBase
    {
        private readonly IDownloadPdfRepository _db;
        public DownloadpdfsController(IDownloadPdfRepository db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IEnumerable<Downloadpdf>> GetAll()
        {
            return await _db.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Downloadpdf> Get(string id)
        {
            return await _db.Get(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Downloadpdf>> Puts(string id, Downloadpdf downloadpdf)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _db.Update(id, downloadpdf);
        }

        [HttpPost]
        public async Task<ActionResult<Downloadpdf>> PostAnnouncement(Downloadpdf downloadpdf)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var downloadpdf1 = await _db.Create(downloadpdf);

            return CreatedAtAction(nameof(Get), new { id = downloadpdf1.Id }, downloadpdf1);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAnnouncement(string id)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            await _db.Delete(id);
        }
    }
}
