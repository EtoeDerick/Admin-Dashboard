using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Admin.Server.Data;
using Microsoft.EntityFrameworkCore;
using Admin.Server.Repositories;
using Admin.Server.Repositories.Subjects;
using Admin.Server.Repositories.ExamSubjects;
using Admin.Server.Repositories.Mcqs;
using Admin.Server.Repositories.Topics;
using Admin.Server.Repositories.Constants;
using Admin.Server.Repositories.Reports;
using Admin.Server.Repositories.AdminDashboards;
using Admin.Server.Repositories.Announcements;
using Admin.Server.Repositories.Instructors;
using Admin.Server.Repositories.InstructorSubjects;
using Admin.Server.Repositories.FrontEnd.AppUsers;
using Admin.Server.Repositories.FrontEnd.SubjectEnrollment;
using Admin.Server.Repositories.FrontEnd.PaperOne;
using Admin.Server.Repositories.FrontEnd.UserProgression;
using Admin.Server.Repositories.FrontEnd.McqPastpaper;
using Admin.Server.Repositories.FrontEnd.ETQ;
using Admin.Server.Repositories.Chapters;
using Admin.Server.Repositories.ChaptersBySubjectId;
using Admin.Server.Repositories.Lessons;
using Admin.Server.Repositories.Videos;
using Admin.Server.Repositories.Downloads;
using Admin.Server.Repositories.LessonsByChapterId;
using Admin.Server.Repositories.Objectives;
using Admin.Server.Repositories.FrontEnd.Tutorials;
using Admin.Server.Repositories.Conversations;
using Admin.Server.Repositories.FrontEnd.Votes;
using Admin.Server.Repositories.FrontEnd.SubjectDownloads;
using Admin.Server.Repositories.FrontEnd.McqReports;
using Admin.Server.Repositories.FrontEnd.Payments;
using Admin.Server.Repositories.Olympiads;
using Admin.Server.Repositories.FrontEnd.UserQuiz;
using Admin.Server.Repositories.FrontEnd.Quizes;
using Admin.Server.Repositories.ExamCategory;
using Admin.Server.Repositories.FrontEnd.Explores;
using Admin.Shared.Models;
using Admin.Server.Repositories.Address;
using Admin.Server.Repositories.FrontEnd.QuizAwards;
using Admin.Server.Repositories.FrontEnd.Pastpaperquizawards;

namespace Admin.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

            //Personal
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //Personal: User.Identity.Name
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options => 
            {
                options.TokenValidationParameters.NameClaimType = "sub";
                options.TokenValidationParameters.NameClaimType = "name";
            });
            //services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IExaminationRepository, ExaminationtRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IExamSubjectRepository, ExamSubjectRepository>();
            services.AddScoped<IPastPaperRepository, PastPaperRepository>();
            services.AddScoped<IMCQRepository, MCQRepository>();
            services.AddScoped<ITopicsRepository, TopicsRepository>();
            services.AddScoped<IConstantsRepository, ConstantsRepository>();
            services.AddScoped<IMcqReportRepository, McqReportRepository>();
            services.AddScoped<IAdminDashboardRepository, AdminDashboardRepository>();

            services.AddScoped<IExaminationClientRepository, ExaminationClientRepository>();
            services.AddScoped<IAnnouncemntRepository, AnnouncementRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IInstructorSubjectRepository, InstructorSubjectRepository>();
            services.AddScoped<ISubjectEnrollmentRepository, SubjectEnrollmentRepository>();
            services.AddScoped<IAppUsersRepository, AppUsersRepository>();
            services.AddScoped<IPaperOnesRepository, PaperOnesRepository>();
            services.AddScoped<IMcqPastpersRepository, McqPastpersRepository>();
            services.AddScoped<IUserProgressionRepository, UserProgressionRepository>();
            services.AddScoped<IPastPaper2n3Repository, PastPaper2n3Repository>();
            services.AddScoped<IETQRepository, ETQRepository>();
            services.AddScoped<IChaptersRepository, ChaptersRepository>();
            services.AddScoped<ILessonsRepository, LessonsRepository>();
            services.AddScoped<IChaptersBySubjectIddRepository, ChaptersBySubjectIdRepository>();
            services.AddScoped<IVideosRepository, VideosRepository>();
            services.AddScoped<ILessonsByChapterIdRepository, LessonsByChapterIdRepository>();
            services.AddScoped<IDownloadsRepository, DownloadsRepository>();
            services.AddScoped<IObjectivesRepository, ObjectivesRepository>();
            services.AddScoped<ITutorialsRepository, TutorialsRepository>();
            services.AddScoped<IConversationsRepository, ConversationsRepository>();
            services.AddScoped<IVotesRepository, VotesRepository>();
            services.AddScoped<ISubjectDownloadsRepository, SubjectDownloadsRepository>();
            services.AddScoped<IMcqReportsRepository, McqReportsRepository>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
            services.AddScoped<IOlympiadRepository, OlympiadRepository>();
            services.AddScoped<IUserQuizRepository, UserQuizRepository>();
            services.AddScoped<IPastPapersQuizesRepository, PastPapersQuizesRepository>();
            services.AddScoped<IExamCategoryRepository, ExamCategoryRepository>();
            services.AddScoped<IExploreRepository, ExploreRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IQuizAwardsRepository, QuizAwardsRepository>();
            services.AddScoped<IPastpaperquizawardsRepository, PastpaperquizawardsRepository>();

            //End of Personal

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddRazorPages();

            //PERSONAL ADDED CONFIGURATION
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000", "https://ogabook.com", "https://etoederick-001-site1.htempurl.com",
                                                          "https://etoederick-001-site2.htempurl.com").AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable middleware to serve generated Swagger as a JSON endpoint


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //To enable local http connect to xamarin forms. Enable before redeploying
            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            //PERSONAL
            app.UseCors(MyAllowSpecificOrigins);
            //PERSONAL

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
