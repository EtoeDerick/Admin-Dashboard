using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class UserSubjectAudit
    {
        public int Id { get; set; }
        public bool IsExpired { get; set; } = false;
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;

        [MaxLength(10)]
        public string Status { get; set; }
        public decimal Price { get; set; }

        public int SubjectId { get; set; }

        public string ApplicationUserId { get; set; }
        public DateTime AuditDate { get; set; } = DateTime.Now;
    }
}
