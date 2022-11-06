using Admin.Server.Repositories.Address;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _db;

        public AddressesController(IAddressRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await _db.Get();
        }

        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(Address address)
        {
            var userid = string.Empty;

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userid = claim.Value;
            }

            address.UserId = userid;

            var newAddress = await _db.Create(address);
            return newAddress;
            //return CreatedAtAction(nameof(GetAnnouncement), new { id = newAnnouncement.Id }, newAnnouncement);
        }
    }
}
