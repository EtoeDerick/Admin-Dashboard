using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class ExamCategory
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string CategoryBgColor { get; set; } = "#D6EAF8";
        [MaxLength(50)]
        public string CategoryTextColor { get; set; } = "DodgerBlue";
        public virtual List<Examination> Examinations { get; set; } = new List<Examination>();
    }
}
