using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class UserProgression
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SubjectId { get; set; }
        public string PastPaperId { get; set; }
        public int PaperNumber { get; set; }
        public int QuestionPosition { get; set; }
        public string PaperYear { get; set; }
        public int TopicNum { get; set; }
        public int AnswerStatus { get; set; } //What is use of Answer Status:  2 ---> Correct Answer, 1 ----> Wrong Answer, -1 ----> No Answer 
        /// <summary> if the solution submitted is a quiz
        /// -1 : Skipped
        /// 1 : Wrong
        /// 2 : Right
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
