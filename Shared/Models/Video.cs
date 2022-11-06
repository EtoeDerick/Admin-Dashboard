using Admin.Shared.Models.Tutorials;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Video
    {
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        public int Duration { get; set; }

        [MaxLength(1024)]
        public string Thumbnail { get; set; }

        [MaxLength(1024)]
        public string Url { get; set; }
        public string PdfTutorialUrl { get; set; }
        public string HtmlEncodedNotes { get; set; }
        public bool IsFree { get; set; }

        public int Position { get; set; }

        //Added Modification
        public int ViewsCount { get; set; }
        public int LikesCount { get; set; }
        public int UnlikesCount { get; set; }
        public int Commentscount { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int TopicId { get; set; }


        // Side-step from 3rd normal form for easier 
        // access to a video’s course
        public int SubjectId { get; set; }
        //public Subject Subject { get; set; }

        //public string PastPaperId { get; set; }
        //public PastPaper PastPaper { get; set; }

        public List<MCQ> MCQs { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

    }
}
