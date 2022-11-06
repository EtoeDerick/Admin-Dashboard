using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class AnnouncementDto
    {
        public int NumberOfDaysToExamination { get; set; }
        public int ExaminationId { get; set; }
        public string ExaminationTitle { get; set; }
        public string ExamDaysLeftBgColor { get; set; }
        public string AnnouncementTitle { get; set; }
        public string AnnouncementDescription { get; set; }
        public string Label1Sub1 { get; set; }
        public string Label1Sub2 { get; set; }
        public string Label1Sub3 { get; set; }
        public string Label2Sub1 { get; set; }
        public string Label2Sub2 { get; set; }
        public string Label2Sub3 { get; set; }
        public string HowToUseOgaBookVideoUrl { get; set; }
        public string VideoTitle { get; set; }
        public string EmailContact { get; set; }
        public string Line1ContactWithWhatsApp { get; set; }
        public string Line2Contact { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
