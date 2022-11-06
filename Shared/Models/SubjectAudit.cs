using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class SubjectAudit
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [MaxLength(255)]
        public string MarqueeImageUrl { get; set; }
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Year { get; set; }

        [MaxLength(20)]
        public string SubjectExamNickName { get; set; } = "GCE";

        [MaxLength(20)]
        public string Category { get; set; }

        public bool IsFree { get; set; } = true;

        public bool IsApproved { get; set; } = true;

        public DateTime AuditDate { get; set; } = DateTime.Now;
    }
}
