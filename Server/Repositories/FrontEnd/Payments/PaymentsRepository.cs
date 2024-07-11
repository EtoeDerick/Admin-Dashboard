using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Payments
{
    public class PaymentsRepository : ControllerBase, IPaymentsRepository
    {
        private readonly ApplicationDbContext _context;
        private HttpClient client;
        public PaymentsRepository(ApplicationDbContext context, HttpClient client)
        {
            _context = context;
            this.client = client;
        }

        //Returns a list of successful payments
        //=> Failed or pending payments are not returned

        public async Task<IEnumerable<Payment>> GetTransactions(string _userId)
        {
            var transactionList = new List<Payment>();

            var pendingPayments = await _context.Payments.Where(p => p.UserId == _userId  /*|| (p.UserId == _userId && p.TransactionStatus.ToLower() == "success")*/).ToListAsync();

            foreach (var p in pendingPayments)
            {
                //1. PENDING STATUS
                if (p.TransactionStatus.ToLower() == "pending" || p.TransactionStatus.ToLower() == "pending...")
                {
                    //Get the transaction from MyCoolPay
                    var myCoolPayTransaction = await GetExternalPaymentTransaction(p.Reference);

                    if (myCoolPayTransaction.transaction_status == "FAILED")
                    {
                        p.TransactionStatus = "FAILED";
                        p.Action = "FAILED";
                        p.Description = "Subscription for " + _context.Subjects.Single(s => s.Id.ToString() == p.SubjectId).Title + " failed!!";
                        //Update Payment with Failed Transaction Status

                        //Only return to the user, the list of successful payments made 
                        //This display is done ONCE per PAYMENT
                        transactionList.Add(p);

                        await DeletePayment(p.Id);

                        //await UpdateOgaBooksPayment(p);
                    }
                    else if (myCoolPayTransaction.transaction_status == "SUCCESS")
                    {
                        //Get Instance of this userSubject and Update
                        var us = _context.UserSubjects.Single(u => u.SubjectId.ToString() == p.SubjectId && u.AppUserId == _userId);

                        us.AppUser = null;
                        us.Subject = null;
                        us.EnrollmentDate = DateTime.Now;
                        us.ExpiryDate = DateTime.Now.AddDays(p.DurationInDays);
                        us.Duration = p.DurationInDays;
                        us.IsExpired = false;
                        us.Status = "PAID";
                        //us.Price = (decimal)Int32.Parse(p.Amount);
                        //us.Price = _context.Subjects.Single(s => s.Id == us.SubjectId).Price;

                        //await updateUserSubject(us);

                        await UpdateUserSubject(us);

                        //Add updated record to the Audit Table
                        var usaudit = MapToUserSubjectAudit(us);
                        await CreateUserSubjectAuditRecord(usaudit);

                        //Change Description to subjectTitle before displaying to output
                        p.Description = _context.Subjects.Single(s => s.Id.ToString() == p.SubjectId).Title;


                        //We copy successful payments in our database and notify the user of the status of payment
                        //Else we no notification
                        p.TransactionStatus = "SUCCESS";
                        p.Action = "SUCCESS";

                        //Only return to the user, the list of successful payments made 
                        //This display is done ONCE per PAYMENT
                        transactionList.Add(p);

                        //Update Payment with SUCCESS Transaction Status
                        //await UpdateOgaBooksPayment(p);
                        await DeletePayment(p.Id);
                    }
                    else if (myCoolPayTransaction.transaction_status.ToLower() != "pending")
                    {
                        p.TransactionStatus = myCoolPayTransaction.transaction_status;
                        p.Action = myCoolPayTransaction.transaction_status; ;
                        //Update Payment with Failed Transaction Status
                        //await UpdateOgaBooksPayment(p);
                    }

                }
                else
                {
                    await DeletePayment(p.Id);
                }

                
            }

            return transactionList;
        }
                
        //Get the status of a transaction using subjectId and userId
        public async Task<MyCoolPaySuccesfulTransaction> GetTransactionStatus(string id, string userId)
        {
            //Get status of transaction using transaction_ref: id
            var transact = await GetExternalPaymentTransaction(id);

            var _userId = userId;
            //var ogaBooksTransact = await _context.Payments.Where(p => p.UserId == _userId && p.Reference == transact.transaction_ref).ToListAsync();

            if(_context.Payments.Any(p => p.UserId == _userId && p.Reference == transact.transaction_ref))
            {
                var t = _context.Payments.Single(p => p.UserId == _userId && p.Reference == transact.transaction_ref);

                if (transact.transaction_status != "PENDING")
                {

                    //Update Payment Table
                    //Update UserSubject where Status == "SUCCESS"
                    if (transact.transaction_status == "SUCCESS")
                    {
                        var us = _context.UserSubjects.Single(u => u.AppUserId == _userId && u.SubjectId.ToString() == t.SubjectId);
                        int price = 0;
                        us.EnrollmentDate = DateTime.UtcNow.AddHours(1);
                        //us.ExpiryDate = DateTime.UtcNow.AddHours(1).AddMonths(5);
                        us.IsExpired = false;
                        us.Status = "PAID";
                        us.Duration = t.DurationInDays;
                        us.ExpiryDate = us.EnrollmentDate.AddDays(t.DurationInDays);
                        Int32.TryParse(t.Amount, out price);
                        us.Price = price;
                        us.IsExpired = false;
                        await updateUserSubject(us);
                        
                        //Add updated record to the Audit Table
                        var usaudit = MapToUserSubjectAudit(us);
                        await CreateUserSubjectAuditRecord(usaudit);

                    }
                    t.TransactionStatus = transact.transaction_status;
                    t.Action = transact.transaction_status;

                    await UpdateOgaBooksPayment(t);
                }
            }

            return transact;
        }

        public async Task<ActionResult<TransactionReference>> PostPayment(CollectPayment payment, string userId, int id)
        {
            var url = "https://my-coolpay.com/api/23610c69-d81e-4389-a5ca-89e227774ccc/payin";

            var req = new HttpRequestMessage(HttpMethod.Post, url);

            var external_ref = Guid.NewGuid().ToString();

            //Extract SubjectId before overriding the content of external_user
            var subjectId = payment.external_user;
            var subject = _context.Subjects.Single(s => s.Id == id);

            //Check if user is enrolled and enroll the use automatically....
            if(!_context.UserSubjects.Any(u => u.AppUserId == userId && u.SubjectId == id))
            {//User hasn't enroll in the subject.
                var userSubject = new UserSubject()
                {
                    IsExpired = true,
                    IsDeleted = false,
                    EnrollmentDate = DateTime.Now,
                    ExpiryDate = DateTime.Now,
                    Status = "Initiated",
                    Duration = 0,
                    SubjectId = id,
                    AppUserId = userId
                };
                _context.UserSubjects.Add(userSubject);
                
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
            else
            {
                var usrSubj = _context.UserSubjects.Single(u => u.AppUserId == userId && u.SubjectId == id);
                if (usrSubj.IsDeleted)
                {
                    usrSubj.IsDeleted = false;
                    await updateUserSubject(usrSubj);
                }
            }

            payment.description = "OgaBook - " + payment.durationInDays.ToString() + " days subscription-" + subject.Title;
            payment.external_user = userId;
            payment.external_reference = external_ref;

            req.Content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {"transaction_amount", payment.amount },
                    {"transaction_currency", "XAF" },
                    {"customer_phone_number", payment.from },
                    {"transaction_reason", payment.description },
                    {"app_transaction_ref", payment.external_reference },
                    {"customer_name", payment.external_user },
                    {"customer_lang", "en" },
                });

            //Sending payment to myCoolPay

            var response = await client.SendAsync(req);

            response.EnsureSuccessStatusCode();

            var transaction_reference = new TransactionReference();

            var paymentObj = new Payment
            {
                Amount = payment.amount,
                DurationInDays = payment.durationInDays,
                Currency = payment.currency,
                From = payment.from,
                Description = payment.description,
                External_reference = payment.external_reference,
                Reference = payment.external_reference,
                UserId = userId,

                PaymentDate = DateTime.UtcNow.AddHours(1),
                SubjectId = subjectId,
                Status = "FAILED"
            };

            if (response.IsSuccessStatusCode)
            {
                paymentObj.Status = "success";
                //Successful REQUEST IS SENT TO CAMPAY
                if (response.Content is object)
                {
                    var contentStream = await response.Content.ReadAsStringAsync();
                    transaction_reference = JsonConvert.DeserializeObject<TransactionReference>(contentStream);

                    //paymentObj.Reference = transaction_reference.transaction_reference;
                    paymentObj.Ussd_code = transaction_reference.ussd;
                    paymentObj.Action = transaction_reference.action;
                    paymentObj.TransactionStatus = "PENDING";
                    paymentObj.Status = transaction_reference.status;
                    paymentObj.Reference = transaction_reference.transaction_ref;
                }

            }

            _context.Payments.Add(paymentObj);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaymentExists(paymentObj.Id))
                {
                    return Conflict();
                }
            }


            return transaction_reference;
        }

        private async Task<MyCoolPaySuccesfulTransaction> GetExternalPaymentTransaction(string transaction_ref)
        {
            //This method allows us to get any transaction from My-CoolPay using the transaction_reference
            var transact = new MyCoolPaySuccesfulTransaction();
            var url = "https://my-coolpay.com/api/23610c69-d81e-4389-a5ca-89e227774ccc/checkStatus/" + transaction_ref;

            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("Accept", "*/*");
            req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            req.Headers.Add("Connection", "keep-alive");


            var response = await client.SendAsync(req);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                if (response.Content is object)
                {
                    var contentStream = await response.Content.ReadAsStringAsync();
                    transact = JsonConvert.DeserializeObject<MyCoolPaySuccesfulTransaction>(contentStream);
                }
            }

            if (transact.transaction_status == "SUCCESS")
            {
                //update the content of usersubject
            }

            return transact;
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }

        private async Task updateUserSubject(UserSubject us)
        {
            us.Subject = null;
            us.AppUser = null;

            _context.Entry(us).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
        }
        public async Task<IActionResult> UpdateUserSubject(UserSubject userSubject)
        {
           
            _context.Entry(userSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }
       
        private async Task CreateUserSubjectAuditRecord(UserSubjectAudit us)
        {
            us.AuditDate = DateTime.Now;

            _context.UserSubjectAudits.Add(us);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
        }

        private async Task RemoveSubjectSubscription(UserSubject us, string amount)
        {
            us.Subject = null;
            us.AppUser = null;

            us.ExpiryDate = DateTime.Now.AddMonths(0);
            us.IsExpired = true;
            us.Status = "Expired";

            var usaudit = MapToUserSubjectAudit(us);

            _context.Entry(us).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            await CreateUserSubjectAuditRecord(usaudit);
        }

        private async Task<bool> UpdateOgaBooksPayment(Payment ogaBooksTrans)
        {

            _context.Entry(ogaBooksTrans).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (PaymentExists(ogaBooksTrans.Id))
                {
                    return false;
                }
                return false;
            }

            return true;
        }

        private UserSubjectAudit MapToUserSubjectAudit(UserSubject us)
        {
            var usaudit = new UserSubjectAudit
            {
                IsExpired = us.IsExpired,
                EnrollmentDate = us.EnrollmentDate,
                ExpiryDate = us.ExpiryDate,
                Status = us.Status,
                Price = us.Price,
                SubjectId = us.SubjectId,
                ApplicationUserId = us.AppUserId,
                AuditDate = DateTime.Now
            };

            return usaudit;
        }

        public async Task<IActionResult> DeletePayment(int id)
        {
            var product = await _context.Payments.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

