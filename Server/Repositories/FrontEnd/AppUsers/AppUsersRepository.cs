using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.AppUsers
{
    public class AppUsersRepository : ControllerBase, IAppUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public AppUsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> Get()
        {
            var users = await _context.AppUsers.ToListAsync();
            return users;
        }

        public async Task<AppUser> Get(string id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public async Task<ActionResult<AppUser>> Update(string id, AppUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return user;
        }
        public async Task<AppUser> Create(AppUser user)
        {
            _context.AppUsers.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (UserExists(user.Id))
                {                 
                    throw;
                }
                Console.WriteLine("Error", ex.Message);
            }

            return user;
        }

        public async Task<ActionResult> Delete(string id)
        {
            var userToDelete = await Get(id);
            if (userToDelete == null)
            {
                return NotFound();
            }
            //2. Delete the actual past paper record
            _context.AppUsers.Remove(userToDelete);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool UserExists(string id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }

        
    }
}
