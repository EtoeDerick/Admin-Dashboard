using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class UserSubjectsDto
    {
        public bool IsExpired { get; set; }
        public string Status { get; set; }
        public int SubjectId { get; set; }
        public string AppUserId { get; set; }
        public bool IsNavigated { get; set; }
        public string SubjectTitle { get; set; }
        public int MonthlyPrice { get; set; }
        public int AnnualPrice { get; set; }
        public int TenMonthsPrice { get; set; }
        public string ImageUrl { get; set; }
        public string AverageProgress { get; set; }
        public bool IsNotEnroll { get; set; } = false;
        public string ExamType { get; set; } //FrontEnd
        public bool IsPaper1ContentAvailable { get; set; } = true;
        public bool IsPaper2ContentAvailable { get; set; }
        public bool IsPaper3ContentAvailable { get; set; }
        public bool IsTutorialContentAvailable { get; set; }

        public string PaymentStatus { get; set; }

        //Use this field to track the TabBar page number
        //1: MCQ, 2: MCQBYTopics, 3: Paper 2&3, 4: Tutorial, 5: Forums
        public int TopBarPageNumber { get; set; } = 1; //FrontEnd
        public List<SubjectInstructorDto> Instructors { get; set; }
        public List<SubjectContentType> SubjectContentTypes { get; set; }

        public UserSubjectsDto()
        {
            Instructors = new List<SubjectInstructorDto>();
            SubjectContentTypes = new List<SubjectContentType>();
        }
    }
}
