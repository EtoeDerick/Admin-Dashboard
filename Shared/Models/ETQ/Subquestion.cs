using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models.ETQ
{
    public class Subquestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Position { get; set; }
        public string ImageUrlBeforeText { get; set; }
        public string ImageUrlAfterText { get; set; }
        public int Marks { get; set; }
        public int TopicId { get; set; }
        public List<Solution> Solution { get; set; }
        public string VideoUrl { get; set; }

        //Navigation Properties
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
