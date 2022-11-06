using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class QuizStatusDto
    {
        public string Title { get; set; }

        public bool IsApproved { get; set; }
        public bool IsFree { get; set; }
        public int Quantity { get; set; }

        //Grants the user ability to use some past papers
        //Status = true ==> This given past paper is free even if the student must pay for the subject
        public int Status { get; set; } = 2;
        public bool IsRightWrong { get; set; }

        //QUIZ ADDED: 17-05-2022
        public DateTime WrittenDate { get; set; } = DateTime.UtcNow;
        public int DurationInMinutes { get; set; } = 0;

        public string Visibility { get; set; } = "protected";//private, protected or public
        public string QuizOwnerId { get; set; } = string.Empty;
        public int QuizNumber { get; set; }
        //subjectId,pastpaperId
        public int SubjectId { get; set; }
        public string PastPaperId { get; set; }
        public bool HasParticipated { get; set; }
    }
}
