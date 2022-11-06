using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class mcqdto
    {
        public string Question { get; set; }
        public string QuestionImageUrl { get; set; }

        public List<string> Options { get; set; } = new List<string>();
        public List<string> OptionImageUrl { get; set; } = new List<string>();
        public List<int> Answers { get; set; } = new List<int>();
        public int Answer { get; set; }

        public string AnswerProvided { get; set; }
        public bool MultipleAnswers { get; set; }
        public bool IsAnonymous { get; set; }
        public string correctAnswer { get; set; }
        public string JustificationText { get; set; }
        public string JustificationImageUrl { get; set; }
        public string VideoUrl { get; set; }

        public string Instruction { get; set; }
        //public string Topic { get; set; }
        public int TopicId { get; set; }
        public int Position { get; set; }
    }

}
