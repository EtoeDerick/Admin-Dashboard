using Admin.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Server.Repositories.AdminDashboards;
using Microsoft.AspNetCore.Mvc;
using Admin.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Admin.Server.Repositories.AdminDashboards
{
    public class AdminDashboardRepository : /*ControllerBase,*/ IAdminDashboardRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminDashboardRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<DbCount> Get()
        {
            var count = new DbCount();

            count.subjects = await _db.Subjects.CountAsync();
            count.instructors = await _db.Instructors.CountAsync();
            count.instructorsubjects = await _db.InstructorSubject.CountAsync();
            count.pastpapers = await _db.PastPapers.CountAsync();
            count.mcqs = await _db.MCQs.CountAsync();
            //count.users = await _db.Users.CountAsync();
            count.usersubjects = await _db.UserSubjects.CountAsync();
            //count.accounts = await _db.Accounts.CountAsync();
            //count.credits = _db.Credits.Count();
            count.payments = await _db.Payments.CountAsync();
            //count.payments_success = await _db.Payments.CountAsync(p => p.TransactionStatus.ToLower() == "success");
            //count.payments_total = Convert.ToInt16(total);
            //count.orderdetails = await _db.OrderDetails.CountAsync();
            /*count.orders = await _db.Orders.CountAsync(); */
            count.examinations = await _db.Examinations.CountAsync();
            count.topics = await _db.Topic.CountAsync();
            count.reports = await _db.MCQReports.CountAsync();
            count.constants = await _db.Constants.CountAsync();
            count.announcements = await _db.Announcements.CountAsync();
            count.essaytypequestions = await _db.EssayTypeQuestions.CountAsync();
            count.chapters = await _db.Chapters.CountAsync();
            count.lessons = await _db.Lessons.CountAsync();
            count.downloads = await _db.VideoDownloads.CountAsync();
            count.videos = await _db.Videos.CountAsync();
            count.objectives = await _db.Objectives.CountAsync();
            count.conversations = await _db.Conversations.CountAsync();
            count.examCategories = await _db.ExamCategories.CountAsync();
            count.quizes = await _db.PastPapers.CountAsync(x => x.IsQuiz == true);
            count.quizawards = await _db.QuizAwards.CountAsync();

            return count;
        }
    }
}
