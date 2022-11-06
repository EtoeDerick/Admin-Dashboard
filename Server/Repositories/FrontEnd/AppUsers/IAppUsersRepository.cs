using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.AppUsers
{
    public interface IAppUsersRepository
    {
        Task<IEnumerable<AppUser>> Get();
        Task<AppUser> Get(string id);
        Task<AppUser> Create(AppUser user);
        Task<ActionResult<AppUser>> Update(string id, AppUser user);
        Task<ActionResult> Delete(string id);
    }

    
}
