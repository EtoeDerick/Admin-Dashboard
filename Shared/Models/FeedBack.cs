using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        public DateTime FeedBackDate { get; set; } = DateTime.Now;

        [Required, MaxLength(2048)]
        public string Description { get; set; }


        //Navigation Properties
        public string AppUserId { get; set; }
    }
}
