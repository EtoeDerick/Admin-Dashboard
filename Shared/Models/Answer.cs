using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public int Ans { get; set; }

        //Navigation Property
        public int MCQId { get; set; }
        public virtual MCQ MCQ { get; set; } = new MCQ();
    }
}
