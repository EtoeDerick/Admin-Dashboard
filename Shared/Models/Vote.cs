using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Vote
    {

        [MaxLength(200)]
        public string UserId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsUnliked { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        //Navigation Property
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
