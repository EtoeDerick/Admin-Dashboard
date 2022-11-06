using Admin.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Admin.Server.Repositories.Announcements
{
    public class AnnouncementRepository : ControllerBase, IAnnouncemntRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin.Shared.Models.Announcement>> Get()
        {
            var announcements = await _context.Announcements.ToListAsync();


            return announcements;
        }

        public async Task<Admin.Shared.Models.Announcement> Get(int id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        public async Task<ActionResult<Admin.Shared.Models.Announcement>> Update(int id, Admin.Shared.Models.Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return BadRequest();
            }



            _context.Entry(announcement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementsExists(announcement.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return announcement;
        }
        public async Task<Admin.Shared.Models.Announcement> Create(Admin.Shared.Models.Announcement announcement)
        {

            _context.Announcements.Add(announcement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (AnnouncementsExists(announcement.Id))
                {

                    throw;
                }
                Console.WriteLine("Error", ex.Message);
                return null;
            }

            return announcement;
        }

        public async Task<ActionResult> Delete(int id)
        {
            var constantsToDelete = await Get(id);

            if (constantsToDelete == null)
            {
                return NotFound();
            }

            //2. Delete the actual past paper record
            _context.Announcements.Remove(constantsToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AnnouncementsExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }


    }
}
