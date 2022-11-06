using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Reports
{
    public class McqReportRepository : ControllerBase, IMcqReportRepository
    {
        private readonly ApplicationDbContext _context;

        public McqReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MCQReport>> Get()
        {
            var constants = await _context.MCQReports.ToListAsync();


            return constants;
        }

        public async Task<MCQReport> Get(int id)
        {
            return await _context.MCQReports.FindAsync(id);
        }

        public async Task<ActionResult<MCQReport>> Update(int id, MCQReport userreport)
        {
            if (id != userreport.Id)
            {
                return BadRequest();
            }

            
            
            _context.Entry(userreport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConstantsExists(userreport.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return userreport;
        }
        public async Task<MCQReport> Create(MCQReport userreport)
        {

            _context.MCQReports.Add(userreport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ConstantsExists(userreport.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return userreport;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var reportToDelete = await Get(id);

            if (reportToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.MCQReports.Remove(reportToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ConstantsExists(int id)
        {
            return _context.Topic.Any(e => e.Id == id);
        }

        
    }
}
