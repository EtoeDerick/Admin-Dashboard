using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Quizes
{
    public class PastPapersQuizesRepository : ControllerBase, IPastPapersQuizesRepository
    {
        private readonly ApplicationDbContext _context;
        public PastPapersQuizesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PastPaper>> Get()
        {
            return await _context.PastPapers.Where(p => (p.PaperNumber  == 1 || p.PaperNumber > 3 ) && p.IsQuiz == true && p.IsApproved == true).ToListAsync();
        }

        public async Task<QuizResultsDto> Get(string id )
        {
            var quizResult = new QuizResultsDto();
            var pastPaper = await _context.PastPapers.FindAsync(id);

            if (pastPaper == null)
            {
                return quizResult;
            }

            quizResult.QuizTitle = pastPaper.Title;
            quizResult.TotalParticipated = await _context.UserQuizzes.CountAsync(q => q.QuizId == pastPaper.Id);
            //quizResult.Quantity = pastPaper.Quantity;
            quizResult.Quantity = await _context.MCQs.CountAsync(m => m.PastPaperId == id);
            quizResult.WrittenTime = pastPaper.WrittenDate;
            quizResult.TotalPassed = await _context.UserQuizzes.CountAsync(u => u.QuizId == id && u.Score >= (pastPaper.Quantity / 2) );

            if(quizResult.TotalParticipated > 0)
            {
                quizResult.PercentagePassed = (float)(quizResult.TotalPassed / quizResult.TotalParticipated);
            }
            

            var usq = await _context.UserQuizzes.Where(u => u.QuizId == pastPaper.Id).ToListAsync();
            var quizparticipants = new List<QuizParticipant>();


            if(usq.Count > 0)
            {
                for(int i = 0; i<usq.Count; i++)
                {
                    var qp = new QuizParticipant()
                    {
                        
                         //Name = await _context.AppUsers.AnyAsync(x => x.Id == usq[i].UserId) ? _context.AppUsers.Single(x => x.Id == usq[i].UserId).UserName : "",
                         Score = usq[i].Score,
                         Remarks = "Keep Working!"
                    };
                    if (_context.AppUsers.Any(x => x.Id == usq[i].UserId))
                    {
                        var usr = _context.AppUsers.Single(x => x.Id == usq[i].UserId);
                        if(usr != null)
                        {
                            qp.Name = usr.UserName;
                        }
                    }
                    else
                    {
                        qp.Name = "No Name (Anonymous User!)";
                    }

                    quizparticipants.Add(qp);
                }

                var participants = (List<QuizParticipant>)quizparticipants.OrderByDescending(x => x.Score);

                for (int i = 0; i < participants.Count; i++)
                {
                    participants[i].Id = i + 1;
                }

                quizResult.QuizParticipants.AddRange(participants);
            }
            //quizResult.QuizParticipants.AddRange(quizparticipants.OrderBy(x => x.Score));

            return quizResult;
        }
    }
}
