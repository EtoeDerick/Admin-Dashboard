using Admin.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.AdminDashboards
{
    public interface IAdminDashboardRepository
    {
        Task<DbCount> Get();
    }
}
