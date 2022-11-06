using Admin.Server.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.Address
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Shared.Models.Address> Create(Shared.Models.Address address)
        {
            _context.Addresses.Add(address);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                
                Console.WriteLine("Error", ex.Message);
                return null;
            }

            return address;
        }

        public async Task<IEnumerable<Shared.Models.Address>> Get()
        {
            return await _context.Addresses.ToListAsync();
        }
    }
}
