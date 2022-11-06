using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class EnrollmentSubjectDto
    {
        public int SubjectId { get; set; }
        public string UserId { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
