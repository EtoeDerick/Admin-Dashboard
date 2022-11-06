using Admin.Server.Data;
using Admin.Shared.Models;
using Admin.Shared.Models.Tutorials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Objectives
{
    public class ObjectivesRepository : ControllerBase, IObjectivesRepository
    {
        private readonly ApplicationDbContext _context;

        public ObjectivesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Objective>> Get()
        {
            var objectives = await _context.Objectives.ToListAsync();


            return objectives;
        }

        public async Task<Objective> Get(int id)
        {
            return await _context.Objectives.FindAsync(id);
        }

        public async Task<ActionResult<Objective>> Update(int id, Objective objective)
        {
            if (id != objective.Id)
            {
                return BadRequest();
            }

            
            
            _context.Entry(objective).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(objective.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return objective;
        }
        public async Task<Objective> Create(Objective objective)
        {
            _context.Objectives.Add(objective);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (Exists(objective.Id))
                {
                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return objective;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var constantsToDelete = await Get(id);

            if (constantsToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.Objectives.Remove(constantsToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool Exists(int id)
        {
            return _context.Objectives.Any(e => e.Id == id);
        }

        
    }
}
