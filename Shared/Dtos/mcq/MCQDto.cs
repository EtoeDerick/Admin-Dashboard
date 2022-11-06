using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos.mcq
{
    public class MCQDto
    {

        public int Id { get; set; }
        public string Question { get; set; }
        public string Instruction { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public List<string> OptionImageUrl { get; set; } = new List<string>();
        public string QuestionImageUrl { get; set; }
        public int Answer { get; set; }
        public List<int> Answers { get; set; } = new List<int>();
        //public List<int> AnswersSubmitted { get; set; } = new List<int>();
        //public bool IsAnswerSubmitted { get; set; }
        //public bool IsExplanationVisible { get; set; }
        //public bool IsCorrect { get; set; }
        public string ExamYear { get; set; }
        public string JustificationText { get; set; }
        public string JustificationImageUrl { get; set; }
        public int TopicId { get; set; }
        public int Position { get; set; }
        public string PastPaperId { get; set; }
        public bool IsAnsweredCorrectly { get; set; }
        public string VideoUrl { get; set; }

        //public bool IsQuestionImgAvailable { get; set; }
        //public bool IsJustificationImgAvailable { get; set; }

        public List<MCQOption> MCQOptions { get; set; } = new List<MCQOption>();
        public List<MCQOptionImage> MCQOptionImages { get; set; } = new List<MCQOptionImage>();
    }
}
