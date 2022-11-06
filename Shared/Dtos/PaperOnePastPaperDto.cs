using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class PaperOnePastPaperDto
    {
        public string Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public int CorrectAnsweredCount { get; set; }
        public int WrongAnswerCount { get; set; }
        public long DownloadSize { get; set; } //in KB
        public bool IsRed { get; set; } //Transparent: Still to view, Red: Not any answered, Yellow: Partially answered, Green: All questions answered correctly
        public bool IsYellow { get; set; }
        public bool IsGreen { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public bool IsNotDownloaded { get; set; }
        public bool IsDownloaded { get; set; }
    }
}
