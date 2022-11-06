using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Examination
    {
        public string Id { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Exam Title can not be longer than 50 characters ")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Exam Description can not be longer than 255 characters ")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [StringLength(1024, ErrorMessage = "Image Link can not be longer than 1024 characters ")]
        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [StringLength(50, ErrorMessage = "Enter text: 2010 - 2020 for available past papers")]
        [Display(Name = "Range of Questions")]
        public string QuestionRange { get; set; }

        [MaxLength(10)]
        public string ExamType { get; set; }

        public bool IsApproved { get; set; }

        //Indicates or contains information on the day the exam will be written: This will be used to notify the user how many days into the exam
        public DateTime WrittenOn { get; set; } = DateTime.Now;

        //Navigation Properties
        public virtual List<Subject> Subjects { get; set; } = new List<Subject>();

        public int ExamCategoryId { get; set; }
        public virtual ExamCategory ExamCategory { get; set; } = new ExamCategory();

    }
}
