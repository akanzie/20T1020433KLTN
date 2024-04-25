USE [QuanLyKyThi]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 24/04/2024 4:42:34 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] NOT NULL,
	[Body] [nvarchar](255) NULL,
	[TeacherId] [nchar](10) NULL,
	[SubmissionId] [int] NULL,
	[CommentedTime] [datetime] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [nchar](10) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubmissionFiles]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubmissionFiles](
	[FileId] [uniqueidentifier] NOT NULL,
	[FIleName] [nvarchar](max) NOT NULL,
	[FilePath] [nvarchar](max) NOT NULL,
	[MimeType] [nvarchar](50) NOT NULL,
	[Size] [decimal](18, 0) NULL,
	[SubmissionId] [int] NOT NULL,
	[OriginalName] [nvarchar](max) NULL,
 CONSTRAINT [PK_SubmissionFiles] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Submissions]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Submissions](
	[SubmissionId] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[StudentId] [nchar](10) NULL,
	[IPAddress] [nchar](15) NULL,
	[SubmittedTime] [datetime] NOT NULL,
	[Status] [nvarchar](20) NULL,
 CONSTRAINT [PK_Submission] PRIMARY KEY CLUSTERED 
(
	[SubmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[TeacherId] [nchar](10) NOT NULL,
	[TeacherName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TestFiles]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestFiles](
	[FileId] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[FilePath] [nvarchar](max) NOT NULL,
	[MimeType] [nvarchar](50) NOT NULL,
	[Size] [decimal](18, 0) NULL,
	[TestId] [int] NOT NULL,
	[OriginalName] [nvarchar](max) NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tests]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[TestId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Instruction] [nvarchar](max) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[IsCheckIP] [bit] NOT NULL,
	[IsConductedAtSchool] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
	[TestType] [nvarchar](20) NULL,
	[TeacherId] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'64cffb3e-6c86-4257-b2ed-0b71278d0e2e', N'64cffb3e-6c86-4257-b2ed-0b71278d0e2e_giaynhanxetthuctap.docx', N'D:\File\Thi kết thúc học phần - Nhập môn - Nhóm 3\Submission\64cffb3e-6c86-4257-b2ed-0b71278d0e2e_giaynhanxetthuctap.docx', N'application/vnd.openxmlformats-officedocument.word', CAST(19386 AS Decimal(18, 0)), 1006, N'giaynhanxetthuctap.docx')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'8e78f9cb-4737-4be6-8c97-b833f8b88993', N'8e78f9cb-4737-4be6-8c97-b833f8b88993_xemkythi.jpg', N'D:\File\Thi kết thúc học phần - Lập trình web - Nhóm 1,2\Submission\8e78f9cb-4737-4be6-8c97-b833f8b88993_xemkythi.jpg', N'image/jpeg', CAST(34580 AS Decimal(18, 0)), 2, N'xemkythi.jpg')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'92d30a22-0b9a-4b1d-a2da-d45bdca25521', N'92d30a22-0b9a-4b1d-a2da-d45bdca25521_nopbai (1).jpg', N'D:\File\Thi kết thúc học phần - Lập trình web - Nhóm 1,2\Submission\92d30a22-0b9a-4b1d-a2da-d45bdca25521_nopbai (1).jpg', N'image/jpeg', CAST(55878 AS Decimal(18, 0)), 2, N'nopbai (1).jpg')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'3e4bca4f-3001-442b-b444-e5dc9c5dfa19', N'3e4bca4f-3001-442b-b444-e5dc9c5dfa19_nopbai.jpg', N'D:\File\Thi kết thúc học phần - Lập trình web - Nhóm 1,2\Submission\3e4bca4f-3001-442b-b444-e5dc9c5dfa19_nopbai.jpg', N'image/jpeg', CAST(45773 AS Decimal(18, 0)), 2, N'nopbai.jpg')
SET IDENTITY_INSERT [dbo].[Submissions] ON 

INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [StudentId], [IPAddress], [SubmittedTime], [Status]) VALUES (2, 1, N'20T1020433', N'::1            ', CAST(N'2024-04-24 08:41:01.833' AS DateTime), N'Submitted')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [StudentId], [IPAddress], [SubmittedTime], [Status]) VALUES (1005, 2012, N'20T1020433', NULL, CAST(N'2024-04-23 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [StudentId], [IPAddress], [SubmittedTime], [Status]) VALUES (1006, 2004, N'20T1020433', N'::1            ', CAST(N'2024-04-24 10:10:19.250' AS DateTime), N'Submitted')
SET IDENTITY_INSERT [dbo].[Submissions] OFF
INSERT [dbo].[Teachers] ([TeacherId], [TeacherName], [Email]) VALUES (N'1         ', N'Trần Nguyên Phong', N'tnphong@husc.edu.vn')
INSERT [dbo].[Teachers] ([TeacherId], [TeacherName], [Email]) VALUES (N'DHT0220   ', N'Trần Nguyên Phong', N'tnphong@husc.edu.vn')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'a871d303-4a0c-4bbf-a33e-29d15c1dd9d3', N'a871d303-4a0c-4bbf-a33e-29d15c1dd9d3_SelectListHelper.cs', N'D:\File\2009\Test\a871d303-4a0c-4bbf-a33e-29d15c1dd9d3_SelectListHelper.cs', N'text/plain', CAST(2845 AS Decimal(18, 0)), 2009, N'SelectListHelper.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'8fb08cd1-45a4-4974-bb44-5b269add6e17', N'8fb08cd1-45a4-4974-bb44-5b269add6e17_SelectListHelper.cs', N'D:\File\2005\Test\8fb08cd1-45a4-4974-bb44-5b269add6e17_SelectListHelper.cs', N'text/plain', CAST(2845 AS Decimal(18, 0)), 2005, N'SelectListHelper.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'0063b28c-0800-4250-8cc3-5cf99cb6f559', N'0063b28c-0800-4250-8cc3-5cf99cb6f559_SelectListHelper.cs', N'D:\File\2011\Test\0063b28c-0800-4250-8cc3-5cf99cb6f559_SelectListHelper.cs', N'text/plain', CAST(2845 AS Decimal(18, 0)), 2011, N'SelectListHelper.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'27a87177-5b08-4f0e-8d12-5e09f6bcbd39', N'27a87177-5b08-4f0e-8d12-5e09f6bcbd39_Converter.cs', N'D:\File\2006\Test\27a87177-5b08-4f0e-8d12-5e09f6bcbd39_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2006, N'Converter.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'f9bf3cee-7a5c-411e-b042-9744ebc4c775', N'f9bf3cee-7a5c-411e-b042-9744ebc4c775_Converter.cs', N'D:\File\2004\Test\f9bf3cee-7a5c-411e-b042-9744ebc4c775_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2004, N'Converter.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'736985bd-6b02-48d0-b4ea-b0e47c832812', N'736985bd-6b02-48d0-b4ea-b0e47c832812_WebSecurityModels.cs', N'D:\File\2012\Test\736985bd-6b02-48d0-b4ea-b0e47c832812_WebSecurityModels.cs', N'text/plain', CAST(6640 AS Decimal(18, 0)), 2012, N'WebSecurityModels.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'3ed2305a-e555-4414-86f2-b84917ebfed8', N'3ed2305a-e555-4414-86f2-b84917ebfed8_Converter.cs', N'D:\File\2008\Test\3ed2305a-e555-4414-86f2-b84917ebfed8_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2008, N'Converter.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'6052732b-499a-45ac-9270-c060a4c539dd', N'6052732b-499a-45ac-9270-c060a4c539dd_Converter.cs', N'D:\File\2010\Test\6052732b-499a-45ac-9270-c060a4c539dd_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2010, N'Converter.cs')
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (1, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'- Một cộng một bằng mấy?', NULL, NULL, N'InProgress', 0, 0, CAST(N'2024-03-27 00:00:00.000' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2004, N'Thi kết thúc học phần - Nhập môn - Nhóm 3', N'', CAST(N'2024-04-14 14:38:00.000' AS DateTime), NULL, N'InProgress', 0, 0, CAST(N'2024-04-09 13:42:16.293' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2005, N'', N'', NULL, NULL, N'Creating', 0, 0, CAST(N'2024-04-09 13:42:45.750' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2006, N'', N'', NULL, NULL, N'Creating', 0, 0, CAST(N'2024-04-09 13:44:20.903' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2007, N'', N'', NULL, NULL, N'Creating', 0, 0, CAST(N'2024-04-09 13:47:25.523' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2008, N'', N'', NULL, NULL, N'Creating', 0, 0, CAST(N'2024-04-09 13:50:48.823' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2009, N'', N'', NULL, NULL, N'Creating', 0, 0, CAST(N'2024-04-09 13:58:33.347' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2010, N'', N'', NULL, NULL, N'Creating', 0, 0, CAST(N'2024-04-09 14:15:50.483' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2011, N'', N'', NULL, NULL, N'Creating', 0, 0, CAST(N'2024-04-09 14:36:21.650' AS DateTime), NULL, N'Exam', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2012, N'Kiểm tra - Kỹ thuật lập trình', N'', CAST(N'2024-04-14 14:38:00.000' AS DateTime), CAST(N'2024-04-25 14:38:00.000' AS DateTime), N'InProgress', 0, 0, CAST(N'2024-04-09 14:38:43.713' AS DateTime), NULL, N'Quiz', N'DHT0220   ')
SET IDENTITY_INSERT [dbo].[Tests] OFF
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_IsCanceled]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_IsCheckIP]  DEFAULT ((0)) FOR [IsCheckIP]
GO
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_IsConductedAtSchool]  DEFAULT ((0)) FOR [IsConductedAtSchool]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Submissions] FOREIGN KEY([SubmissionId])
REFERENCES [dbo].[Submissions] ([SubmissionId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comment_Submissions]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Teachers] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([TeacherId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comment_Teachers]
GO
ALTER TABLE [dbo].[SubmissionFiles]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionFiles_Submissions] FOREIGN KEY([SubmissionId])
REFERENCES [dbo].[Submissions] ([SubmissionId])
GO
ALTER TABLE [dbo].[SubmissionFiles] CHECK CONSTRAINT [FK_SubmissionFiles_Submissions]
GO
ALTER TABLE [dbo].[Submissions]  WITH CHECK ADD  CONSTRAINT [FK_Submissions_Exams] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TestId])
GO
ALTER TABLE [dbo].[Submissions] CHECK CONSTRAINT [FK_Submissions_Exams]
GO
ALTER TABLE [dbo].[TestFiles]  WITH CHECK ADD  CONSTRAINT [FK_ExamFiles_Exams] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TestId])
GO
ALTER TABLE [dbo].[TestFiles] CHECK CONSTRAINT [FK_ExamFiles_Exams]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Teachers] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([TeacherId])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Teachers]
GO
/****** Object:  StoredProcedure [dbo].[AddComment]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddComment]
    @Body NVARCHAR(255),
    @TeacherId NVARCHAR(10),
    @SubmissionId INT,
    @CommentedTime DATETIME
AS
BEGIN
    INSERT INTO Comments (Body, TeacherId, SubmissionId, CommentedTime) 
    VALUES (@Body, @TeacherId, @SubmissionId, @CommentedTime);
    SELECT SCOPE_IDENTITY() AS CommentId;
END;

GO
/****** Object:  StoredProcedure [dbo].[AddStudent]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[AddStudent]
    @StudentId NCHAR(10),
    @FirstName NVARCHAR(50),
	 @LastName NVARCHAR(50),
    @Email NVARCHAR(50)
AS
BEGIN
    INSERT INTO Students (StudentId, FirstName, LastName, Email)
    VALUES (@StudentId, @FirstName, @LastName, @Email);
END;

GO
/****** Object:  StoredProcedure [dbo].[AddSubmission]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSubmission]
    @StudentId NVARCHAR(10),
    @TestId INT,
    @SubmittedTime DATETIME,
    @IPAddress NCHAR(15),
    @Status NVARCHAR(20),
    @SubmissionId INT OUTPUT
AS
BEGIN
    INSERT INTO Submissions (StudentId, TestId, SubmittedTime, IPAddress, Status)
    VALUES (@StudentId, @TestId, @SubmittedTime, @IPAddress, @Status);

    SET @SubmissionId = SCOPE_IDENTITY();
END;

GO
/****** Object:  StoredProcedure [dbo].[AddSubmissionFile]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSubmissionFile]
    @FileId UNIQUEIDENTIFIER,
    @SubmissionId INT,
    @FileName NVARCHAR(MAX),
    @FilePath NVARCHAR(MAX),
    @MimeType NVARCHAR(50),
    @Size decimal(18, 0),
    @OriginalName NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO SubmissionFiles (FileId, SubmissionId, FileName, FilePath, MimeType, Size, OriginalName)
    VALUES (@FileId, @SubmissionId, @FileName, @FilePath, @MimeType, @Size, @OriginalName);
END;

GO
/****** Object:  StoredProcedure [dbo].[AddTeacher]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTeacher]
    @TeacherId NCHAR(10),
    @TeacherName NVARCHAR(50),
    @Email NVARCHAR(50)
AS
BEGIN
    INSERT INTO Teachers (TeacherId, TeacherName, Email)
    VALUES (@TeacherId, @TeacherName, @Email);
END;

GO
/****** Object:  StoredProcedure [dbo].[AddTest]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTest]
    @Title NVARCHAR(255),
    @Instruction NVARCHAR(MAX),
    @StartTime DATETIME,
    @EndTime DATETIME,
    @Status NVARCHAR(20),
    @IsCheckIP BIT,
    @IsConductedAtSchool BIT,
    @CreatedTime DATETIME,
	@LastUpdateTime DATETIME,
    @TestType NVARCHAR(20),
    @TeacherId INT,
    @TestId INT OUTPUT
AS
BEGIN
    INSERT INTO Tests (Title, Instruction, StartTime, EndTime, Status, IsCheckIP, IsConductedAtSchool, CreatedTime, LastUpdateTime, TestType, TeacherId)
    VALUES (@Title, @Instruction, @StartTime, @EndTime, @Status, @IsCheckIP, @IsConductedAtSchool, @CreatedTime, @LastUpdateTime, @TestType, @TeacherId);

    SET @TestId = SCOPE_IDENTITY();
END;

GO
/****** Object:  StoredProcedure [dbo].[AddTestFile]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTestFile]
    @FileId UNIQUEIDENTIFIER,
    @FileName NVARCHAR(MAX),
    @FilePath NVARCHAR(MAX),
    @MimeType NVARCHAR(100),
    @Size decimal(18, 0),
    @OriginalName NVARCHAR(MAX),
    @TestId INT
AS
BEGIN
    INSERT INTO TestFiles (FileId, FileName, FilePath, MimeType, Size, OriginalName, TestId)
    VALUES (@FileId, @FileName, @FilePath, @MimeType, @Size, @OriginalName, @TestId);
END;

GO
/****** Object:  StoredProcedure [dbo].[CountSubmissions]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CReate PROCEDURE [dbo].[CountSubmissions]
    @TestId NVARCHAR(10),
    @SearchValue NVARCHAR(MAX),
    @SubmissionStatus NVARCHAR(20)
AS
BEGIN
SELECT COUNT(*)
        FROM Tests t join Submissions s on t.TestId=s.TestId join Students st on s.StudentId = st.StudentId
        WHERE t.TestId = @TestId
        AND (@SearchValue = N'' OR s.StudentId LIKE '%' + @SearchValue + '%' or st.FirstName like '%' + @SearchValue + '%' or st.LastName like '%' + @SearchValue + '%')
		AND (@SubmissionStatus = N'' OR s.Status like @SubmissionStatus)

    
END;
GO
/****** Object:  StoredProcedure [dbo].[CountTestsOfStudent]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountTestsOfStudent]
    @StudentId NVARCHAR(10),
    @SearchValue NVARCHAR(MAX),
    @TestType NVARCHAR(20),
    @FromTime DATETIME,
    @ToTime DATETIME
AS
BEGIN
    SELECT COUNT(*)
    FROM Tests t join Submissions s on t.TestId=s.TestId
    WHERE s.StudentId = @StudentId
    AND (@SearchValue = '' OR t.Title LIKE '%' + @SearchValue + '%')
    AND (@TestType = '' OR t.TestType = @TestType)
	AND (t.Status = 'InProgress')
    AND (@FromTime IS NULL OR t.StartTime >= @FromTime)
    AND (@ToTime IS NULL OR t.EndTime <= @ToTime);
END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteComment]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteComment]
    @CommentId INT
AS
BEGIN
    DELETE FROM Comments WHERE CommentId = @CommentId;
END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteSubmission]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSubmission]
    @SubmissionId INT
AS
BEGIN
	DELETE FROM SubmissionFiles WHERE SubmissionId = @SubmissionId;
    DELETE FROM Submissions WHERE SubmissionId = @SubmissionId;
END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteSubmissionFile]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSubmissionFile]
    @FileId UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM SubmissionFiles WHERE FileId = @FileId;
END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteTest]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTest]
    @TestId INT
AS
BEGIN
    BEGIN TRANSACTION;

    DELETE FROM TestFiles WHERE TestID = @TestId;
	DELETE FROM Submissions WHERE TestID = @TestId;
    DELETE FROM Tests WHERE TestID = @TestId;

    COMMIT;
END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteTestFile]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTestFile]
    @FileId UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM TestFiles WHERE FileId = @FileId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetCommentById]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCommentById]
    @CommentId INT
AS
BEGIN
    SELECT * FROM Comments WHERE CommentId = @CommentId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetCommentsBySubmissionId]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCommentsBySubmissionId]
    @SubmissionId INT
AS
BEGIN
    SELECT * FROM Comments WHERE SubmissionId = @SubmissionId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetStudentById]
    @StudentId NCHAR(10)
AS
BEGIN
    SELECT * FROM Students WHERE StudentId = @StudentId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSubmissionById]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubmissionById]
    @SubmissionId INT
AS
BEGIN
    SELECT * FROM Submissions WHERE SubmissionId = @SubmissionId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestId]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubmissionByTestId]
    @TestId INT
AS
BEGIN
    SELECT * FROM Submissions WHERE TestId = @TestId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestIdAndStudentId]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSubmissionByTestIdAndStudentId]
    @TestId INT, @StudentId NVARCHAR(20)
AS
BEGIN
    SELECT * FROM Submissions WHERE TestId = @TestId and StudentId = @StudentId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetSubmissionFileById]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubmissionFileById]
    @FileId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM SubmissionFiles WHERE FileId = @FileId;
END;


GO
/****** Object:  StoredProcedure [dbo].[GetSubmissionFilesBySubmissionId]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubmissionFilesBySubmissionId]
    @SubmissionId INT
AS
BEGIN
    SELECT * FROM SubmissionFiles WHERE SubmissionId = @SubmissionId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetSubmissions]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CReate PROCEDURE [dbo].[GetSubmissions]
    @Page INT,
    @PageSize INT,
    @TestId NVARCHAR(10),
    @SearchValue NVARCHAR(MAX),
    @SubmissionStatus NVARCHAR(20)
AS
BEGIN
WITH cte AS
    (
        SELECT t.*, ROW_NUMBER() OVER (ORDER BY StartTime) AS RowNumber
        FROM Tests t join Submissions s on t.TestId=s.TestId join Students st on s.StudentId = st.StudentId
        WHERE t.TestId = @TestId
        AND (@SearchValue = N'' OR s.StudentId LIKE '%' + @SearchValue + '%' or st.FirstName like '%' + @SearchValue + '%' or st.LastName like '%' + @SearchValue + '%')
		AND (@SubmissionStatus = N'' OR s.Status like @SubmissionStatus)

    )
    SELECT *
    FROM cte
    WHERE (@PageSize = 0)
        OR (RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize)
    ORDER BY RowNumber;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTeacherById]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTeacherById]
    @TeacherId NCHAR(10)
AS
BEGIN
    SELECT * FROM Teachers WHERE TeacherId = @TeacherId;
END;


GO
/****** Object:  StoredProcedure [dbo].[GetTestById]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTestById]
    @TestId INT

AS
BEGIN
    SELECT *
    FROM Tests
    WHERE TestID = @TestId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetTestFileById]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTestFileById]
    @FileId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM TestFiles WHERE FileId = @FileId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetTestFilesByTestId]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTestFilesByTestId]
    @TestId INT
AS
BEGIN
    SELECT tf.*
    FROM TestFiles tf
    INNER JOIN Tests t ON tf.TestId = t.TestId
    WHERE tf.TestId = @TestId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetTestsOfStudent]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTestsOfStudent]
    @Page INT,
    @PageSize INT,
    @StudentId NVARCHAR(10),
    @SearchValue NVARCHAR(MAX),
    @TestType NVARCHAR(20),
    @FromTime DATETIME,
    @ToTime DATETIME
AS
BEGIN
WITH cte AS
    (
        SELECT t.*, ROW_NUMBER() OVER (ORDER BY StartTime) AS RowNumber
        FROM Tests t join Submissions s on t.TestId=s.TestId
        WHERE s.StudentId = @StudentId
        AND (@SearchValue = N'' OR t.Title LIKE '%' + @SearchValue + '%')
        AND (@TestType = N'' OR t.TestType like @TestType)
			AND (t.Status = 'InProgress')
        AND (@FromTime IS NULL OR t.StartTime >= @FromTime)
        AND (@ToTime IS NULL OR t.EndTime <= @ToTime)
    )
    SELECT *
    FROM cte
    WHERE (@PageSize = 0)
        OR (RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize)
    ORDER BY RowNumber;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetTestsOfTeacher]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CReate PROCEDURE [dbo].[GetTestsOfTeacher]
    @Page INT,
    @PageSize INT,
    @TeacherId NVARCHAR(10),
    @SearchValue NVARCHAR(MAX),
    @TestType NVARCHAR(20),
	 @TestStatus NVARCHAR(20),
    @FromTime DATETIME,
    @ToTime DATETIME
AS
BEGIN
WITH cte AS
    (
        SELECT t.*, ROW_NUMBER() OVER (ORDER BY StartTime) AS RowNumber
        FROM Tests t join Submissions s on t.TestId=s.TestId
        WHERE t.TeacherId = @TeacherId
        AND (@SearchValue = N'' OR t.Title LIKE '%' + @SearchValue + '%')
        AND (@TestType = N'' OR t.TestType like @TestType)
		AND (@TestStatus = N'' OR t.Status like @TestStatus)
        AND (@FromTime IS NULL OR t.StartTime >= @FromTime)
        AND (@ToTime IS NULL OR t.EndTime <= @ToTime)
    )
    SELECT *
    FROM cte
    WHERE (@PageSize = 0)
        OR (RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize)
    ORDER BY RowNumber;
END;
GO
/****** Object:  StoredProcedure [dbo].[IsTestUsed]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsTestUsed]
    @TestId INT,
    @Result BIT OUTPUT
AS
BEGIN
    IF EXISTS(SELECT * FROM Submissions s inner join SubmissionFiles sf on s.SubmissionId = sf.SubmissionId WHERE s.TestId = @TestId)
        SET @Result = 1;
    ELSE
        SET @Result = 0;
END;

GO
/****** Object:  StoredProcedure [dbo].[UpdateComment]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateComment]
    @CommentId INT,
    @Body NVARCHAR(MAX),
    @TeacherId NVARCHAR(50),
    @SubmissionId INT,
    @CommentedTime DATETIME
AS
BEGIN
    UPDATE Comments 
    SET Body = @Body, TeacherId = @TeacherId, SubmissionId = @SubmissionId, CommentedTime = @CommentedTime
    WHERE CommentId = @CommentId;
END;

GO
/****** Object:  StoredProcedure [dbo].[UpdateSubmission]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSubmission]
    @SubmissionId INT,
    @SubmittedTime DATETIME,
    @IPAddress NVARCHAR(50),
    @Status NVARCHAR(20)
AS
BEGIN
    UPDATE Submissions
    SET SubmittedTime = @SubmittedTime,
        IPAddress = @IPAddress,
        Status = @Status
    WHERE SubmissionId = @SubmissionId;
END;

GO
/****** Object:  StoredProcedure [dbo].[UpdateTest]    Script Date: 24/04/2024 4:42:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTest]
    @TestId INT,
    @Title NVARCHAR(255),
    @Instruction NVARCHAR(MAX),
    @StartTime DATETIME,
    @EndTime DATETIME,
    @Status NVARCHAR(20),
    @IsCheckIP BIT,
    @IsConductedAtSchool BIT,
    @CreatedTime DATETIME,
    @LastUpdateTime DATETIME,
    @TestType NVARCHAR(20),
    @TeacherId INT,
    @Result BIT OUTPUT
AS
BEGIN
    BEGIN TRANSACTION;

    UPDATE Tests
    SET Title = @Title,
        Instruction = @Instruction,
        StartTime = @StartTime,
        EndTime = @EndTime,
        Status = @Status,
        IsCheckIP = @IsCheckIP,
        IsConductedAtSchool = @IsConductedAtSchool,
        CreatedTime = @CreatedTime,
        LastUpdateTime = @LastUpdateTime,
        TestType = @TestType,
        TeacherId = @TeacherId
    WHERE TestId = @TestId;

    IF @@ROWCOUNT > 0
        SET @Result = 1;
    ELSE
        SET @Result = 0;

    COMMIT;
END;

GO
