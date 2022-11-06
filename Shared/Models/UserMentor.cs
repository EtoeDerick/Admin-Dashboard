using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class UserMentor
    {
        public DateTime DateAssigned { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }

    }
}
