using Admin.Shared.Models.Tutorials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class SubjectOverViewDto
    {
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public string Description { get; set; }
        public string VideoPreviewUrl { get; set; }
        public bool IsVideoUrlPresent { get; set; }
        public bool IsVideoUrlAbsent { get; set; }
        public string Ratings { get; set; }
        public int EnrollmentCount { get; set; }

        public List<Chapter> Chapters { get; set; }

        public SubjectOverViewDto()
        {
            Chapters = new List<Chapter>();
        }
    }
}
