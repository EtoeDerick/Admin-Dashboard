using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Instructors
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Admin.Shared.Models.Instructor>> Get();
        Task<Admin.Shared.Models.Instructor> Get(string id);
        Task<Admin.Shared.Models.Instructor> Create(Admin.Shared.Models.Instructor instructor);
        Task<ActionResult<Admin.Shared.Models.Instructor>> Update(string id, Admin.Shared.Models.Instructor instructor);
        Task<ActionResult> Delete(string id);
    }
}
