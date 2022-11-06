using Admin.Server.Repositories.Reports;
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
    public class McqReportsController: ControllerBase
    {
        private readonly IMcqReportRepository _db;
        public McqReportsController(IMcqReportRepository db)
        {
            _db = db;
        }

        // GET: api/Examinations
        [HttpGet]
        public async Task<IEnumerable<MCQReport>> GetAll()
        {
            return await _db.Get();
        }

        // GET: api/Examinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MCQReport>> GetReport(int id)
        {
            return await _db.Get(id);
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<MCQReport>> Puts(int id, MCQReport userReport)
        {

            return await _db.Update(id, userReport);
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MCQReport>> PostReport(MCQReport report)
        {
            var newReport = await _db.Create(report);

            return CreatedAtAction(nameof(GetReport), new { id = newReport.Id }, newReport);
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task DeleteReport(int id)
        {
            await _db.Delete(id);
        }
    }
}
