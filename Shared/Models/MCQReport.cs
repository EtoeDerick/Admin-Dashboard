using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class MCQReport
    {
        public int Id { get; set; }
        public int McqId { get; set; }

        [MaxLength(24)]
        public string PastPaperId { get; set; }

        public int SubjectId { get; set; }

        [MaxLength(1024)]
        public string Report { get; set; }

        [MaxLength(12048)]
        public string Response { get; set; }

        public int QuestionPosition { get; set; }

        [MaxLength(1024)]
        public string UserId { get; set; }

        public bool IsReported { get; set; }

        public bool IsResolved { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public DateTime ResolveDate { get; set; }
    }
}
