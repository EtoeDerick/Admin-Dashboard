using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class QuizResultDto
    {
        public string Username { get; set; }
        public string RegionOfOrigin { get; set; }
        public string Town { get; set; }
        public string School { get; set; }
        public string Phone { get; set; }
        public int Rank { get; set; }
        public string Price { get; set; }
        public bool IsWinner { get; set; }
        public int Score { get; set; }
        public int TotalParticipants { get; set; }
        public string PastPaperId { get; set; }
        public string UserId { get; set; }
    }
}
