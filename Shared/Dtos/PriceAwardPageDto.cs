using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class PriceAwardPageDto
    {
        public string StudentName { get; set; }
        public string ImageUrl { get; set; }
        public string School { get; set; }
        public string AwardDate { get; set; }
        public string Description { get; set; }
        public List<AwardComment> AwardComments { get; set; } = new List<AwardComment>();
    }

    public class AwardComment
    {
        public string UserName { get; set; }
        public string Duration { get; set; }
        public string UserNameInitialLetter { get; set; }
        public string Message { get; set; }
        public TimeSpan DurationTimeSpan { get; set; }
        public DateTime Date { get; set; }
    }
}
