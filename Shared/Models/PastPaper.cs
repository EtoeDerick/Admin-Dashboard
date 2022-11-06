using Admin.Shared.Models.ETQ;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class PastPaper
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(50), Required]
        public string Title { get; set; }

        [MaxLength(30), Required]
        public string PaperYear { get; set; }

        [Required]
        public int PaperNumber { get; set; }
        public string Thumbnail { get; set; }

        [MaxLength(1024)]
        public string Url { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFree { get; set; }
        public int Quantity { get; set; }
        public bool IsDownloaded { get; set; } //updated each time user submits answers
        public int CorrectAnswerCount { get; set; } // updated each time user submits answers
        public long DownloadSize { get; set; } //in MB

        //Grants the user ability to use some past papers
        //Status = true ==> This given past paper is free even if the student must pay for the subject
        public string Status { get; set; }

        //QUIZ ADDED: 17-05-2022
        public DateTime WrittenDate { get; set; } = DateTime.UtcNow;
        public int DurationInMinutes { get; set; } = 0;

        [MaxLength(20)]
        public string Visibility { get; set; } = "protected";//private, protected or public
        public string QuizOwnerId { get; set; } = string.Empty;

        [MaxLength(256)]
        public string QuizPassCode { get; set; }
        public int QuizNumber { get; set; }
        public bool IsQuiz { get; set; }

        public bool IsRightWrong { get; set; }

        //Navigation Properties
        public virtual List<MCQ> Questions { get; set; } = new List<MCQ>();

        //public virtual List<Video> Videos { get; set; } = new List<Video>();

        public List<EssayTypeQuestion> EssayTypeQuestions { get; set; }

        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; } = new Subject();

        public string SetId()
        {
            if (IsQuiz)
            {
                Id = SubjectID.ToString() + "_Quiz_No._" + QuizNumber + "_" + PaperNumber + "_" + PaperYear;
            }
            else
            {
                Id = SubjectID.ToString() + "_P" + "_" + PaperNumber + "_" + PaperYear;
            }
            return Id;
        }
    }
}
