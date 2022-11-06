using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.UserProgression
{
    public class UserProgressionRepository : ControllerBase, IUserProgressionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProgressionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Get(int subjectId, string pastpaperId, int pastpaperNumber, int topicNumber, int questionPosition, string paperYear, string userid, int answerStatus = 3)
        {
            

            if (!RecordExists(pastpaperId, questionPosition, userid))
            {
                //Perform the update under this condition
                var up = new Admin.Shared.Models.UserProgression()
                {
                    UserId = userid,
                    SubjectId = subjectId,
                    PastPaperId = pastpaperId,
                    PaperNumber = pastpaperNumber,
                    QuestionPosition = questionPosition,
                    PaperYear = paperYear,
                    TopicNum = topicNumber,
                    AnswerStatus = answerStatus
                }; 
                await CreateRecord(up);
                /*
                if (answerStatus == -1 || answerStatus == 1 || answerStatus == 2)
                {
                    if (!UserQuizExists(pastpaperId, userid))
                    {
                        var usrquiz = new Admin.Shared.Models.UserQuiz()
                        {
                            UserId = userid,
                            QuizId = pastpaperId,
                            WrittenDate = DateTime.UtcNow
                        };

                        await CreateUserQuizRecord(usrquiz);
                    }
                }*/

                return 0;
            }

            return 1;
        }
        public async Task<string> GetQuizResponse(string pastpaperId, int score, string userid, string successfulSolutions)
        {
            //int subjectId, string pastpaperId, int pastpaperNumber, int topicNumber, string paperYear,
            //Above info is present in the pastpaperId --------> We can replace them with pastpaperId

            //STEPS: 
            /*
            1.) Enter userscore and save in db
            2.) Compute info and return user result
             */

            //SAVE UserQuiz Score
            await saveQuizResponses(pastpaperId, userid, successfulSolutions);

            var olympiads = await _context.UserProgressions.Where(u => u.PastPaperId == pastpaperId).ToListAsync();

            //if(olympiads.Count == 0)
            //{
            //    return "1 out of 1";
            //}
            //int currentscore = await _context.UserProgressions.CountAsync(up => up.PastPaperId == pastpaperId && up.UserId == userid && up.AnswerStatus == 1);

            //var allScoresPeruser = olympiads.GroupBy(x => x.UserId);

            int position = await getUserRankById(pastpaperId, userid);
            var totalParticipants = olympiads.GroupBy(x => x.UserId).Count(); //current user was exempted from the query

            //string rank = "5 out of 32";

            return position.ToString() + " out of " + totalParticipants.ToString();
        }

        async Task saveQuizResponses(string pastpaperId, string userid, string successfulSolutions)
        {
            //ENTERING USER SCORE
            var answers = successfulSolutions.Split(',');

            //answerStatus: 0 ---> Not Attempted, 1 ---> Correct, 2 ---> Wrong
            //1-0-20102: Question 1 not attempted, topicId = 20102
            //4-1-20102: Qustion 4 is correct, topicId = 20102
            //9-2-20102: Question 9 is wrong, topicId = 20102
            //questionPosition-answerStatus

            int subjectId; int pastpaperNumber; int topicNumber = 0; string paperYear;

            //201_P_1_2014 or 595_Quiz_No._1_1_2022 is the format of pastpaper

            var allFields = pastpaperId.Split('_');
            Int32.TryParse(allFields.ElementAt(0), out subjectId);
            Int32.TryParse(allFields.ElementAt(4), out pastpaperNumber);
            paperYear = allFields.ElementAt(5);

            foreach (var answer in answers)
            {
                var ans = answer.Split('-');
                int questionPosition = 0;
                int answerStatus = 3;
                Int32.TryParse(ans.ElementAt(0), out questionPosition);
                Int32.TryParse(ans.ElementAt(1), out answerStatus);
                Int32.TryParse(ans.ElementAt(2), out topicNumber);

                if (!RecordExists(pastpaperId, questionPosition, userid))
                {
                    //Perform the update under this condition
                    var up = new Admin.Shared.Models.UserProgression()
                    {
                        UserId = userid,
                        SubjectId = subjectId,
                        PastPaperId = pastpaperId,
                        PaperNumber = pastpaperNumber,
                        QuestionPosition = questionPosition,
                        PaperYear = paperYear,
                        TopicNum = topicNumber,
                        AnswerStatus = answerStatus
                    };
                    await CreateRecord(up);

                }
            }
        }
        private bool RecordExists(string pastpaperId, int position, string userId)
        {
            return _context.UserProgressions.Any(up => up.PastPaperId == pastpaperId && up.QuestionPosition == position && up.UserId == userId);
        }
        private bool UserQuizExists(string pastpaperId, string userId)
        {
            return _context.UserQuizzes.Any(uq => uq.QuizId == pastpaperId && uq.UserId == userId);
        }
        async Task CreateRecord(Admin.Shared.Models.UserProgression userProgression)
        {
            _context.UserProgressions.Add(userProgression);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error", ex.Message);
            }

        }

        async Task CreateUserQuizRecord(Admin.Shared.Models.UserQuiz userQuiz)
        {
            _context.UserQuizzes.Add(userQuiz);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error", ex.Message);
            }

        }


        public async Task<IEnumerable<Shared.Models.UserProgression>> GetUserProgressions()
        {
            return await _context.UserProgressions.ToListAsync();
        }

        public async Task<List<Shared.Models.UserProgression>> GetQuizSubmitted(string pastpaperId)
        {
            return await _context.UserProgressions.Where(u => u.PastPaperId == pastpaperId).ToListAsync();
        }

        
        public async Task<int> getUserRankById(string pastpaperId, string userid)
        {
            var quizResultStats = new List<QuizResultDto>();
            var participants = await _context.UserProgressions.Where(pp => pp.PastPaperId == pastpaperId).ToListAsync();
            var allparticipants = participants.GroupBy(x => x.UserId);
            //Count the number of users
            var numberOfParticipants = allparticipants.Count();   //current user was exempted from the query       

            foreach (var u in allparticipants)
            {
                //var ups =  await _context.UserProgressions.Where(up => up.PastPaperId == pastpaperId && up.UserId == u.Key && up.AnswerStatus == 1).ToListAsync();

                int score = u.Count(up => up.PastPaperId == pastpaperId && /*up.UserId == u.Key &&*/ up.AnswerStatus == 1);

                var addresses = await _context.Addresses.Where(a => a.UserId == u.Key).ToListAsync();
                var address = new Admin.Shared.Models.Address();
                if (addresses.Count > 0)
                    address = addresses.ElementAt(0);
                var quizStats = new QuizResultDto
                {
                    UserId = address.UserId,
                    Username = address.UserName,
                    TotalParticipants = numberOfParticipants,
                    //Price = _context.PastPapers.Single(p => p.Id == pastpaperId).Url,
                    Phone = address.Phone,
                    RegionOfOrigin = address.RegionOfOrigin,
                    Town = address.Town,
                    School = address.School,
                    //Rank = await getUserRankById(pastpaperId, u.Key, score),
                    Score = score,
                    PastPaperId = pastpaperId
                };



                if (quizStats.Rank == 1)
                {
                    quizStats.IsWinner = true;
                    quizStats.Price = _context.PastPapers.Single(p => p.Id == pastpaperId).Url;
                }
                quizResultStats.Add(quizStats);
            }

            var sortedByRank = quizResultStats.OrderByDescending(x => x.Score).ThenBy(y => y.Username);
            int rank = 0;
            var isNotFound = true;
            while (isNotFound && rank < sortedByRank.Count())
            {
                rank = rank + 1;
                if (sortedByRank.ElementAt(rank).UserId == userid)
                {
                    isNotFound = true;
                    break;
                }
                
            }
            
            return rank;
        }
    }
}
