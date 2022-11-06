using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class SubjectEnrollmentAndPaymentDto
    {
        public int Duration { get; set; }
        public int Price { get; set; }
        public int SubjectId { get; set; }
        public string Status { get; set; }
    }
}
