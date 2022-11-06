using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class ForumDiscussionPageDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string UserId { get; set; }
        public int ReplyId { get; set; }
        public string UserName { get; set; }
        public string Duration { get; set; }
        public DateTime Date { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDescription { get; set; }
        public int ReplyCount { get; set; }
        public int LikeCount { get; set; }
        public int UnlikeCount { get; set; }
        public bool IsInstructor { get; set; }
        public bool IsMentor { get; set; }
        public bool IsPinned { get; set; }
        public bool IsMany { get; set; }
        public bool IsNotMany { get; set; }
        public int DiscussionPriority { get; set; }
    }
}
