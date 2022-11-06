using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class ETQTopicsDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int TopicNumber { get; set; }
        public int PaperNumber { get; set; }
        public string Title { get; set; }
        public string SubjectTitle { get; set; }


        public double PercentageCoverage { get; set; }

        //Statistics
        public int CorrectAnsweredCount { get; set; }
        public int WrongAnswerCount { get; set; }
        public bool IsRed { get; set; }
        public bool IsYellow { get; set; }
        public bool IsGreen { get; set; }
        public int NumberOfQuestions { get; set; }
        public string Month { get; set; }
    }
}
