using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class MCQ
    {
        public int Id { get; set; }
        public string Question { get; set; }

        [MaxLength(1024)]
        public string QuestionImageUrl { get; set; }

        public List<Option> Options { get; set; }
        public List<OptionImage> OptionImageUrl { get; set; }
        public List<Answer> Answers { get; set; }
        public int Answer { get; set; }

        [MaxLength(255)]
        public string AnswerProvided { get; set; }
        public bool MultipleAnswers { get; set; }
        public bool IsAnonymous { get; set; }

        [MaxLength(255)]
        public string correctAnswer { get; set; }

        [MaxLength(10024)]
        public string JustificationText { get; set; }

        [MaxLength(1024)]
        public string JustificationImageUrl { get; set; }

        [MaxLength(1024)]
        public string VideoUrl { get; set; }

        public string Instruction { get; set; }
        //public string Topic { get; set; }
        public int TopicId { get; set; }
        public int Position { get; set; }
        public int SubjectId { get; set; }

        //NAVIGATION PROPERTIES
        public string PastPaperId { get; set; }
        public virtual PastPaper PastPaper { get; set; }

    }
}
