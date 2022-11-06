using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class UserSubject
    {
        public bool IsExpired { get; set; } = true;
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;

        [MaxLength(10)]
        public string Status { get; set; } = "Initiated";
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Duration { get; set; }

        //To track activity of user
        public DateTime LastUsedOn { get; set; } = DateTime.Now;
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = new Subject();

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
