using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class McqPastPaperFormDto
    {
        public string ExamTitle { get; set; }
        public string ExamId { get; set; }
        public List<McqSubjectFormDto> McqSubjects { get; set; }
    }

    public class McqSubjectFormDto
    {
        public string SubjectTitle { get; set; }
        public int SubjectId { get; set; }
    }
}
