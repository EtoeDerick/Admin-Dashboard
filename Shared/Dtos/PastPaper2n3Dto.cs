using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class PastPaper2n3Dto
    {
        public string Id { get; set; }
        public int SubjectId { get; set; }
        public string PastPaperTitle { get; set; }
        public string Year { get; set; }
        public int PaperNumber { get; set; }
        public int NumberOfQuestions { get; set; }
        public bool IsDownloaded { get; set; }
        public bool IsNotDownloaded { get; set; }
        public string SubjectTitle { get; set; }
        public string TotalVideoDownloadSizeAndCount { get; set; }


        public long DownloadSize { get; set; }
        public int CorrectAnsweredCount { get; set; }

        public bool IsGreen { get; set; }
        public bool IsRed { get; set; }
        public bool IsYellow { get; set; }
        public string Month { get; set; }

        public string Status { get; set; }
        public string StatusColor { get; set; }
        public double PercentageCoverage { get; set; }
    }
}
