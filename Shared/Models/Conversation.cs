using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [MaxLength(200)]
        public string MessageTitle { get; set; }

        [MaxLength(2000)]
        public string MessageDescription { get; set; }
        public int ReplyId { get; set; }
        public bool IsAReply { get; set; }
        public int PriorityNumber { get; set; }
        public bool IsNotApproved { get; set; }

        [MaxLength(1024)]
        public string UserId { get; set; }
        public int DiscussionForumId { get; set; }
        public int SubjectId { get; set; }
        public List<Vote> Votes { get; set; }
    }
}
