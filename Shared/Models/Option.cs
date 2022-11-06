using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Option
    {

        public int Id { get; set; }

        [MaxLength(4024)]
        public string mcqOption { get; set; }

        //Navigation Property
        public int MCQId { get; set; }
        public virtual MCQ MCQ { get; set; } = new MCQ();
    }
}
