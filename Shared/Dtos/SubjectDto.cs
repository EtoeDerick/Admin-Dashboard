using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class SubjectDto
    {
        public string ExaminationId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string BackgroundImageUrl { get; set; }
        public string Ratings { get; set; }
        public int ReviewCount { get; set; }
        public int MonthlyPrice { get; set; }
        public int AnnualPrice { get; set; }
        public string Category { get; set; }
        public string VideoPreviewUrl { get; set; }
        public int EnrollmentCount { get; set; }
        public string Instructors { get; set; }
        public string AppUserId { get; set; }
        public string PaymentStatus { get; set; } //NotEnrolled, Paid, Expired ==> Computed field, null means the user doesn't have an account yet or not yet enrolled in the subject

        //To
        public bool IsNotEnroll { get; set; }
        public bool IsPaper1ContentAvailable { get; set; }
        public bool IsPaper2ContentAvailable { get; set; }
        public bool IsPaper3ContentAvailable { get; set; }
        public bool IsTutorialContentAvailable { get; set; }


    }

}
