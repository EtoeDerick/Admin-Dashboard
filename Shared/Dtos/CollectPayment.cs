using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class CollectPayment
    {
        public string amount { get; set; }
        public int durationInDays { get; set; }
        public string currency { get; set; }
        public string from { get; set; }
        public string description { get; set; }
        public string external_reference { get; set; }
        public string external_user { get; set; }
    }
}
