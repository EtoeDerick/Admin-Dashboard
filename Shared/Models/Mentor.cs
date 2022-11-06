using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Mentor
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string TelephoneLine1 { get; set; }

        [MaxLength(100)]
        public string TelephoneLine2 { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        [MaxLength(1024)]
        public string MentorBio { get; set; }

        //Navigation Property
        public virtual List<UserMentor> UserMentors { get; set; }
    }
}
