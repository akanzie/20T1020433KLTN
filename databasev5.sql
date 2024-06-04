USE [master]
GO
/****** Object:  Database [QuanLyKyThi]    Script Date: 03/06/2024 7:46:03 CH ******/
CREATE DATABASE [QuanLyKyThi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyKyThi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QuanLyKyThi.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLyKyThi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QuanLyKyThi_log.ldf' , SIZE = 2560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuanLyKyThi] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyKyThi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyKyThi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyKyThi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyKyThi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuanLyKyThi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyKyThi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLyKyThi] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyKyThi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyKyThi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyKyThi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyKyThi] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QuanLyKyThi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyKyThi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyKyThi', N'ON'
GO
ALTER DATABASE [QuanLyKyThi] SET QUERY_STORE = OFF
GO
USE [QuanLyKyThi]
GO
/****** Object:  UserDefinedFunction [dbo].[GetTestStatus]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetTestStatus] (
    @StartTime DATETIME,
    @EndTime DATETIME
)
RETURNS NVARCHAR(20)
AS
BEGIN
    DECLARE @CurrentDateTime DATETIME = GETDATE();
    DECLARE @Status NVARCHAR(20);

    IF @CurrentDateTime < @StartTime
        SET @Status = 'Upcoming';
    ELSE IF @CurrentDateTime > @EndTime
        SET @Status = 'Finished';
    ELSE
        SET @Status = 'Ongoing';

    RETURN @Status;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString]
(
    @String NVARCHAR(MAX),
    @Delimiter NVARCHAR(5)
)
RETURNS TABLE
AS
RETURN
(
    SELECT value AS Item
    FROM (SELECT 
            CAST('<X>' + REPLACE(@String, @Delimiter, '</X><X>') + '</X>' AS XML) AS xmlData
         ) AS sub
         CROSS APPLY
         (SELECT fdata.D.value('.', 'nvarchar(100)') AS value 
          FROM sub.xmlData.nodes('X') AS fdata(D)) AS split
)
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](255) NULL,
	[TeacherId] [varchar](50) NULL,
	[SubmissionId] [int] NULL,
	[CommentedTime] [datetime] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](10) NULL,
	[LastName] [nvarchar](25) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubmissionFiles]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubmissionFiles](
	[FileId] [uniqueidentifier] NOT NULL,
	[FIleName] [nvarchar](max) NOT NULL,
	[FilePath] [nvarchar](max) NOT NULL,
	[MimeType] [nvarchar](255) NOT NULL,
	[Size] [decimal](18, 0) NULL,
	[SubmissionId] [int] NOT NULL,
	[OriginalName] [nvarchar](max) NULL,
 CONSTRAINT [PK_SubmissionFiles] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubmissionHistory]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubmissionHistory](
	[HistoryId] [int] IDENTITY(1,1) NOT NULL,
	[SubmissionId] [int] NULL,
	[IPAddress] [varchar](50) NULL,
	[SubmitTime] [datetime] NULL,
 CONSTRAINT [PK_SubmissionHistory] PRIMARY KEY CLUSTERED 
(
	[HistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Submissions]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Submissions](
	[SubmissionId] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[Status] [nvarchar](20) NULL,
	[StudentId] [varchar](50) NOT NULL,
	[SubmitTime] [datetime] NULL,
 CONSTRAINT [PK_Submissions] PRIMARY KEY CLUSTERED 
(
	[SubmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[TeacherId] [varchar](50) NOT NULL,
	[TeacherName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestFiles]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestFiles](
	[FileId] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[FilePath] [nvarchar](max) NOT NULL,
	[MimeType] [nvarchar](255) NOT NULL,
	[Size] [decimal](18, 0) NULL,
	[TestId] [int] NOT NULL,
	[OriginalName] [nvarchar](max) NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 03/06/2024 7:46:03 CH ******/
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
	[CanSubmitLate] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
	[TestType] [nvarchar](20) NOT NULL,
	[TeacherId] [varchar](50) NOT NULL,
	[Semester] [varchar](50) NULL,
	[ModuleId] [varchar](50) NULL,
 CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Submissions] ADD  CONSTRAINT [DF_Submissions_Status]  DEFAULT (N'NotSubmitted') FOR [Status]
