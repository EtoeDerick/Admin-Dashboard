using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models.Tutorials
{
    public class Lesson
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public int SubjectId { get; set; }
        public string LessonTitle { get; set; }
        public string Description { get; set; }
        public int LectureCount { get; set; }
        public int TotalLectureDuration { get; set; }
        public List<Video> Videos { get; set; }
        public List<Download> Downloads { get; set; }

        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }

    }
}
