using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class TopicsDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int TopicNumber { get; set; }
        public string Title { get; set; }
        public string SubjectTitle { get; set; }
        public float TotalQuestionCount { get; set; }
        public float FailedQuestionCount { get; set; }
        public float PassedQuestionCount { get; set; }
        public double PercentageCoverage { get; set; }
    }
}
