using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models.ETQ
{
    public class Question
    {
        public int Id { get; set; }
        public bool HasUniqueSolution { get; set; }
        public int Position { get; set; }
        public string Text { get; set; }
        public string ImageUrlBeforeText { get; set; }
        public string ImageUrlAfterText { get; set; }
        public int Marks { get; set; }
        public int TopicId { get; set; }
        public string VideoUrl { get; set; }

        //Navigation Properties
        public List<Subquestion> SubQuestions { get; set; }
        public List<QuestionSolution> QuestionSolution { get; set; }

        public int EssayTypeQuestionId { get; set; }
        public EssayTypeQuestion EssayTypeQuestion { get; set; }
    }
}
