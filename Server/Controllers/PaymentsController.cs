using Admin.Server.Repositories.FrontEnd.Payments;
using Admin.Shared.Dtos;
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
    public class PaymentsController : ControllerBase
    {
        IPaymentsRepository _db;
        public PaymentsController(IPaymentsRepository db)
        {
            _db = db;
        }

        // GET: api/<PaymentsController>
        [HttpGet]
        public async Task<IEnumerable<Payment>> GetTransactions()
        {
            
            var _userId = "user1";

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                _userId = claim.Value;
            }



            return await _db.GetTransactions(_userId);
        }


        // GET api/<PaymentsController>/5
        [HttpGet("{id}")]
        public async Task<MyCoolPaySuccesfulTransaction> GetTransationStatus(string id)
        {
            var userId = "user1";
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userId = claim.Value;
            }           

            return await _db.GetTransactionStatus(id, userId);            
        }


        // PUT api/<PaymentsController>/5
        //id = subjectId
        [HttpPost("{id}")]
        public async Task<ActionResult<TransactionReference>> PostPayment(int id, int price, int duration, string phone)
        {
            var userId = "user1";
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim != null)
            {
                userId = claim.Value;
            }

            var payment = new CollectPayment()
            {
                external_user = id.ToString(),
                amount = price.ToString(),
                durationInDays = duration,
                from = phone
            };

            return await _db.PostPayment(payment, userId, id);

            //var url = _context.Constants.Single(c => c.Key == "externalpaymentbaseUrl").Value + "payin";

            
        }

    }
}
