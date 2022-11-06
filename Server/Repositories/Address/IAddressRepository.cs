using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Address
{
    public interface IAddressRepository
    {
        Task<Admin.Shared.Models.Address> Create(Admin.Shared.Models.Address address);
        Task<IEnumerable<Shared.Models.Address>> Get();
    }
}
