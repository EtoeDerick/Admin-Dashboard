using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class QuizAward
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PastPaperId { get; set; }
        public string StudentImageUrl { get; set; }
        public DateTime AwardedDate { get; set; } = DateTime.UtcNow.AddHours(1);
        public string Description { get; set; } = "is the winner of the 12th Contest of OgaBook Talent Search Program. This contests provides a means for talent search of the best students within the country exposing them to varying opportunities and scolarships!";
    }

    public class QuizAwardDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PastPaperTitle { get; set; }
        public string PastPaperId { get; set; }
        public string StudentImageUrl { get; set; }
        public DateTime AwardedDate { get; set; } = DateTime.UtcNow.AddHours(1);
        public string Description { get; set; } = "is the winner of the 12th Contest of OgaBook Talent Search Program. This contests provides a means for talent search of the best students within the country exposing them to varying opportunities and scolarships!";
    }
}
