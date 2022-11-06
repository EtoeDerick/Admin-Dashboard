using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class TransactionReference
    {
        public int Id { get; set; }
        public string status { get; set; }
        public string transaction_ref { get; set; }
        public string action { get; set; }
        public string ussd { get; set; }
    }
}