GO
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_IsCanceled]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_IsCheckIP]  DEFAULT ((0)) FOR [IsCheckIP]
GO
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_IsConductedAtSchool]  DEFAULT ((0)) FOR [IsConductedAtSchool]
GO
ALTER TABLE [dbo].[Tests] ADD  CONSTRAINT [DF_Tests_TestType]  DEFAULT (N'Exam') FOR [TestType]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Teachers] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([TeacherId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comment_Teachers]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Submissions] FOREIGN KEY([SubmissionId])
REFERENCES [dbo].[Submissions] ([SubmissionId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Submissions]
GO
ALTER TABLE [dbo].[SubmissionFiles]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionFiles_Submissions] FOREIGN KEY([SubmissionId])
REFERENCES [dbo].[Submissions] ([SubmissionId])
GO
ALTER TABLE [dbo].[SubmissionFiles] CHECK CONSTRAINT [FK_SubmissionFiles_Submissions]
GO
ALTER TABLE [dbo].[SubmissionHistory]  WITH CHECK ADD  CONSTRAINT [FK_SubmissionHistory_Submissions] FOREIGN KEY([SubmissionId])
REFERENCES [dbo].[Submissions] ([SubmissionId])
GO
ALTER TABLE [dbo].[SubmissionHistory] CHECK CONSTRAINT [FK_SubmissionHistory_Submissions]
GO
ALTER TABLE [dbo].[Submissions]  WITH CHECK ADD  CONSTRAINT [FK_Submissions_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Submissions] CHECK CONSTRAINT [FK_Submissions_Students]
GO
ALTER TABLE [dbo].[Submissions]  WITH CHECK ADD  CONSTRAINT [FK_Submissions_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TestId])
GO
ALTER TABLE [dbo].[Submissions] CHECK CONSTRAINT [FK_Submissions_Tests]
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
/****** Object:  StoredProcedure [dbo].[AddComment]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddComment]
    @Body NVARCHAR(255),
    @TeacherId VARCHAR(50),
    @SubmissionId INT,
    @CommentedTime DATETIME
AS
BEGIN
    INSERT INTO Comments (Body, TeacherId, SubmissionId, CommentedTime) 
    VALUES (@Body, @TeacherId, @SubmissionId, @CommentedTime);
    
END;
GO
/****** Object:  StoredProcedure [dbo].[AddStudent]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStudent]
    @StudentId VARCHAR(50),
    @FirstName NVARCHAR(50),
	 @LastName NVARCHAR(50)

AS
BEGIN
    INSERT INTO Students (StudentId, FirstName, LastName)
    VALUES (@StudentId, @FirstName, @LastName);
END;


GO
/****** Object:  StoredProcedure [dbo].[AddSubmission]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSubmission]
    @StudentId VARCHAR(50),
    @TestId INT,    
    @SubmissionId INT OUTPUT
AS
BEGIN
     SET NOCOUNT ON;

    -- Kiểm tra xem submission đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM Submissions WHERE StudentId = @StudentId AND TestId = @TestId)
    BEGIN
        -- Trả về lỗi nếu submission đã tồn tại
        RAISERROR ('A submission for this student and test already exists.', 16, 1);
        RETURN;
    END

    -- Nếu không tồn tại, chèn submission mới
    INSERT INTO Submissions (StudentId, TestId)
    VALUES (@StudentId, @TestId);

    -- Lấy SubmissionId của bản ghi vừa chèn
    SET @SubmissionId = SCOPE_IDENTITY();
END;


GO
/****** Object:  StoredProcedure [dbo].[AddSubmissionFile]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSubmissionFile]
    @FileId UNIQUEIDENTIFIER,
    @SubmissionId INT,
    @FileName NVARCHAR(MAX),
    @FilePath NVARCHAR(MAX),
    @MimeType NVARCHAR(255),
    @Size decimal(18, 0),
    @OriginalName NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO SubmissionFiles (FileId, SubmissionId, FileName, FilePath, MimeType, Size, OriginalName)
    VALUES (@FileId, @SubmissionId, @FileName, @FilePath, @MimeType, @Size, @OriginalName);
END;


GO
/****** Object:  StoredProcedure [dbo].[AddSubmissionHistory]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSubmissionHistory]
    @SubmissionId INT,
    @SubmitTime DATETIME,
    @IPAddress VARCHAR(50)
AS
BEGIN
    INSERT INTO SubmissionHistory (SubmissionId, SubmitTime, IPAddress)
    VALUES (@SubmissionId, @SubmitTime, @IPAddress)
    SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[AddTeacher]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTeacher]
    @TeacherId VARCHAR(50),
    @TeacherName NVARCHAR(50),
    @Email NVARCHAR(50)
AS
BEGIN
    INSERT INTO Teachers (TeacherId, TeacherName, Email)
    VALUES (@TeacherId, @TeacherName, @Email);
END;


GO
/****** Object:  StoredProcedure [dbo].[AddTest]    Script Date: 03/06/2024 7:46:03 CH ******/
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
	@CanSubmitLate BIT,
    @TestType NVARCHAR(20),
    @TeacherId VARCHAR(50),
    @TestId INT OUTPUT
