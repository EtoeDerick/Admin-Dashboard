using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        public int Credit { get; set; }

        //Navigation Property
        public virtual List<UserSubject> UserSubjects { get; set; }
        public virtual List<UserMentor> UserMentors { get; set; }
    }
}
