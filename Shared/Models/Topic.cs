using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Topic
    {
        public int Id { get; set; }

        [MaxLength(120)]
        public string Title { get; set; }

        public int TopicNum { get; set; }
        public bool IsAlsoP3Topic { get; set; }

        //Navigation Properties
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = new Subject();
    }
}
