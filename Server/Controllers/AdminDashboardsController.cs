using Admin.Server.Repositories.AdminDashboards;
using Admin.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdminDashboardsController : ControllerBase
    {
        private readonly IAdminDashboardRepository _db;
        public AdminDashboardsController(IAdminDashboardRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public Task<DbCount> Get()
        {
            return  _db.Get();
            
        }
    }
}
