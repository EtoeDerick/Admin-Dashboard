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
        public async Task<IEnumerable<Payment>> GetTransactions(string userId=null)
        {
            
            var _userId = userId;

            if (string.IsNullOrEmpty(userId))
            {
                //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    _userId = claim.Value;
                }
            }
            else
            {
                _userId = userId;
            }
            return await _db.GetTransactions(_userId);
        }


        // GET api/<PaymentsController>/5
        //id = transaction_ref
        [HttpGet("{id}")]
        public async Task<MyCoolPaySuccesfulTransaction> GetTransationStatus(string id, string userId = null)
        {
            var _userId = "";
            if (string.IsNullOrEmpty(userId))
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    _userId = claim.Value;
                }
            }
            else
            {
                _userId = userId;
            }
                       

            return await _db.GetTransactionStatus(id, _userId);            
        }


        // PUT api/<PaymentsController>/5
        //id = subjectId
        [HttpPost("{id}")]
        public async Task<ActionResult<TransactionReference>> PostPayment(int id, int price, int duration, string phone, string userId=null)
        {
            var _userId = "";
            if (string.IsNullOrEmpty(userId))
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim != null)
                {
                    _userId = claim.Value;
                }
            }
            else
            {
                _userId = userId;
            }            

            var payment = new CollectPayment()
            {
                external_user = id.ToString(),
                amount = price.ToString(),
                durationInDays = duration,
                from = phone
            };

            return await _db.PostPayment(payment, _userId, id);

            //var url = _context.Constants.Single(c => c.Key == "externalpaymentbaseUrl").Value + "payin";

            
        }

    }
}
