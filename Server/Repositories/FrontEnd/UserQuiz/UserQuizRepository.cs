using Admin.Server.Data;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.UserQuiz
{
    public class UserQuizRepository: ControllerBase, IUserQuizRepository
    {
        private readonly ApplicationDbContext _context;
        public UserQuizRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(string pastpaperId, string userid, string username, int score)
        {

            if (!_context.UserQuizzes.Any(u => u.QuizId == pastpaperId && u.UserId == userid))
            {
                var usrquiz = new Admin.Shared.Models.UserQuiz()
                {
                    UserId = userid,
                    QuizId = pastpaperId,
                    Score = score,
                    WrittenDate = DateTime.UtcNow
                };

                await CreateUserQuizRecord(usrquiz);
            }

            var userExist = await _context.AppUsers.AnyAsync( u => u.Id == userid);
            if (!userExist)
            {
                var usr = new AppUser()
                {
                    Id = userid,
                    UserName = username
                };
                await CreateUser(usr);
            }
            else  if (userExist)
            {
                var usr = await _context.AppUsers.FindAsync(userid);
                if (String.IsNullOrEmpty(usr.UserName))
                {

                    usr.UserName = username;
                    
                    //Update User Info to contain name
                    await UpdateUser(usr);
                }                
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

        async Task CreateUser(Admin.Shared.Models.AppUser user)
        {
            _context.AppUsers.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error", ex.Message);
            }

        }

        async Task UpdateUser(Admin.Shared.Models.AppUser usr)
        {
            _context.Entry(usr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

        }

        public async Task<IEnumerable<Shared.Models.UserQuiz>> GetUserQuizzes()
        {
            return await _context.UserQuizzes.ToListAsync();
        }
    }
}
