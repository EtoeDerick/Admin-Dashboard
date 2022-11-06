using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models.ETQ
{
    public class EssayTypeQuestion
    {
        public int Id { get; set; }
        public bool HasUniqueSolution { get; set; }
        public int TotalMarks { get; set; }
        public int Position { get; set; }
        public string Introduction { get; set; }
        public string ImageUrlBeforeIntroduction { get; set; }
        public string ImageUrlAfterIntroduction { get; set; }
        public string VideoUrl { get; set; }
        //public int TopicId { get; set; }
        public List<Question> Questions { get; set; }

        //NAVIGATION PROPERTIES
        public string PastPaperId { get; set; }
        public virtual PastPaper PastPaper { get; set; }
    }
}
