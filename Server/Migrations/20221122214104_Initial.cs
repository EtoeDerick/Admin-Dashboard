using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RegionOfOrigin = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    Town = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    School = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfDaysToExamination = table.Column<int>(type: "int", nullable: false),
                    ExaminationId = table.Column<int>(type: "int", nullable: false),
                    ExaminationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamDaysLeftBgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnouncementTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnouncementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label1Sub1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label1Sub2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label1Sub3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label2Sub1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label2Sub2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label2Sub3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowToUseOgaBookVideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line1ContactWithWhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line2Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Credit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MessageDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ReplyId = table.Column<int>(type: "int", nullable: false),
                    IsAReply = table.Column<bool>(type: "bit", nullable: false),
                    PriorityNumber = table.Column<int>(type: "int", nullable: false),
                    IsNotApproved = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DiscussionForumId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DownloadTrackingTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDownloaded = table.Column<bool>(type: "bit", nullable: false),
                    PastPaperId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DownloadSize = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadTrackingTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CategoryBgColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CategoryTextColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedBacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedBackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DiscountCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MCQReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    McqId = table.Column<int>(type: "int", nullable: false),
                    PastPaperId = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Report = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", maxLength: 12048, nullable: true),
                    QuestionPosition = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsReported = table.Column<bool>(type: "bit", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCQReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelephoneLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelephoneLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    MentorBio = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    From = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    External_reference = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SubjectId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TransactionStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Ussd_code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Operator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizAwards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastPaperId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAwards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizOwners",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MarqueeImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubjectExamNickName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAudits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProgressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PastPaperId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaperNumber = table.Column<int>(type: "int", nullable: false),
                    QuestionPosition = table.Column<int>(type: "int", nullable: false),
                    PaperYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicNum = table.Column<int>(type: "int", nullable: false),
                    AnswerStatus = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgressions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    WrittenDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSubjectAudits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubjectAudits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false),
                    IsUnliked = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => new { x.UserId, x.ConversationId });
                    table.ForeignKey(
                        name: "FK_Votes_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examination",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    QuestionRange = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExamType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    WrittenOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examination_ExamCategories_ExamCategoryId",
                        column: x => x.ExamCategoryId,
                        principalTable: "ExamCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMentors",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MentorId = table.Column<int>(type: "int", nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMentors", x => new { x.AppUserId, x.MentorId });
                    table.ForeignKey(
                        name: "FK_UserMentors_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMentors_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MarqueeImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyPrice = table.Column<int>(type: "int", nullable: false),
                    TenMonths = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubjectExamNickName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    VideoPreviewUrl = table.Column<string>(type: "nvarchar(1220)", maxLength: 1220, nullable: true),
                    IsPaper1ContentAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsPaper2ContentAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsPaper3ContentAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsTutorialContentAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ExaminationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Examination_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChapterTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChapterPriorityNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LectureCount = table.Column<int>(type: "int", nullable: false),
                    TotalLectureDuration = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapters_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Downloadpdfs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaperYear = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PaperNumber = table.Column<int>(type: "int", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Downloadpdfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Downloadpdfs_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorSubject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Commission = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSubject", x => new { x.InstructorId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_InstructorSubject_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PastPaper",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaperYear = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PaperNumber = table.Column<int>(type: "int", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDownloaded = table.Column<bool>(type: "bit", nullable: false),
                    CorrectAnswerCount = table.Column<int>(type: "int", nullable: false),
                    DownloadSize = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WrittenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    Visibility = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    QuizOwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizPassCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    QuizNumber = table.Column<int>(type: "int", nullable: false),
                    IsQuiz = table.Column<bool>(type: "bit", nullable: false),
                    IsRightWrong = table.Column<bool>(type: "bit", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastPaper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PastPaper_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    TopicNum = table.Column<int>(type: "int", nullable: false),
                    IsAlsoP3Topic = table.Column<bool>(type: "bit", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topic_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tutorial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Chapter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutorial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutorial_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSubject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    LastUsedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubject", x => new { x.AppUserId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_UserSubject_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonNumber = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    LessonTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LectureCount = table.Column<int>(type: "int", nullable: false),
                    TotalLectureDuration = table.Column<int>(type: "int", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objectives_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EssayTypeQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasUniqueSolution = table.Column<bool>(type: "bit", nullable: false),
                    TotalMarks = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlBeforeIntroduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlAfterIntroduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastPaperId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayTypeQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EssayTypeQuestions_PastPaper_PastPaperId",
                        column: x => x.PastPaperId,
                        principalTable: "PastPaper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    PdfTutorialUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtmlEncodedNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    LikesCount = table.Column<int>(type: "int", nullable: false),
                    UnlikesCount = table.Column<int>(type: "int", nullable: false),
                    Commentscount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoDownloads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    DownloadTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownloadUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPastPaper = table.Column<bool>(type: "bit", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastPaperNumber = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoDownloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoDownloads_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ETQQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasUniqueSolution = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlBeforeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlAfterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EssayTypeQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETQQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ETQQuestions_EssayTypeQuestions_EssayTypeQuestionId",
                        column: x => x.EssayTypeQuestionId,
                        principalTable: "EssayTypeQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MCQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionImageUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Answer = table.Column<int>(type: "int", nullable: false),
                    AnswerProvided = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MultipleAnswers = table.Column<bool>(type: "bit", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    correctAnswer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    JustificationText = table.Column<string>(type: "nvarchar(max)", maxLength: 10024, nullable: true),
                    JustificationImageUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PastPaperId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VideoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCQ", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MCQ_PastPaper_PastPaperId",
                        column: x => x.PastPaperId,
                        principalTable: "PastPaper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MCQ_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MCQ_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ETQSubquestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ImageUrlBeforeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlAfterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETQSubquestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ETQSubquestions_ETQQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "ETQQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSolution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionSolution_ETQQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "ETQQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ans = table.Column<int>(type: "int", nullable: false),
                    MCQId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_MCQ_MCQId",
                        column: x => x.MCQId,
                        principalTable: "MCQ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mcqOption = table.Column<string>(type: "nvarchar(max)", maxLength: 4024, nullable: true),
                    MCQId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Option_MCQ_MCQId",
                        column: x => x.MCQId,
                        principalTable: "MCQ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionImgUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    MCQId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionImage_MCQ_MCQId",
                        column: x => x.MCQId,
                        principalTable: "MCQ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ETQSolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SubquestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETQSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ETQSolutions_ETQSubquestions_SubquestionId",
                        column: x => x.SubquestionId,
                        principalTable: "ETQSubquestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_MCQId",
                table: "Answer",
                column: "MCQId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_SubjectId",
                table: "Chapters",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Downloadpdfs_SubjectId",
                table: "Downloadpdfs",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EssayTypeQuestions_PastPaperId",
                table: "EssayTypeQuestions",
                column: "PastPaperId");

            migrationBuilder.CreateIndex(
                name: "IX_ETQQuestions_EssayTypeQuestionId",
                table: "ETQQuestions",
                column: "EssayTypeQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ETQSolutions_SubquestionId",
                table: "ETQSolutions",
                column: "SubquestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ETQSubquestions_QuestionId",
                table: "ETQSubquestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Examination_ExamCategoryId",
                table: "Examination",
                column: "ExamCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubject_SubjectId",
                table: "InstructorSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ChapterId",
                table: "Lessons",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQ_PastPaperId",
                table: "MCQ",
                column: "PastPaperId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQ_SubjectId",
                table: "MCQ",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQ_VideoId",
                table: "MCQ",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ChapterId",
                table: "Objectives",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_MCQId",
                table: "Option",
                column: "MCQId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionImage_MCQId",
                table: "OptionImage",
                column: "MCQId");

            migrationBuilder.CreateIndex(
                name: "IX_PastPaper_SubjectID",
                table: "PastPaper",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSolution_QuestionId",
                table: "QuestionSolution",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ExaminationId",
                table: "Subject",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_SubjectId",
                table: "Topic",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutorial_SubjectId",
                table: "Tutorial",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMentors_MentorId",
                table: "UserMentors",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubject_SubjectId",
                table: "UserSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_LessonId",
                table: "Video",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoDownloads_LessonId",
                table: "VideoDownloads",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ConversationId",
                table: "Votes",
                column: "ConversationId");
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Constants");

            migrationBuilder.DropTable(
                name: "Downloadpdfs");

            migrationBuilder.DropTable(
                name: "DownloadTrackingTables");

            migrationBuilder.DropTable(
                name: "ETQSolutions");

            migrationBuilder.DropTable(
                name: "FeedBacks");

            migrationBuilder.DropTable(
                name: "InstructorSubject");

            migrationBuilder.DropTable(
                name: "MCQReports");

            migrationBuilder.DropTable(
                name: "Objectives");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "OptionImage");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "QuestionSolution");

            migrationBuilder.DropTable(
                name: "QuizAwards");

            migrationBuilder.DropTable(
                name: "QuizOwners");

            migrationBuilder.DropTable(
                name: "SubjectAudits");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Tutorial");

            migrationBuilder.DropTable(
                name: "UserMentors");

            migrationBuilder.DropTable(
                name: "UserProgressions");

            migrationBuilder.DropTable(
                name: "UserQuizzes");

            migrationBuilder.DropTable(
                name: "UserSubject");

            migrationBuilder.DropTable(
                name: "UserSubjectAudits");

            migrationBuilder.DropTable(
                name: "VideoDownloads");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "ETQSubquestions");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "MCQ");

            migrationBuilder.DropTable(
                name: "Mentors");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "ETQQuestions");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "EssayTypeQuestions");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "PastPaper");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Examination");

            migrationBuilder.DropTable(
                name: "ExamCategories");
        }
    }
}
