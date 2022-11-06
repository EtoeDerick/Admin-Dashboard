using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class UserQuiz
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string QuizId { get; set; }
        public int Score { get; set; }
        public DateTime WrittenDate { get; set; } = DateTime.Now;
    }
}