AS
BEGIN
    INSERT INTO Tests (Title, Instruction, StartTime, EndTime, Status, IsCheckIP, IsConductedAtSchool, CreatedTime, LastUpdateTime, CanSubmitLate, TestType, TeacherId)
    VALUES (@Title, @Instruction, @StartTime, @EndTime, @Status, @IsCheckIP, @IsConductedAtSchool, @CreatedTime, @LastUpdateTime,@CanSubmitLate, @TestType, @TeacherId);

    SET @TestId = SCOPE_IDENTITY();
END;


GO
/****** Object:  StoredProcedure [dbo].[AddTestFile]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTestFile]
    @FileId UNIQUEIDENTIFIER,
    @FileName NVARCHAR(MAX),
    @FilePath NVARCHAR(MAX),
    @MimeType NVARCHAR(255),
    @Size decimal(18, 0),
    @OriginalName NVARCHAR(MAX),
    @TestId INT
AS
BEGIN
    INSERT INTO TestFiles (FileId, FileName, FilePath, MimeType, Size, OriginalName, TestId)
    VALUES (@FileId, @FileName, @FilePath, @MimeType, @Size, @OriginalName, @TestId);
END;


GO
/****** Object:  StoredProcedure [dbo].[CheckIPAddressExists]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CheckIPAddressExists]
    @IPAddress NVARCHAR(50),
    @SubmissionId INT,
    @TestId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem có sinh viên nào không
    IF EXISTS (
        SELECT 1
        FROM Submissions s
        INNER JOIN SubmissionHistory sh ON s.SubmissionId = sh.SubmissionId
        WHERE sh.IPAddress = @IPAddress AND s.TestId = @TestId AND s.SubmissionId != @SubmissionId
    )
    BEGIN
        -- Cập nhật trạng thái nộp bài của sinh viên có địa chỉ IP đã cho và testid đã cho thành 'pendingprocessing'
        UPDATE Submissions
        SET Status = 'PendingProcessing'
        WHERE StudentId IN (
            SELECT s.StudentId
            FROM Submissions s
            INNER JOIN SubmissionHistory sh ON s.SubmissionId = sh.SubmissionId
            WHERE sh.IPAddress = @IPAddress AND s.TestId = @TestId AND s.SubmissionId != @SubmissionId
        ) AND TestId = @TestId;

        -- Trả về true nếu có sinh viên nào được cập nhật
        SELECT 1 AS Updated;
    END
    ELSE
    BEGIN
        -- Trả về false nếu không có sinh viên nào
        SELECT 0 AS Updated;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[CountFilesBySubmissionId]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountFilesBySubmissionId]
    @SubmissionId int   
AS
BEGIN
SELECT COUNT(*)
        FROM Submissions s join SubmissionFiles sf on s.SubmissionId=sf.SubmissionId 
        WHERE s.SubmissionId = @SubmissionId and s.Status not like 'NotSubmitted'    

    
END;
GO
/****** Object:  StoredProcedure [dbo].[CountSubmissions]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountSubmissions]
    @TestId NVARCHAR(10),
    @SearchValue NVARCHAR(MAX),
    @SubmissionStatus NVARCHAR(20)
AS
BEGIN
SELECT COUNT(*)
        FROM Tests t join Submissions s on t.TestId=s.TestId join Students st on s.StudentId = st.StudentId
        WHERE t.TestId = @TestId
        AND (@SearchValue = N'' OR s.StudentId LIKE '%' + @SearchValue + '%' or st.FirstName like '%' + @SearchValue + '%' or st.LastName like '%' + @SearchValue + '%')
		AND (@SubmissionStatus = N'' OR s.Status IN (SELECT * FROM dbo.SplitString(@SubmissionStatus, ',')))

    
END;

GO
/****** Object:  StoredProcedure [dbo].[CountTestsForStudentHome]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountTestsForStudentHome]
    @StudentId VARCHAR(50)
   
AS
BEGIN
	DECLARE @CurrentDateTime DATETIME = GETDATE();
    SELECT COUNT(*)
    FROM Tests t join Submissions s on t.TestId=s.TestId
    WHERE s.StudentId = @StudentId   
	AND (t.Status = 'InProgress' AND (dbo.GetTestStatus(t.StartTime, t.EndTime) = 'Upcoming' OR dbo.GetTestStatus(t.StartTime, t.EndTime) = 'Ongoing'))
    AND (@CurrentDateTime < t.EndTime OR t.EndTime IS NULL)
END;
GO
/****** Object:  StoredProcedure [dbo].[CountTestsOfStudent]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountTestsOfStudent]
    @StudentId VARCHAR(50),
    @SearchValue NVARCHAR(MAX),
	 @TestStatus NVARCHAR(20),
    @TestType NVARCHAR(20),
    @FromTime DATETIME,
    @ToTime DATETIME
AS
BEGIN
	DECLARE @CurrentDateTime DATETIME = GETDATE();		
    SELECT COUNT(*)
    FROM Tests t join Submissions s on t.TestId=s.TestId
    WHERE s.StudentId = @StudentId
    AND (@SearchValue = N'' OR t.Title LIKE '%' + @SearchValue + '%')
    AND (@TestType = N'' OR t.TestType = @TestType)
	AND (@TestStatus = N'' OR (t.Status = 'InProgress' AND dbo.GetTestStatus(t.StartTime, t.EndTime) = @TestStatus))
    AND (@FromTime IS NULL OR t.StartTime >= @FromTime)
    AND (@ToTime IS NULL OR t.EndTime <= @ToTime)
END;


GO
/****** Object:  StoredProcedure [dbo].[CountTestsOfTeacher]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountTestsOfTeacher]
    @TeacherId VARCHAR(50),
    @SearchValue NVARCHAR(MAX),
    @TestType NVARCHAR(20),
	@TestStatus NVARCHAR(20),
    @FromTime DATETIME,
    @ToTime DATETIME
AS
BEGIN
    SELECT COUNT(*)
    FROM Tests t
    WHERE t.TeacherId = @TeacherId
    AND (@SearchValue = '' OR t.Title LIKE '%' + @SearchValue + '%')
    AND (@TestType = '' OR t.TestType = @TestType)
	AND (@TestStatus = N'' OR t.Status like @TestStatus OR (t.Status = 'InProgress' AND dbo.GetTestStatus(t.StartTime, t.EndTime) = @TestStatus))
    AND (@FromTime IS NULL OR t.StartTime >= @FromTime)
    AND (@ToTime IS NULL OR t.EndTime <= @ToTime);
END;


GO
/****** Object:  StoredProcedure [dbo].[DeleteComment]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmission]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmissionFile]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmissionHistory]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSubmissionHistory]
    @Id INT
AS
BEGIN
    DELETE FROM SubmissionHistory
    WHERE HistoryId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTest]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteTestFile]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetCommentById]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetCommentsBySubmissionId]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetFilesBySubmissionId]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFilesBySubmissionId]
    @SubmissionId INT
AS
BEGIN
    SELECT * FROM SubmissionFiles WHERE SubmissionId = @SubmissionId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetHistorysBySubmissionId]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetHistorysBySubmissionId]
    @SubmissionId INT
AS
BEGIN
    SELECT * FROM SubmissionHistory
    WHERE SubmissionId = @SubmissionId
	ORDER BY SubmitTime desc
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentById]
    @StudentId VARCHAR(50)
AS
BEGIN
    SELECT * FROM Students WHERE StudentId = @StudentId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetSubmissionById]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestId]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestIdAndStudentId]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSubmissionByTestIdAndStudentId]
    @TestId INT, @StudentId VARCHAR(50)
AS
BEGIN
    SELECT * FROM Submissions WHERE TestId = @TestId and StudentId = @StudentId;
END;


GO
/****** Object:  StoredProcedure [dbo].[GetSubmissionFileById]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionFilesByTestId]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubmissionFilesByTestId]
    @TestId INT
AS
BEGIN
    SELECT sf.* 
    FROM SubmissionFiles sf 
    INNER JOIN Submissions s ON sf.SubmissionId = s.SubmissionId 
    WHERE TestId = @TestId and s.Status not like 'NotSubmitted'
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSubmissionHistoryById]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubmissionHistoryById]
    @Id INT
AS
BEGIN
    SELECT * FROM SubmissionHistory
    WHERE HistoryId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetSubmissions]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubmissions]
    @Page INT,
    @PageSize INT,
    @TestId INT,
    @SearchValue NVARCHAR(MAX),
    @SubmissionStatus NVARCHAR(50)
AS
BEGIN
WITH cte AS
    (
        SELECT s.*, st.FirstName, st.LastName, ROW_NUMBER() OVER (ORDER BY StartTime) AS RowNumber
        FROM Tests t JOIN Submissions s ON t.TestId=s.TestId JOIN Students st ON s.StudentId = st.StudentId
        WHERE t.TestId = @TestId
        AND (@SearchValue = N'' OR s.StudentId LIKE '%' + @SearchValue + '%' or st.FirstName like '%' + @SearchValue + '%' or st.LastName like '%' + @SearchValue + '%')
        AND (@SubmissionStatus = N'' OR s.Status IN (SELECT * FROM dbo.SplitString(@SubmissionStatus, ',')))
		)
    SELECT *
    FROM cte
    WHERE (@PageSize = 0)
        OR (RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize)
    ORDER BY RowNumber;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTeacherById]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTeacherById]
    @TeacherId VARCHAR(50)
AS
BEGIN
    SELECT * FROM Teachers WHERE TeacherId = @TeacherId;
END;



GO
/****** Object:  StoredProcedure [dbo].[GetTestById]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestFileById]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestFilesByTestId]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestsForStudentHome]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTestsForStudentHome]
    @Page INT,
    @PageSize INT,
    @StudentId VARCHAR(50)   
AS
BEGIN
DECLARE @CurrentDateTime DATETIME = GETDATE();
WITH cte AS
    (
        SELECT t.*, ROW_NUMBER() OVER (ORDER BY StartTime) AS RowNumber
        FROM Tests t join Submissions s on t.TestId=s.TestId
        WHERE s.StudentId = @StudentId
       
			AND (t.Status = 'InProgress' AND (dbo.GetTestStatus(t.StartTime, t.EndTime) = 'Upcoming' OR dbo.GetTestStatus(t.StartTime, t.EndTime) = 'Ongoing'))
       AND (@CurrentDateTime < t.EndTime OR t.EndTime IS NULL)
    )
    SELECT *
    FROM cte
    WHERE (@PageSize = 0)
        OR (RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize)
    ORDER BY RowNumber;
END;


GO
/****** Object:  StoredProcedure [dbo].[GetTestsOfStudent]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTestsOfStudent]
    @Page INT,
    @PageSize INT,
    @StudentId VARCHAR(50),
    @SearchValue NVARCHAR(MAX),
	@TestStatus NVARCHAR(20),
    @TestType NVARCHAR(20),
    @FromTime DATETIME,
    @ToTime DATETIME
AS
BEGIN
DECLARE @CurrentDateTime DATETIME = GETDATE();		
WITH cte AS
    (
        SELECT t.*, ROW_NUMBER() OVER (ORDER BY t.StartTime desc) AS RowNumber
        FROM Tests t join Submissions s on t.TestId=s.TestId
        WHERE s.StudentId = @StudentId
        AND (@SearchValue = N'' OR t.Title LIKE '%' + @SearchValue + '%')
        AND (@TestType = N'' OR t.TestType like @TestType)
			AND (@TestStatus = N'' OR (t.Status = 'InProgress' AND dbo.GetTestStatus(t.StartTime, t.EndTime) = @TestStatus))
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
/****** Object:  StoredProcedure [dbo].[GetTestsOfTeacher]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTestsOfTeacher]
    @Page INT,
    @PageSize INT,
    @TeacherId VARCHAR(50),
    @SearchValue NVARCHAR(MAX),
    @TestType NVARCHAR(20),
	@TestStatus NVARCHAR(20),
    @FromTime DATETIME,
    @ToTime DATETIME
AS
BEGIN
WITH cte AS
    (
        SELECT t.*, ROW_NUMBER() OVER (ORDER BY t.CreatedTime desc) AS RowNumber
        FROM Tests t
        WHERE t.TeacherId = @TeacherId
        AND (@SearchValue = N'' OR t.Title LIKE '%' + @SearchValue + '%')
        AND (@TestType = N'' OR t.TestType like @TestType)
		AND (@TestStatus = N'' OR t.Status like @TestStatus OR (t.Status = 'InProgress' AND dbo.GetTestStatus(t.StartTime, t.EndTime) = @TestStatus))
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
/****** Object:  StoredProcedure [dbo].[IsTestUsed]    Script Date: 03/06/2024 7:46:03 CH ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateComment]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateComment]
    @CommentId INT,
    @Body NVARCHAR(MAX),
    @TeacherId VARCHAR(50),
    @SubmissionId INT,
    @CommentedTime DATETIME
AS
BEGIN
    UPDATE Comments 
    SET Body = @Body, TeacherId = @TeacherId, SubmissionId = @SubmissionId, CommentedTime = @CommentedTime
    WHERE CommentId = @CommentId;
END;


GO
/****** Object:  StoredProcedure [dbo].[UpdateSubmission]    Script Date: 03/06/2024 7:46:03 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSubmission]
    @SubmissionId INT,
    @SubmitTime DATETIME,
    @Status NVARCHAR(20)
AS
BEGIN
    UPDATE Submissions
    SET SubmitTime = @SubmitTime,
        Status = @Status
    WHERE SubmissionId = @SubmissionId;
END;


GO
/****** Object:  StoredProcedure [dbo].[UpdateTest]    Script Date: 03/06/2024 7:46:03 CH ******/
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
    @CanSubmitLate BIT,
    @LastUpdateTime DATETIME,
    @TestType NVARCHAR(20),
	@Semester VARCHAR(50),
	@ModuleId VARCHAR(50),
    @TeacherId VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra và cập nhật trạng thái
        IF @Status = 'Upcoming' OR @Status = 'Ongoing' OR @Status = 'Finished'
        BEGIN
            SET @Status = 'InProgress';
        END

        -- Thực hiện câu lệnh UPDATE
        UPDATE Tests
        SET Title = @Title,
            Instruction = @Instruction,
            StartTime = @StartTime,
            EndTime = @EndTime,
            Status = @Status,
            IsCheckIP = @IsCheckIP,
            IsConductedAtSchool = @IsConductedAtSchool,
            LastUpdateTime = @LastUpdateTime,
            CanSubmitLate = @CanSubmitLate,
            TestType = @TestType,
            TeacherId = @TeacherId,
			ModuleId = @ModuleId,
            Semester = @Semester
        WHERE TestId = @TestId;

        -- Hoàn tất giao dịch
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Xử lý lỗi và rollback giao dịch nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Tùy chỉnh thêm nếu bạn muốn log lỗi hoặc thông báo lỗi cụ thể
        THROW;
    END CATCH
END;
GO
USE [master]
GO
ALTER DATABASE [QuanLyKyThi] SET  READ_WRITE 
GO
