using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models.Tutorials
{
    public class Download
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string DownloadTitle { get; set; }
        public string DownloadUrl { get; set; }
        public bool IsPastPaper { get; set; }
        public string Year { get; set; }
        public int PastPaperNumber { get; set; }


        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
