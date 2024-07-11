using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.DownloadPdf
{
    public class DownloadPdfRepository : ControllerBase, IDownloadPdfRepository
    {
        private readonly ApplicationDbContext _context;
        public DownloadPdfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Downloadpdf>> GetAll()
        {
            return await _context.Downloadpdfs.ToListAsync();
        }

        public async Task<Downloadpdf> Get(string id)
        {
            return await _context.Downloadpdfs.FindAsync(id);
        }
        public async Task<Downloadpdf> Create(Downloadpdf downloadpdf)
        {
            _context.Downloadpdfs.Add(downloadpdf);
            try
            {
                await _context.SaveChangesAsync();
            }catch(Exception e)
            {
                throw;
            }

            return downloadpdf;
        }

        public async Task<ActionResult> Delete(string id)
        {
            var download = await Get(id);
            if(download == null)
            {
                return NotFound();
            }

            _context.Downloadpdfs.Remove(download);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<ActionResult<Downloadpdf>> Update(string id, Downloadpdf downloadpdf)
        {
            if(id != downloadpdf.Id)
            {
                return BadRequest();
            }

            _context.Entry(downloadpdf).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateException ex)
            {
                Console.WriteLine("Error ", ex.Message);
                return NotFound();
            }

            return downloadpdf;
        }
    }
}
