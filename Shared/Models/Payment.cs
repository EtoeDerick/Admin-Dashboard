using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Amount { get; set; }   //Amount the user is to pay

        public int DurationInDays { get; set; }

        [MaxLength(250)]
        public string Currency { get; set; }  //XAF

        [MaxLength(250)]
        public string From { get; set; }      //User phone number

        [MaxLength(1024)]
        public string Description { get; set; }  //Information describing the object of payment: Subject

        [MaxLength(250)]
        public string External_reference { get; set; }  // GUID for unique payment issued

        [MaxLength(250)]
        public string UserId { get; set; }  //ID of USER issuing payment

        //Value of Content Paid
        [MaxLength(250)]
        public string SubjectId { get; set; }

        [MaxLength(250)]
        public string Status { get; set; } //PENDING, PAID or SUCCESS

        [MaxLength(250)]
        public string TransactionStatus { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        //Reference Payment Information
        [MaxLength(250)]
        public string Reference { get; set; } //Code sent by Campay indicating paymentReferenceId

        [MaxLength(250)]
        public string Ussd_code { get; set; } // *126# for MoMo and #150*50# for Orange Money

        [MaxLength(250)]
        public string Operator { get; set; } // MTN or Orange
        [MaxLength(250)]
        public string Action { get; set; }

    }
}
