using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Payments
{
    public interface IPaymentsRepository
    {
        Task<IEnumerable<Payment>> GetTransactions(string _userId);
        Task<MyCoolPaySuccesfulTransaction> GetTransactionStatus(string id, string userId);
        Task<ActionResult<TransactionReference>> PostPayment(CollectPayment payment, string userId, int id);
    }  

}
