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

            var pendingPayments = await _context.Payments.Where(p => (p.UserId == _userId && p.TransactionStatus.ToLower() == "pending") || (p.UserId == _userId && p.TransactionStatus.ToLower() == "success")).ToListAsync();

            if (pendingPayments.Count > 0)
            {
                

                foreach (var p in pendingPayments)
                {
                    //1. PENDING STATUS
                    if (p.TransactionStatus.ToLower() == "pending")
                    {
                        //Get the transaction from MyCoolPay
                        var myCoolPayTransaction = await GetExternalPaymentTransaction(p.Reference);

                        if (myCoolPayTransaction.transaction_status == "FAILED")
                        {
                            p.TransactionStatus = "FAILED";
                            p.Action = "FAILED";
                            //Update Payment with Failed Transaction Status
                            await UpdateOgaBooksPayment(p);
                        }
                        else if (myCoolPayTransaction.transaction_status == "SUCCESS")
                        {
                            //We copy successful payments in our database and notify the user of the status of payment
                            //Else we no notification
                            p.TransactionStatus = "SUCCESS";
                            p.Action = "SUCCESS";
                            //Update Payment with Failed Transaction Status
                            await UpdateOgaBooksPayment(p);

                            //Get Instance of this userSubject and Update
                            //var us = _context.UserSubjects.Single(us => us.SubjectId.ToString() == p.SubjectId && us.ApplicationUserId == _userId);
                            var us = await _context.UserSubjects.FirstOrDefaultAsync(us => us.SubjectId.ToString() == p.SubjectId && us.AppUserId == _userId);
                            us.AppUser = null;
                            us.Subject = null;
                            us.EnrollmentDate = DateTime.Now;
                            us.ExpiryDate = DateTime.Now.AddDays(p.DurationInDays);
                            us.Duration = p.DurationInDays;
                            us.IsExpired = false;
                            us.Status = "PAID";
                            us.Price = _context.Subjects.Single(s => s.Id == us.SubjectId).Price;

                            await updateUserSubject(us);

                            //Add updated record to the Audit Table
                            var usaudit = MapToUserSubjectAudit(us);
                            await CreateUserSubjectAuditRecord(usaudit);

                            //Change Description to subjectTitle before displaying to output
                            p.Description = _context.Subjects.Single(s => s.Id.ToString() == p.SubjectId).Title;

                            //Only return to the user, the list of successful payments made 
                            //This display is done ONCE per PAYMENT
                            transactionList.Add(p);
                        }
                        else if (myCoolPayTransaction.transaction_status.ToLower() != "pending")
                        {
                            p.TransactionStatus = myCoolPayTransaction.transaction_status;
                            p.Action = myCoolPayTransaction.transaction_status; ;
                            //Update Payment with Failed Transaction Status
                            await UpdateOgaBooksPayment(p);
                        }



                    }
                    else if (p.TransactionStatus.ToLower() == "success")
                    {
                        var subjectId = 0;
                        Int32.TryParse(p.SubjectId, out subjectId);
                        var us = await _context.UserSubjects.FirstOrDefaultAsync(us => us.SubjectId == subjectId && us.AppUserId == _userId);



                        //Verify the user has been granted access to the subject
                        //Check Whether the user has recived notification and number of notification counts

                        //Verify in the later version the validity of payment
                    }
                }
            }

            //CHECK IF USER IS HAS BEEN BOUNCED OUT BY ANOTHER USER/DEVICE

            //await LoginFraudDetector();

            //CHECK IF USER IS HAS BEEN BOUNCED OUT BY ANOTHER USER/DEVICE

            return transactionList;
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
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

            return transact;
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

        private async Task<ActionResult<bool>> LoginFraudDetector()
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = _context.Users.Single(u => u.Id == _userId).Email;

            //var userId = User.FindFirstValue(ClaimTypes.Name);
            //var sid = HttpContext.Session.GetString("SessionId");

            //if (sid == null)
            //{
            //    sid = HttpContext.Session.Id;
            //    HttpContext.Session.SetString("SessionId", HttpContext.Session.Id);
            //}

            //var login = new LoginCheckContext(_context);

            //var shouldIExit = await login.CanIStillLogin(userId, sid);

            //if (!shouldIExit)
            //{
            //    int suspensionDuration = 50;
            //    string durationOfSuspension = string.Empty;
            //    //var constant = await context.Constants.Where(co);
            //    // Pull Value of Constant for Duration of the suspension
            //    try
            //    {
            //        durationOfSuspension = _context.Constants.Single(c => c.Key.ToLower() == "suspensionduration").Value;
            //    }
            //    catch (Exception)
            //    {

            //    }

            //    Int32.TryParse(durationOfSuspension, out suspensionDuration);

            //    var suspendedAct = new SuspendedAccount
            //    {
            //        IsSuspended = true,
            //        Date = DateTime.Now,
            //        DurationInMinutes = suspensionDuration,
            //        UserId = userId
            //    };

            //    _context.SuspendedAccounts.Add(suspendedAct);
            //    await _context.SaveChangesAsync();

            //    //I have been logout by another Login Session
            //    //Now sign me out
            //    await _signInManager.SignOutAsync();
            //    return RedirectToAction("Login", "Account");
            //}

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

        public async Task<MyCoolPaySuccesfulTransaction> GetTransactionStatus(string id, string userId)
        {
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

                        us.EnrollmentDate = DateTime.Now;
                        us.ExpiryDate = DateTime.Now.AddMonths(5);
                        us.IsExpired = false;
                        us.Status = "PAID";
                        us.Price = _context.Subjects.Single(s => s.Id == us.SubjectId).Price;

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

            //if (subject.MonthlyPrice.ToString() == payment.amount && payment.durationInDays != 30)
            //    return null;

            //if (subject.Price.ToString() == payment.amount && payment.durationInDays != 90)
            //    return null;

            //if (subject.MonthlyPrice.ToString() != payment.amount && subject.Price.ToString() != payment.amount)
            //    return null;

            payment.description = "OgaBooks - " + subject.Title + " : for a duration of " + payment.durationInDays.ToString() + " days;";
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

                PaymentDate = DateTime.Now.ToLocalTime(),
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
    }
}

