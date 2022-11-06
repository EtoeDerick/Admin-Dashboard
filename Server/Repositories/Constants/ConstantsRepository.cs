using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Constants
{
    public class ConstantsRepository : ControllerBase, IConstantsRepository
    {
        private readonly ApplicationDbContext _context;

        public ConstantsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin.Shared.Models.Constants>> Get()
        {
            var constants = await _context.Constants.ToListAsync();


            return constants;
        }

        public async Task<Admin.Shared.Models.Constants> Get(int id)
        {
            return await _context.Constants.FindAsync(id);
        }

        public async Task<ActionResult<Admin.Shared.Models.Constants>> Update(int id, Admin.Shared.Models.Constants constants)
        {
            if (id != constants.Id)
            {
                return BadRequest();
            }

            _context.Entry(constants).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConstantsExists(constants.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return constants;
        }
        public async Task<Admin.Shared.Models.Constants> Create(Admin.Shared.Models.Constants constants)
        {

            _context.Constants.Add(constants);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ConstantsExists(constants.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return constants;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var constantsToDelete = await Get(id);

            if (constantsToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.Constants.Remove(constantsToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ConstantsExists(int id)
        {
            return _context.Constants.Any(e => e.Id == id);
        }

        
    }
}
