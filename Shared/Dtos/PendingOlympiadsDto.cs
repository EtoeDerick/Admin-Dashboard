using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class PendingOlympiadsDto
    {
        public string Id { get; set; }
        public string OlympiadTitle { get; set; }
        public DateTime WrittenDate { get; set; }
        public int DurationInMinutes { get; set; }
        public int ParticipantsCount { get; set; }
        public int QuestionCount { get; set; }
        public int Score { get; set; }
        public bool IsLoading { get; set; }
        public bool IsAttempted { get; set; }
        public int SubjectId { get; set; }
        public bool IsOngoing { get; set; }
        public string FirstPrice { get; set; }
        public string QuizCode { get; set; }
        public string CourseOutline { get; set; }
        public QuizStatusDto QuizStatusDto { get; set; } = new QuizStatusDto();
    }
}
