using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class QuizOwner
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
        public string LogoUrl { get; set; }

    }
}
