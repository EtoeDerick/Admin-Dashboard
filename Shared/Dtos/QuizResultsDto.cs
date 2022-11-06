using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class QuizResultsDto
    {

        public string QuizTitle { get; set; }
        public int TotalParticipated { get; set; }
        public int TotalPassed { get; set; }
        public int Quantity { get; set; }
        public float PercentagePassed { get; set; }
        public DateTime WrittenTime { get; set; }
        public List<QuizParticipant> QuizParticipants { get; set; } = new List<QuizParticipant>();
    }

    public class QuizParticipant
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public float Score { get; set; }
        public string Remarks { get; set; }
    }
}
