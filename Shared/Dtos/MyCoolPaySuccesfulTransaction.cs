using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class MyCoolPaySuccesfulTransaction
    {
        public string status { get; set; }
        public string app_transaction_ref { get; set; }
        public string operator_transaction_ref { get; set; }
        public string transaction_ref { get; set; }
        public string transaction_type { get; set; }
        public string transaction_amount { get; set; }
        public string transaction_fees { get; set; }
        public string transaction_currency { get; set; }
        public string transaction_operator { get; set; }
        public string transaction_status { get; set; }
        public string transaction_reason { get; set; }
        public string transaction_message { get; set; }
        public string customer_phone_number { get; set; }
    }
}
