using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class ExaminationDto
    {
        public string Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string QuestionRange { get; set; }

        public bool IsApproved { get; set; }
    }
}
