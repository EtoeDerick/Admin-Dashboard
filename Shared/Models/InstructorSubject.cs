using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class InstructorSubject
    {
        public int Commission { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; } = true;
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = new Subject();
        public string InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
