using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models.Tutorials
{
    public class Chapter
    {
        public int Id { get; set; }
        public string ChapterTitle { get; set; }
        public int ChapterPriorityNumber { get; set; }
        public string Description { get; set; }
        public int LectureCount { get; set; }
        public int TotalLectureDuration { get; set; }
        public List<Objective> ChapterObjectives { get; set; } //What you will learn
        public List<Lesson> Lessons { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }

}
