IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Addresses] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NULL,
        [UserName] nvarchar(250) NULL,
        [RegionOfOrigin] nvarchar(24) NULL,
        [Town] nvarchar(24) NULL,
        [Phone] nvarchar(24) NULL,
        [School] nvarchar(100) NULL,
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Announcements] (
        [Id] int NOT NULL IDENTITY,
        [NumberOfDaysToExamination] int NOT NULL,
        [ExaminationId] int NOT NULL,
        [ExaminationTitle] nvarchar(max) NULL,
        [ExamDaysLeftBgColor] nvarchar(max) NULL,
        [AnnouncementTitle] nvarchar(max) NULL,
        [AnnouncementDescription] nvarchar(max) NULL,
        [Label1Sub1] nvarchar(max) NULL,
        [Label1Sub2] nvarchar(max) NULL,
        [Label1Sub3] nvarchar(max) NULL,
        [Label2Sub1] nvarchar(max) NULL,
        [Label2Sub2] nvarchar(max) NULL,
        [Label2Sub3] nvarchar(max) NULL,
        [HowToUseOgaBookVideoUrl] nvarchar(max) NULL,
        [VideoTitle] nvarchar(max) NULL,
        [EmailContact] nvarchar(max) NULL,
        [Line1ContactWithWhatsApp] nvarchar(max) NULL,
        [Line2Contact] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_Announcements] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [AppUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(100) NULL,
        [Phone] nvarchar(15) NULL,
        [Credit] int NOT NULL,
        CONSTRAINT [PK_AppUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Constants] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(250) NULL,
        [Key] nvarchar(250) NULL,
        [Value] nvarchar(250) NULL,
        [Code] nvarchar(250) NULL,
        [Description] nvarchar(1024) NULL,
        CONSTRAINT [PK_Constants] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Conversations] (
        [Id] int NOT NULL IDENTITY,
        [Date] datetime2 NOT NULL,
        [MessageTitle] nvarchar(200) NULL,
        [MessageDescription] nvarchar(2000) NULL,
        [ReplyId] int NOT NULL,
        [IsAReply] bit NOT NULL,
        [PriorityNumber] int NOT NULL,
        [IsNotApproved] bit NOT NULL,
        [UserId] nvarchar(1024) NULL,
        [DiscussionForumId] int NOT NULL,
        [SubjectId] int NOT NULL,
        CONSTRAINT [PK_Conversations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [DownloadTrackingTables] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NULL,
        [ObjectId] nvarchar(max) NULL,
        [IsDownloaded] bit NOT NULL,
        [PastPaperId] nvarchar(max) NULL,
        [Date] datetime2 NOT NULL,
        [DownloadSize] nvarchar(max) NULL,
        CONSTRAINT [PK_DownloadTrackingTables] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [ExamCategories] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(200) NULL,
        [Description] nvarchar(500) NULL,
        [Code] nvarchar(50) NULL,
        [CategoryBgColor] nvarchar(50) NULL,
        [CategoryTextColor] nvarchar(50) NULL,
        CONSTRAINT [PK_ExamCategories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [FeedBacks] (
        [Id] int NOT NULL IDENTITY,
        [FeedBackDate] datetime2 NOT NULL,
        [Description] nvarchar(2048) NOT NULL,
        [AppUserId] nvarchar(max) NULL,
        CONSTRAINT [PK_FeedBacks] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Instructor] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(80) NOT NULL,
        [Description] nvarchar(255) NULL,
        [ImageUrl] nvarchar(1024) NULL,
        [DiscountCode] nvarchar(10) NULL,
        CONSTRAINT [PK_Instructor] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [MCQReports] (
        [Id] int NOT NULL IDENTITY,
        [McqId] int NOT NULL,
        [PastPaperId] nvarchar(24) NULL,
        [SubjectId] int NOT NULL,
        [Report] nvarchar(1024) NULL,
        [Response] nvarchar(max) NULL,
        [QuestionPosition] int NOT NULL,
        [UserId] nvarchar(1024) NULL,
        [IsReported] bit NOT NULL,
        [IsResolved] bit NOT NULL,
        [ReportDate] datetime2 NOT NULL,
        [ResolveDate] datetime2 NOT NULL,
        CONSTRAINT [PK_MCQReports] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Mentors] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NULL,
        [TelephoneLine1] nvarchar(100) NULL,
        [TelephoneLine2] nvarchar(100) NULL,
        [Email] nvarchar(100) NULL,
        [Description] nvarchar(2048) NULL,
        [MentorBio] nvarchar(1024) NULL,
        CONSTRAINT [PK_Mentors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Payments] (
        [Id] int NOT NULL IDENTITY,
        [Amount] nvarchar(250) NULL,
        [DurationInDays] int NOT NULL,
        [Currency] nvarchar(250) NULL,
        [From] nvarchar(250) NULL,
        [Description] nvarchar(1024) NULL,
        [External_reference] nvarchar(250) NULL,
        [UserId] nvarchar(250) NULL,
        [SubjectId] nvarchar(250) NULL,
        [Status] nvarchar(250) NULL,
        [TransactionStatus] nvarchar(250) NULL,
        [PaymentDate] datetime2 NOT NULL,
        [Reference] nvarchar(250) NULL,
        [Ussd_code] nvarchar(250) NULL,
        [Operator] nvarchar(250) NULL,
        [Action] nvarchar(250) NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [QuizAwards] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NULL,
        [PastPaperId] nvarchar(max) NULL,
        [StudentImageUrl] nvarchar(max) NULL,
        [AwardedDate] datetime2 NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_QuizAwards] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [QuizOwners] (
        [Id] nvarchar(450) NOT NULL,
        [Title] nvarchar(100) NULL,
        [LogoUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_QuizOwners] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [SubjectAudits] (
        [Id] int NOT NULL IDENTITY,
        [SubjectId] int NOT NULL,
        [Title] nvarchar(80) NOT NULL,
        [Description] nvarchar(1024) NULL,
        [ImageUrl] nvarchar(255) NULL,
        [MarqueeImageUrl] nvarchar(255) NULL,
        [Price] decimal(18,2) NOT NULL,
        [Year] nvarchar(50) NULL,
        [SubjectExamNickName] nvarchar(20) NULL,
        [Category] nvarchar(20) NULL,
        [IsFree] bit NOT NULL,
        [IsApproved] bit NOT NULL,
        [AuditDate] datetime2 NOT NULL,
        CONSTRAINT [PK_SubjectAudits] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [UserProgressions] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NULL,
        [SubjectId] int NOT NULL,
        [PastPaperId] nvarchar(max) NULL,
        [PaperNumber] int NOT NULL,
        [QuestionPosition] int NOT NULL,
        [PaperYear] nvarchar(max) NULL,
        [TopicNum] int NOT NULL,
        [AnswerStatus] int NOT NULL,
        [DateTime] datetime2 NOT NULL,
        CONSTRAINT [PK_UserProgressions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [UserQuizzes] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NULL,
        [QuizId] nvarchar(max) NULL,
        [Score] int NOT NULL,
        [WrittenDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserQuizzes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [UserSubjectAudits] (
        [Id] int NOT NULL IDENTITY,
        [IsExpired] bit NOT NULL,
        [EnrollmentDate] datetime2 NOT NULL,
        [ExpiryDate] datetime2 NOT NULL,
        [Status] nvarchar(10) NULL,
        [Price] decimal(18,2) NOT NULL,
        [SubjectId] int NOT NULL,
        [ApplicationUserId] nvarchar(max) NULL,
        [AuditDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserSubjectAudits] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Votes] (
        [UserId] nvarchar(200) NOT NULL,
        [ConversationId] int NOT NULL,
        [IsLiked] bit NOT NULL,
        [IsUnliked] bit NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_Votes] PRIMARY KEY ([UserId], [ConversationId]),
        CONSTRAINT [FK_Votes_Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [Conversations] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Examination] (
        [Id] nvarchar(450) NOT NULL,
        [Title] nvarchar(250) NOT NULL,
        [Description] nvarchar(255) NOT NULL,
        [ImageUrl] nvarchar(1024) NULL,
        [QuestionRange] nvarchar(50) NULL,
        [ExamType] nvarchar(10) NULL,
        [IsApproved] bit NOT NULL,
        [WrittenOn] datetime2 NOT NULL,
        [ExamCategoryId] int NOT NULL,
        CONSTRAINT [PK_Examination] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Examination_ExamCategories_ExamCategoryId] FOREIGN KEY ([ExamCategoryId]) REFERENCES [ExamCategories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [UserMentors] (
        [AppUserId] nvarchar(450) NOT NULL,
        [MentorId] int NOT NULL,
        [DateAssigned] datetime2 NOT NULL,
        CONSTRAINT [PK_UserMentors] PRIMARY KEY ([AppUserId], [MentorId]),
        CONSTRAINT [FK_UserMentors_AppUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserMentors_Mentors_MentorId] FOREIGN KEY ([MentorId]) REFERENCES [Mentors] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Subject] (
        [Id] int NOT NULL,
        [Title] nvarchar(80) NOT NULL,
        [Description] nvarchar(1024) NULL,
        [ImageUrl] nvarchar(255) NULL,
        [MarqueeImageUrl] nvarchar(255) NULL,
        [Price] decimal(18,2) NOT NULL,
        [MonthlyPrice] int NOT NULL,
        [TenMonths] int NOT NULL,
        [Year] nvarchar(50) NULL,
        [SubjectExamNickName] nvarchar(20) NULL,
        [Category] nvarchar(20) NULL,
        [IsFree] bit NOT NULL,
        [IsApproved] bit NOT NULL,
        [VideoPreviewUrl] nvarchar(1220) NULL,
        [IsPaper1ContentAvailable] bit NOT NULL,
        [IsPaper2ContentAvailable] bit NOT NULL,
        [IsPaper3ContentAvailable] bit NOT NULL,
        [IsTutorialContentAvailable] bit NOT NULL,
        [ExaminationId] nvarchar(450) NULL,
        CONSTRAINT [PK_Subject] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Subject_Examination_ExaminationId] FOREIGN KEY ([ExaminationId]) REFERENCES [Examination] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Chapters] (
        [Id] int NOT NULL IDENTITY,
        [ChapterTitle] nvarchar(max) NULL,
        [ChapterPriorityNumber] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [LectureCount] int NOT NULL,
        [TotalLectureDuration] int NOT NULL,
        [SubjectId] int NOT NULL,
        CONSTRAINT [PK_Chapters] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Chapters_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Downloadpdfs] (
        [Id] nvarchar(450) NOT NULL,
        [Title] nvarchar(50) NOT NULL,
        [PaperYear] nvarchar(30) NOT NULL,
        [PaperNumber] int NOT NULL,
        [Thumbnail] nvarchar(2048) NULL,
        [Url] nvarchar(1024) NULL,
        [IsApproved] bit NOT NULL,
        [IsFree] bit NOT NULL,
        [SubjectId] int NOT NULL,
        CONSTRAINT [PK_Downloadpdfs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Downloadpdfs_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [InstructorSubject] (
        [SubjectId] int NOT NULL,
        [InstructorId] nvarchar(450) NOT NULL,
        [Commission] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_InstructorSubject] PRIMARY KEY ([InstructorId], [SubjectId]),
        CONSTRAINT [FK_InstructorSubject_Instructor_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [Instructor] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_InstructorSubject_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [PastPaper] (
        [Id] nvarchar(450) NOT NULL,
        [Title] nvarchar(50) NOT NULL,
        [PaperYear] nvarchar(30) NOT NULL,
        [PaperNumber] int NOT NULL,
        [Thumbnail] nvarchar(max) NULL,
        [Url] nvarchar(1024) NULL,
        [IsApproved] bit NOT NULL,
        [IsFree] bit NOT NULL,
        [Quantity] int NOT NULL,
        [IsDownloaded] bit NOT NULL,
        [CorrectAnswerCount] int NOT NULL,
        [DownloadSize] bigint NOT NULL,
        [Status] nvarchar(max) NULL,
        [WrittenDate] datetime2 NOT NULL,
        [DurationInMinutes] int NOT NULL,
        [Visibility] nvarchar(20) NULL,
        [QuizOwnerId] nvarchar(max) NULL,
        [QuizPassCode] nvarchar(256) NULL,
        [QuizNumber] int NOT NULL,
        [IsQuiz] bit NOT NULL,
        [IsRightWrong] bit NOT NULL,
        [SubjectID] int NOT NULL,
        CONSTRAINT [PK_PastPaper] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PastPaper_Subject_SubjectID] FOREIGN KEY ([SubjectID]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Topic] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(120) NULL,
        [TopicNum] int NOT NULL,
        [IsAlsoP3Topic] bit NOT NULL,
        [SubjectId] int NOT NULL,
        CONSTRAINT [PK_Topic] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Topic_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Tutorial] (
        [Id] int NOT NULL IDENTITY,
        [TopicId] int NOT NULL,
        [Chapter] nvarchar(50) NULL,
        [Description] nvarchar(255) NULL,
        [Content] nvarchar(max) NULL,
        [SubjectId] int NOT NULL,
        CONSTRAINT [PK_Tutorial] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tutorial_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [UserSubject] (
        [SubjectId] int NOT NULL,
        [AppUserId] nvarchar(450) NOT NULL,
        [IsExpired] bit NOT NULL,
        [EnrollmentDate] datetime2 NOT NULL,
        [ExpiryDate] datetime2 NOT NULL,
        [Status] nvarchar(10) NULL,
        [Price] decimal(18,2) NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Duration] int NOT NULL,
        [LastUsedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_UserSubject] PRIMARY KEY ([AppUserId], [SubjectId]),
        CONSTRAINT [FK_UserSubject_AppUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserSubject_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Lessons] (
        [Id] int NOT NULL IDENTITY,
        [LessonNumber] int NOT NULL,
        [SubjectId] int NOT NULL,
        [LessonTitle] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [LectureCount] int NOT NULL,
        [TotalLectureDuration] int NOT NULL,
        [ChapterId] int NOT NULL,
        CONSTRAINT [PK_Lessons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Lessons_Chapters_ChapterId] FOREIGN KEY ([ChapterId]) REFERENCES [Chapters] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Objectives] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [ChapterId] int NOT NULL,
        CONSTRAINT [PK_Objectives] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Objectives_Chapters_ChapterId] FOREIGN KEY ([ChapterId]) REFERENCES [Chapters] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [EssayTypeQuestions] (
        [Id] int NOT NULL IDENTITY,
        [HasUniqueSolution] bit NOT NULL,
        [TotalMarks] int NOT NULL,
        [Position] int NOT NULL,
        [Introduction] nvarchar(max) NULL,
        [ImageUrlBeforeIntroduction] nvarchar(max) NULL,
        [ImageUrlAfterIntroduction] nvarchar(max) NULL,
        [VideoUrl] nvarchar(max) NULL,
        [PastPaperId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_EssayTypeQuestions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_EssayTypeQuestions_PastPaper_PastPaperId] FOREIGN KEY ([PastPaperId]) REFERENCES [PastPaper] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Video] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(80) NOT NULL,
        [Description] nvarchar(2048) NULL,
        [Duration] int NOT NULL,
        [Thumbnail] nvarchar(1024) NULL,
        [Url] nvarchar(1024) NULL,
        [PdfTutorialUrl] nvarchar(max) NULL,
        [HtmlEncodedNotes] nvarchar(max) NULL,
        [IsFree] bit NOT NULL,
        [Position] int NOT NULL,
        [ViewsCount] int NOT NULL,
        [LikesCount] int NOT NULL,
        [UnlikesCount] int NOT NULL,
        [Commentscount] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [TopicId] int NOT NULL,
        [SubjectId] int NOT NULL,
        [LessonId] int NOT NULL,
        CONSTRAINT [PK_Video] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Video_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [VideoDownloads] (
        [Id] int NOT NULL IDENTITY,
        [SubjectId] int NOT NULL,
        [DownloadTitle] nvarchar(max) NULL,
        [DownloadUrl] nvarchar(max) NULL,
        [IsPastPaper] bit NOT NULL,
        [Year] nvarchar(max) NULL,
        [PastPaperNumber] int NOT NULL,
        [LessonId] int NOT NULL,
        CONSTRAINT [PK_VideoDownloads] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VideoDownloads_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [ETQQuestions] (
        [Id] int NOT NULL IDENTITY,
        [HasUniqueSolution] bit NOT NULL,
        [Position] int NOT NULL,
        [Text] nvarchar(max) NULL,
        [ImageUrlBeforeText] nvarchar(max) NULL,
        [ImageUrlAfterText] nvarchar(max) NULL,
        [Marks] int NOT NULL,
        [TopicId] int NOT NULL,
        [VideoUrl] nvarchar(max) NULL,
        [EssayTypeQuestionId] int NOT NULL,
        CONSTRAINT [PK_ETQQuestions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ETQQuestions_EssayTypeQuestions_EssayTypeQuestionId] FOREIGN KEY ([EssayTypeQuestionId]) REFERENCES [EssayTypeQuestions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [MCQ] (
        [Id] int NOT NULL IDENTITY,
        [Question] nvarchar(max) NULL,
        [QuestionImageUrl] nvarchar(1024) NULL,
        [Answer] int NOT NULL,
        [AnswerProvided] nvarchar(255) NULL,
        [MultipleAnswers] bit NOT NULL,
        [IsAnonymous] bit NOT NULL,
        [correctAnswer] nvarchar(255) NULL,
        [JustificationText] nvarchar(max) NULL,
        [JustificationImageUrl] nvarchar(1024) NULL,
        [VideoUrl] nvarchar(1024) NULL,
        [Instruction] nvarchar(max) NULL,
        [TopicId] int NOT NULL,
        [Position] int NOT NULL,
        [SubjectId] int NOT NULL,
        [PastPaperId] nvarchar(450) NULL,
        [VideoId] int NULL,
        CONSTRAINT [PK_MCQ] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MCQ_PastPaper_PastPaperId] FOREIGN KEY ([PastPaperId]) REFERENCES [PastPaper] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_MCQ_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MCQ_Video_VideoId] FOREIGN KEY ([VideoId]) REFERENCES [Video] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [ETQSubquestions] (
        [Id] int NOT NULL IDENTITY,
        [Text] nvarchar(max) NULL,
        [Position] int NOT NULL,
        [ImageUrlBeforeText] nvarchar(max) NULL,
        [ImageUrlAfterText] nvarchar(max) NULL,
        [Marks] int NOT NULL,
        [TopicId] int NOT NULL,
        [VideoUrl] nvarchar(max) NULL,
        [QuestionId] int NOT NULL,
        CONSTRAINT [PK_ETQSubquestions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ETQSubquestions_ETQQuestions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [ETQQuestions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [QuestionSolution] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NULL,
        [ImageUrl] nvarchar(max) NULL,
        [Position] int NOT NULL,
        [QuestionId] int NOT NULL,
        CONSTRAINT [PK_QuestionSolution] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_QuestionSolution_ETQQuestions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [ETQQuestions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Answer] (
        [Id] int NOT NULL IDENTITY,
        [Ans] int NOT NULL,
        [MCQId] int NOT NULL,
        CONSTRAINT [PK_Answer] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Answer_MCQ_MCQId] FOREIGN KEY ([MCQId]) REFERENCES [MCQ] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [Option] (
        [Id] int NOT NULL IDENTITY,
        [mcqOption] nvarchar(max) NULL,
        [MCQId] int NOT NULL,
        CONSTRAINT [PK_Option] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Option_MCQ_MCQId] FOREIGN KEY ([MCQId]) REFERENCES [MCQ] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [OptionImage] (
        [Id] int NOT NULL IDENTITY,
        [OptionImgUrl] nvarchar(1024) NULL,
        [MCQId] int NOT NULL,
        CONSTRAINT [PK_OptionImage] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OptionImage_MCQ_MCQId] FOREIGN KEY ([MCQId]) REFERENCES [MCQ] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE TABLE [ETQSolutions] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NULL,
        [ImageUrl] nvarchar(max) NULL,
        [Position] int NOT NULL,
        [SubquestionId] int NOT NULL,
        CONSTRAINT [PK_ETQSolutions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ETQSolutions_ETQSubquestions_SubquestionId] FOREIGN KEY ([SubquestionId]) REFERENCES [ETQSubquestions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Answer_MCQId] ON [Answer] ([MCQId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Chapters_SubjectId] ON [Chapters] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Downloadpdfs_SubjectId] ON [Downloadpdfs] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_EssayTypeQuestions_PastPaperId] ON [EssayTypeQuestions] ([PastPaperId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_ETQQuestions_EssayTypeQuestionId] ON [ETQQuestions] ([EssayTypeQuestionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_ETQSolutions_SubquestionId] ON [ETQSolutions] ([SubquestionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_ETQSubquestions_QuestionId] ON [ETQSubquestions] ([QuestionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Examination_ExamCategoryId] ON [Examination] ([ExamCategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_InstructorSubject_SubjectId] ON [InstructorSubject] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Lessons_ChapterId] ON [Lessons] ([ChapterId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_MCQ_PastPaperId] ON [MCQ] ([PastPaperId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_MCQ_SubjectId] ON [MCQ] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_MCQ_VideoId] ON [MCQ] ([VideoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Objectives_ChapterId] ON [Objectives] ([ChapterId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Option_MCQId] ON [Option] ([MCQId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_OptionImage_MCQId] ON [OptionImage] ([MCQId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_PastPaper_SubjectID] ON [PastPaper] ([SubjectID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_QuestionSolution_QuestionId] ON [QuestionSolution] ([QuestionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Subject_ExaminationId] ON [Subject] ([ExaminationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Topic_SubjectId] ON [Topic] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Tutorial_SubjectId] ON [Tutorial] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_UserMentors_MentorId] ON [UserMentors] ([MentorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_UserSubject_SubjectId] ON [UserSubject] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Video_LessonId] ON [Video] ([LessonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_VideoDownloads_LessonId] ON [VideoDownloads] ([LessonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    CREATE INDEX [IX_Votes_ConversationId] ON [Votes] ([ConversationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122214104_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221122214104_Initial', N'5.0.9');
END;
GO

COMMIT;
GO

