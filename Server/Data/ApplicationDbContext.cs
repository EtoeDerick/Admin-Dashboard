using Admin.Shared.Models;
using Admin.Shared.Models.ETQ;
using Admin.Shared.Models.Tutorials;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        //public DbSet<Book> Books { get; set; }

        //public DbSet<Book> Books { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<QuizAward> QuizAwards { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<ExamCategory> ExamCategories { get; set; }

        public DbSet<UserSubject> UserSubjects { get; set; }
        public DbSet<Downloadpdf> Downloadpdfs { get; set; }


        //Update: Adding Quizzes and Mentorship
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<UserMentor> UserMentors { get; set; }
        public DbSet<QuizOwner> QuizOwners { get; set; }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<PastPaper> PastPapers { get; set; }
        public DbSet<MCQ> MCQs { get; set; }

        //public DbSet<Bank> Banks { get; set; }

        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Video> Videos { get; set; }

        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<InstructorSubject> InstructorSubject { get; set; }

        public DbSet<MCQReport> MCQReports { get; set; }

        public DbSet<Constants> Constants { get; set; }

        public DbSet<Payment> Payments { get; set; }

        //public DbSet<Logins> Logins { get; set; }
        //public DbSet<SuspendedAccount> SuspendedAccounts { get; set; }

        public DbSet<SubjectAudit> SubjectAudits { get; set; }
        public DbSet<UserSubjectAudit> UserSubjectAudits { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<UserProgression> UserProgressions { get; set; }

        //ESSAY TYPE QUESTIONS MODEL
        public DbSet<EssayTypeQuestion> EssayTypeQuestions { get; set; }
        public DbSet<Question> ETQQuestions { get; set; }
        public DbSet<Subquestion> ETQSubquestions { get; set; }
        public DbSet<Solution> ETQSolutions { get; set; }
        public DbSet<QuestionSolution> QuestionSolution { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Download> VideoDownloads { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<DownloadTrackingTable> DownloadTrackingTables { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subject>().ToTable("Subject");
            builder.Entity<Examination>().ToTable("Examination");

            builder.Entity<UserSubject>().ToTable("UserSubject");

            builder.Entity<Instructor>().ToTable("Instructor");
            builder.Entity<PastPaper>().ToTable("PastPaper");
            builder.Entity<MCQ>().ToTable("MCQ");
            builder.Entity<Video>().ToTable("Video");
            builder.Entity<Tutorial>().ToTable("Tutorial");

            //One to many relationship between MCQ and Option/OptionImages/Answers
            builder.Entity<MCQ>()
                .HasMany(m => m.Options)
                .WithOne(o => o.MCQ)
                .IsRequired();

            builder.Entity<MCQ>()
                .HasMany(m => m.OptionImageUrl)
                .WithOne(o => o.MCQ)
                .IsRequired();

            builder.Entity<MCQ>()
                .HasMany(m => m.Answers)
                .WithOne(o => o.MCQ)
                .IsRequired();

            //Modelling Many to One relationships for complete ESSAY TYPE QUESTION
            builder.Entity<PastPaper>()
                .HasMany(p => p.EssayTypeQuestions)
                .WithOne(e => e.PastPaper).IsRequired();

            builder.Entity<EssayTypeQuestion>()
                .HasMany(e => e.Questions)
                .WithOne(q => q.EssayTypeQuestion).IsRequired();


            builder.Entity<Question>()
                .HasMany(q => q.SubQuestions)
                .WithOne(s => s.Question).IsRequired();

            builder.Entity<Question>()
                .HasMany(q => q.QuestionSolution)
                .WithOne(sol => sol.Question).IsRequired();

            builder.Entity<Subquestion>()
                .HasMany(s => s.Solution)
                .WithOne(sol => sol.Subquestion).IsRequired();

            //subject and topics
            builder.Entity<Subject>()
                .HasMany(s => s.Topics)
                .WithOne(t => t.Subject).IsRequired();

            //Modelling relationship for Tutorial
            builder.Entity<Subject>()
                .HasMany(s => s.Chapters)
                .WithOne(c => c.Subject).IsRequired();

            builder.Entity<Chapter>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Chapter).IsRequired();

            builder.Entity<Chapter>()
                .HasMany(c => c.ChapterObjectives)
                .WithOne(o => o.Chapter).IsRequired();

            builder.Entity<Lesson>()
                .HasMany(l => l.Downloads)
                .WithOne(d => d.Lesson).IsRequired();

            builder.Entity<Lesson>()
                .HasMany(l => l.Videos)
                .WithOne(v => v.Lesson).IsRequired();

            //Conversation model
            builder.Entity<Conversation>()
                .HasMany(c => c.Votes)
                .WithOne(v => v.Conversation).IsRequired();

            builder.Entity<Vote>()
                .HasKey(v => new { v.UserId, v.ConversationId });


            //Many-to-many relationship between WebAppUser:Subject
            builder.Entity<UserSubject>()
                .HasKey(us => new { us.AppUserId, us.SubjectId });

            builder.Entity<UserSubject>()
                .HasOne(us => us.AppUser)
                .WithMany(u => u.UserSubjects).HasForeignKey(us => us.AppUserId);

            builder.Entity<UserSubject>()
                .HasOne(us => us.Subject)
                .WithMany(s => s.UserSubjects).HasForeignKey(us => us.SubjectId);

            //Many-to-many relationship between AppUser:Mentor
            builder.Entity<UserMentor>()
                .HasKey(um => new { um.AppUserId, um.MentorId });

            builder.Entity<UserMentor>()
                .HasOne(um => um.AppUser)
                .WithMany(m => m.UserMentors).HasForeignKey(um => um.AppUserId);

            builder.Entity<UserMentor>()
                .HasOne(um => um.Mentor)
                .WithMany(m => m.UserMentors).HasForeignKey(um => um.MentorId);




            //Many-to-many relationship between Instructor:Subject
            builder.Entity<InstructorSubject>()
                .HasKey(ins => new { ins.InstructorId, ins.SubjectId });
            builder.Entity<InstructorSubject>()
                .HasOne(ins => ins.Instructor)
                .WithMany(s => s.InstructorSubjects)
                .HasForeignKey(ins => ins.InstructorId);
            builder.Entity<InstructorSubject>()
                .HasOne(ins => ins.Subject)
                .WithMany(s => s.InstructorSubjects)
                .HasForeignKey(ins => ins.SubjectId);

            //One-One relationship between ForwardedPayment - PaymentReference
        }
        public DbSet<Option> Option { get; set; }
        public DbSet<OptionImage> OptionImage { get; set; }

    }
}
