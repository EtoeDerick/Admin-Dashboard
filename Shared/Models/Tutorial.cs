using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Tutorial
    {
        public int Id { get; set; }
        public int TopicId { get; set; }

        [MaxLength(50)]
        public string Chapter { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public string Content { get; set; }

        //Navigation properties
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = new Subject();
    }
}
