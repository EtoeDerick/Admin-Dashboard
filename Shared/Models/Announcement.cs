using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Announcement
    {
        public int Id { get; set; }
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
        public string UpdateFeatures { get; set; }

        public bool IsActive { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
