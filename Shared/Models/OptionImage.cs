using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class OptionImage
    {
        public int Id { get; set; }

        [MaxLength(1024)]
        public string OptionImgUrl { get; set; }

        //Navigation Property
        public int MCQId { get; set; }
        public virtual MCQ MCQ { get; set; } = new MCQ();
    }
}
