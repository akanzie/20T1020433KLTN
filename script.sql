USE [QuanLyBaiThi]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  Table [dbo].[Students]    Script Date: 19/04/2024 12:01:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [nchar](10) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubmissionFiles]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  Table [dbo].[Submissions]    Script Date: 19/04/2024 12:01:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Submissions](
	[SubmissionId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nchar](10) NOT NULL,
	[TestId] [int] NOT NULL,
	[IPAddress] [nchar](15) NULL,
	[SubmittedTime] [datetime] NOT NULL,
	[Status] [nvarchar](20) NULL,
 CONSTRAINT [PK_Submission] PRIMARY KEY CLUSTERED 
(
	[SubmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 19/04/2024 12:01:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[TeacherId] [nchar](10) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TestFiles]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  Table [dbo].[Tests]    Script Date: 19/04/2024 12:01:20 CH ******/
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
ALTER TABLE [dbo].[Submissions]  WITH CHECK ADD  CONSTRAINT [FK_Submissions_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Submissions] CHECK CONSTRAINT [FK_Submissions_Students]
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
/****** Object:  StoredProcedure [dbo].[AddComment]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[AddSubmission]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[AddSubmissionFile]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[AddTest]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[AddTestFile]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[CountTestsOfStudent]    Script Date: 19/04/2024 12:01:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountTestsOfStudent]
    @StudentId NVARCHAR(10),
    @SearchValue NVARCHAR(MAX),
    @TestType NVARCHAR(20),
    @TestStatus NVARCHAR(20),
    @FromTime DATETIME,
    @ToTime DATETIME
AS
BEGIN
    SELECT COUNT(*)
    FROM Tests t join Submissions s on t.TestId=s.TestId
    WHERE s.StudentId = @StudentId
    AND (@SearchValue = '' OR t.Title LIKE '%' + @SearchValue + '%')
    AND (@TestType IS NULL OR t.TestType = @TestType)
    AND (@TestStatus IS NULL OR t.Status = @TestStatus)
    AND (@FromTime IS NULL OR t.StartTime >= @FromTime)
    AND (@ToTime IS NULL OR t.EndTime <= @ToTime);
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteComment]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmission]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmissionFile]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteTest]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteTestFile]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetCommentById]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetCommentsBySubmissionId]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionById]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestId]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestIdAndStudentId]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionFileById]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionFilesBySubmissionId]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestById]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestFileById]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestFilesByTestId]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestsOfStudent]    Script Date: 19/04/2024 12:01:20 CH ******/
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
    @TestStatus NVARCHAR(20),
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
/****** Object:  StoredProcedure [dbo].[IsTestUsed]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateComment]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateSubmission]    Script Date: 19/04/2024 12:01:20 CH ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateTest]    Script Date: 19/04/2024 12:01:20 CH ******/
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
