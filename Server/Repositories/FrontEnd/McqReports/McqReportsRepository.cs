using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.McqReports
{
    public class McqReportsRepository : ControllerBase, IMcqReportsRepository
    {
        private readonly ApplicationDbContext _context;

        public McqReportsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(MCQReport report)
        {
            _context.MCQReports.Add(report);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {                
                Console.WriteLine("Error", ex.Message);
            }

            return true;
        }
    }
}
