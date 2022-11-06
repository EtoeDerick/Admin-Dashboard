using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Instructor
    {

        public string Id { get; set; }

        [MaxLength(80), Required]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(1024)]
        public string ImageUrl { get; set; }

        [MaxLength(10)]
        public string DiscountCode { get; set; }

        //Navigation Properties

        public virtual List<InstructorSubject> InstructorSubjects { get; set; } = new List<InstructorSubject>();
    }
}
