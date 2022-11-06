using Admin.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class PaperOneSubjectDownloadDto
    {
        public List<MCQ> MCQs { get; set; }
        public Subject Subject { get; set; }
        public UserSubject UserSubject { get; set; }
        public List<Topic> Topics { get; set; }
    }
}
