using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Announcements
{
    public interface IAnnouncemntRepository
    {
        Task<IEnumerable<Admin.Shared.Models.Announcement>> Get();
        Task<Admin.Shared.Models.Announcement> Get(int id);
        Task<Admin.Shared.Models.Announcement> Create(Admin.Shared.Models.Announcement constants);
        Task<ActionResult<Admin.Shared.Models.Announcement>> Update(int id, Admin.Shared.Models.Announcement constants);
        Task<ActionResult> Delete(int id);
    }
}
