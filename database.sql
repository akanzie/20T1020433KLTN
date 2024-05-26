USE [QuanLyKyThi]
GO
/****** Object:  UserDefinedFunction [dbo].[GetTestStatus]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  Table [dbo].[Comments]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  Table [dbo].[Students]    Script Date: 26/05/2024 8:20:19 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](10) NULL,
	[LastName] [nvarchar](25) NULL,
	[Email] [nvarchar](255) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubmissionFiles]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  Table [dbo].[Submissions]    Script Date: 26/05/2024 8:20:19 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Submissions](
	[SubmissionId] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[IPAddress] [varchar](50) NULL,
	[SubmittedTime] [datetime] NULL,
	[Status] [nvarchar](20) NULL,
	[StudentId] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Submissions] PRIMARY KEY CLUSTERED 
(
	[SubmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  Table [dbo].[TestFiles]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  Table [dbo].[Tests]    Script Date: 26/05/2024 8:20:19 SA ******/
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
 CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020000', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000102', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000105', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000108', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000111', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000114', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000117', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200012', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000120', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000123', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000126', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000129', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000132', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000135', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000138', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000141', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000144', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000147', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200015', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000150', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000153', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000156', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000159', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000162', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000165', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000168', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000171', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000174', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000177', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200018', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000180', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000183', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000186', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000189', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000192', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000195', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000198', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000201', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000204', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000207', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200021', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000210', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000213', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000216', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000219', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000222', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000225', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000228', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000231', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000234', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000237', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200024', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000240', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000243', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000246', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000249', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000252', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000255', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000258', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000261', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000264', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000267', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200027', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000270', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000273', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000276', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000279', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000282', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000285', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000288', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000291', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000294', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T102000297', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020003', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200030', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200033', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200036', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200039', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200042', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200045', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200048', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200051', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200054', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200057', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020006', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200060', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200063', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200066', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200069', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200072', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200075', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200078', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200081', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T10200084', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020400', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020401', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020402', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020403', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020404', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020405', N'A', N'Nguyễn Văn', NULL)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020406', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020407', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020408', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020409', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020410', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020411', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020412', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020413', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020414', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020415', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020416', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020417', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020418', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020419', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020420', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020421', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020422', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020423', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020424', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020425', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020426', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020427', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020428', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020429', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020430', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020431', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020432', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020433', N'Kiệt', N'Châu Anh', N'')
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020434', N'A', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020435', N'B', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020436', N'C', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020437', N'D', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020438', N'E', N'Nguyễn Văn', NULL)
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020439', N'F', N'Nguyễn Văn', NULL)
GO
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'aa32194c-a13d-4252-82e9-52f3bad21005', N'aa32194c-a13d-4252-82e9-52f3bad21005_databasev3.sql', N'H:\File\Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1\Submission\aa32194c-a13d-4252-82e9-52f3bad21005_databasev3.sql', N'application/octet-stream', CAST(173882 AS Decimal(18, 0)), 1916, N'databasev3.sql')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'fcb3f8a0-1179-4cf8-992b-69fda0d38ce2', N'fcb3f8a0-1179-4cf8-992b-69fda0d38ce2_databasev3 (1).sql', N'H:\File\Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1\Submission\fcb3f8a0-1179-4cf8-992b-69fda0d38ce2_databasev3 (1).sql', N'application/octet-stream', CAST(173882 AS Decimal(18, 0)), 1916, N'databasev3 (1).sql')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'5849444e-8b95-45d2-ac92-6b271da3c812', N'5849444e-8b95-45d2-ac92-6b271da3c812_StudentRepository.cs', N'H:\File\Kiểm tra giữa kỳ - Thiết kế cơ sở dữ liệu\Submission\5849444e-8b95-45d2-ac92-6b271da3c812_StudentRepository.cs', N'text/plain', CAST(2266 AS Decimal(18, 0)), 1900, N'StudentRepository.cs')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'0f5ddd07-67b7-4b18-8b63-6d787d60aa64', N'0f5ddd07-67b7-4b18-8b63-6d787d60aa64_bangdiem_1_3 (4).xls', N'H:\File\Kiểm tra giữa kỳ - Thiết kế cơ sở dữ liệu\Submission\0f5ddd07-67b7-4b18-8b63-6d787d60aa64_bangdiem_1_3 (4).xls', N'application/vnd.ms-excel', CAST(59904 AS Decimal(18, 0)), 1900, N'bangdiem_1_3 (4).xls')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'35015415-248e-4373-be64-7b0c2eda9285', N'35015415-248e-4373-be64-7b0c2eda9285_CreateExam-dangnhap.jpg', N'H:\File\Kiểm tra giữa kỳ - Thiết kế cơ sở dữ liệu\Submission\35015415-248e-4373-be64-7b0c2eda9285_CreateExam-dangnhap.jpg', N'image/jpeg', CAST(62681 AS Decimal(18, 0)), 1900, N'CreateExam-dangnhap.jpg')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'06fccea9-fd9b-4a4a-a068-c78840b80e7d', N'06fccea9-fd9b-4a4a-a068-c78840b80e7d_xembainop.jpg', N'H:\File\Kiểm tra giữa kỳ - Thiết kế cơ sở dữ liệu\Submission\06fccea9-fd9b-4a4a-a068-c78840b80e7d_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 1900, N'xembainop.jpg')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'99736c22-318c-4799-b531-d26494bcc24e', N'99736c22-318c-4799-b531-d26494bcc24e_dp.docx', N'H:\File\Kiểm tra giữa kỳ - Thiết kế cơ sở dữ liệu\Submission\99736c22-318c-4799-b531-d26494bcc24e_dp.docx', N'application/vnd.openxmlformats-officedocument.wordprocessingml.document', CAST(14114 AS Decimal(18, 0)), 1900, N'dp.docx')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'2f16f64d-49c3-4cfc-bef9-f69b8b662c48', N'2f16f64d-49c3-4cfc-bef9-f69b8b662c48_databasev3.sql', N'H:\File\Kiểm tra giữa kỳ - Thiết kế cơ sở dữ liệu\Submission\2f16f64d-49c3-4cfc-bef9-f69b8b662c48_databasev3.sql', N'application/octet-stream', CAST(173882 AS Decimal(18, 0)), 1900, N'databasev3.sql')
GO
SET IDENTITY_INSERT [dbo].[Submissions] ON 

INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1860, 3225, NULL, NULL, N'NotSubmitted', N'20T102000282')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1861, 3225, NULL, NULL, N'NotSubmitted', N'20T102000285')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1862, 3225, NULL, NULL, N'NotSubmitted', N'20T102000288')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1863, 3225, NULL, NULL, N'NotSubmitted', N'20T102000291')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1864, 3225, NULL, NULL, N'NotSubmitted', N'20T102000294')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1865, 3225, NULL, NULL, N'NotSubmitted', N'20T102000297')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1866, 3204, NULL, NULL, N'NotSubmitted', N'20T1020003')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1867, 3232, NULL, NULL, N'NotSubmitted', N'20T1020400')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1868, 3232, NULL, NULL, N'NotSubmitted', N'20T1020401')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1869, 3232, NULL, NULL, N'NotSubmitted', N'20T1020402')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1870, 3232, NULL, NULL, N'NotSubmitted', N'20T1020403')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1871, 3232, NULL, NULL, N'NotSubmitted', N'20T1020404')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1872, 3232, NULL, NULL, N'NotSubmitted', N'20T1020405')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1873, 3232, NULL, NULL, N'NotSubmitted', N'20T1020406')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1874, 3232, NULL, NULL, N'NotSubmitted', N'20T1020407')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1875, 3232, NULL, NULL, N'NotSubmitted', N'20T1020408')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1876, 3232, NULL, NULL, N'NotSubmitted', N'20T1020409')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1877, 3232, NULL, NULL, N'NotSubmitted', N'20T1020410')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1878, 3232, NULL, NULL, N'NotSubmitted', N'20T1020411')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1879, 3232, NULL, NULL, N'NotSubmitted', N'20T1020412')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1880, 3232, NULL, NULL, N'NotSubmitted', N'20T1020413')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1881, 3232, NULL, NULL, N'NotSubmitted', N'20T1020414')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1882, 3232, NULL, NULL, N'NotSubmitted', N'20T1020415')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1883, 3232, NULL, NULL, N'NotSubmitted', N'20T1020416')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1884, 3232, NULL, NULL, N'NotSubmitted', N'20T1020417')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1885, 3232, NULL, NULL, N'NotSubmitted', N'20T1020418')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1886, 3232, NULL, NULL, N'NotSubmitted', N'20T1020419')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1887, 3232, NULL, NULL, N'NotSubmitted', N'20T1020420')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1888, 3232, NULL, NULL, N'NotSubmitted', N'20T1020421')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1889, 3232, NULL, NULL, N'NotSubmitted', N'20T1020422')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1890, 3232, NULL, NULL, N'NotSubmitted', N'20T1020423')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1891, 3232, NULL, NULL, N'NotSubmitted', N'20T1020424')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1892, 3232, NULL, NULL, N'NotSubmitted', N'20T1020425')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1893, 3232, NULL, NULL, N'NotSubmitted', N'20T1020426')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1894, 3232, NULL, NULL, N'NotSubmitted', N'20T1020427')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1895, 3232, NULL, NULL, N'NotSubmitted', N'20T1020428')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1896, 3232, NULL, NULL, N'NotSubmitted', N'20T1020429')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1897, 3232, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1898, 3232, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1899, 3232, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1900, 3232, N'::1', CAST(N'2024-05-20T20:28:26.063' AS DateTime), N'Submitted', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1901, 2012, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1902, 2012, NULL, NULL, N'Absent', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1903, 3202, NULL, NULL, N'NotSubmitted', N'20T1020429')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1904, 3202, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1905, 3202, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1906, 3202, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1907, 3202, NULL, NULL, N'NotSubmitted', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1908, 3204, NULL, NULL, N'NotSubmitted', N'20T1020429')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1909, 3204, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1910, 3204, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1911, 3204, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1912, 3204, NULL, NULL, N'NotSubmitted', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1913, 3224, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1914, 3224, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1915, 3224, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1916, 3224, N'::1', CAST(N'2024-05-20T20:12:49.833' AS DateTime), N'LateSubmission', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1917, 3229, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1918, 3229, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1919, 3229, NULL, NULL, N'NotSubmitted', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1920, 3233, NULL, NULL, N'NotSubmitted', N'20T1020425')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1921, 3233, NULL, NULL, N'NotSubmitted', N'20T1020426')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1922, 3233, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1923, 3233, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1924, 3233, NULL, NULL, N'NotSubmitted', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1925, 3234, NULL, NULL, N'NotSubmitted', N'20T1020400')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1926, 3234, NULL, NULL, N'NotSubmitted', N'20T1020401')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1927, 3234, NULL, NULL, N'NotSubmitted', N'20T1020402')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1928, 3234, NULL, NULL, N'NotSubmitted', N'20T1020403')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1929, 3234, NULL, NULL, N'NotSubmitted', N'20T1020404')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1930, 3234, NULL, NULL, N'NotSubmitted', N'20T1020405')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1931, 3234, NULL, NULL, N'NotSubmitted', N'20T1020406')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1932, 3234, NULL, NULL, N'NotSubmitted', N'20T1020407')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1933, 3234, NULL, NULL, N'NotSubmitted', N'20T1020408')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1934, 3234, NULL, NULL, N'NotSubmitted', N'20T1020409')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1935, 3234, NULL, NULL, N'NotSubmitted', N'20T1020410')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1936, 3234, NULL, NULL, N'NotSubmitted', N'20T1020411')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1937, 3234, NULL, NULL, N'NotSubmitted', N'20T1020412')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1938, 3234, NULL, NULL, N'NotSubmitted', N'20T1020413')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1939, 3234, NULL, NULL, N'NotSubmitted', N'20T1020414')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1940, 3234, NULL, NULL, N'NotSubmitted', N'20T1020415')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1941, 3234, NULL, NULL, N'NotSubmitted', N'20T1020416')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1942, 3234, NULL, NULL, N'NotSubmitted', N'20T1020417')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1943, 3234, NULL, NULL, N'NotSubmitted', N'20T1020418')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1944, 3234, NULL, NULL, N'NotSubmitted', N'20T1020419')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1945, 3234, NULL, NULL, N'NotSubmitted', N'20T1020420')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1946, 3234, NULL, NULL, N'NotSubmitted', N'20T1020421')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1947, 3234, NULL, NULL, N'NotSubmitted', N'20T1020422')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1948, 3234, NULL, NULL, N'NotSubmitted', N'20T1020423')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1949, 3234, NULL, NULL, N'NotSubmitted', N'20T1020424')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1950, 3234, NULL, NULL, N'NotSubmitted', N'20T1020425')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1951, 3234, NULL, NULL, N'NotSubmitted', N'20T1020426')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1952, 3234, NULL, NULL, N'NotSubmitted', N'20T1020427')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1953, 3234, NULL, NULL, N'NotSubmitted', N'20T1020428')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1954, 3234, NULL, NULL, N'NotSubmitted', N'20T1020429')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1955, 3234, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1956, 3234, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1957, 3234, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1958, 3234, NULL, NULL, N'NotSubmitted', N'20T1020433')
GO
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1959, 3235, NULL, NULL, N'NotSubmitted', N'20T1020400')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1960, 3235, NULL, NULL, N'NotSubmitted', N'20T1020401')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1961, 3235, NULL, NULL, N'NotSubmitted', N'20T1020402')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1962, 3235, NULL, NULL, N'NotSubmitted', N'20T1020403')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1963, 3235, NULL, NULL, N'NotSubmitted', N'20T1020404')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1964, 3235, NULL, NULL, N'NotSubmitted', N'20T1020405')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1965, 3235, NULL, NULL, N'NotSubmitted', N'20T1020406')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1966, 3235, NULL, NULL, N'NotSubmitted', N'20T1020407')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1967, 3235, NULL, NULL, N'NotSubmitted', N'20T1020408')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1968, 3235, NULL, NULL, N'NotSubmitted', N'20T1020409')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1969, 3235, NULL, NULL, N'NotSubmitted', N'20T1020410')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1970, 3235, NULL, NULL, N'NotSubmitted', N'20T1020411')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1971, 3235, NULL, NULL, N'NotSubmitted', N'20T1020412')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1972, 3235, NULL, NULL, N'NotSubmitted', N'20T1020413')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1973, 3235, NULL, NULL, N'NotSubmitted', N'20T1020414')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1974, 3235, NULL, NULL, N'NotSubmitted', N'20T1020415')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1975, 3235, NULL, NULL, N'NotSubmitted', N'20T1020416')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1976, 3235, NULL, NULL, N'NotSubmitted', N'20T1020417')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1977, 3235, NULL, NULL, N'NotSubmitted', N'20T1020418')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1978, 3235, NULL, NULL, N'NotSubmitted', N'20T1020419')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1979, 3235, NULL, NULL, N'NotSubmitted', N'20T1020420')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1980, 3235, NULL, NULL, N'NotSubmitted', N'20T1020421')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1981, 3235, NULL, NULL, N'NotSubmitted', N'20T1020422')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1982, 3235, NULL, NULL, N'NotSubmitted', N'20T1020423')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1983, 3235, NULL, NULL, N'NotSubmitted', N'20T1020424')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1984, 3235, NULL, NULL, N'NotSubmitted', N'20T1020425')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1985, 3235, NULL, NULL, N'NotSubmitted', N'20T1020426')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1986, 3235, NULL, NULL, N'NotSubmitted', N'20T1020427')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1987, 3235, NULL, NULL, N'NotSubmitted', N'20T1020428')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1988, 3235, NULL, NULL, N'NotSubmitted', N'20T1020429')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1989, 3235, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1990, 3235, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1991, 3235, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1992, 3235, NULL, NULL, N'NotSubmitted', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1993, 3236, NULL, NULL, N'NotSubmitted', N'20T1020400')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1994, 3236, NULL, NULL, N'NotSubmitted', N'20T1020401')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1995, 3236, NULL, NULL, N'NotSubmitted', N'20T1020402')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1996, 3236, NULL, NULL, N'NotSubmitted', N'20T1020403')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1997, 3236, NULL, NULL, N'NotSubmitted', N'20T1020404')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1998, 3236, NULL, NULL, N'NotSubmitted', N'20T1020405')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (1999, 3236, NULL, NULL, N'NotSubmitted', N'20T1020406')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2000, 3236, NULL, NULL, N'NotSubmitted', N'20T1020407')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2001, 3236, NULL, NULL, N'NotSubmitted', N'20T1020408')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2002, 3236, NULL, NULL, N'NotSubmitted', N'20T1020409')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2003, 3236, NULL, NULL, N'NotSubmitted', N'20T1020410')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2004, 3236, NULL, NULL, N'NotSubmitted', N'20T1020411')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2005, 3236, NULL, NULL, N'NotSubmitted', N'20T1020412')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2006, 3236, NULL, NULL, N'NotSubmitted', N'20T1020413')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2007, 3236, NULL, NULL, N'NotSubmitted', N'20T1020414')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2008, 3236, NULL, NULL, N'NotSubmitted', N'20T1020415')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2009, 3236, NULL, NULL, N'NotSubmitted', N'20T1020416')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2010, 3236, NULL, NULL, N'NotSubmitted', N'20T1020417')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2011, 3236, NULL, NULL, N'NotSubmitted', N'20T1020418')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2012, 3236, NULL, NULL, N'NotSubmitted', N'20T1020419')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2013, 3236, NULL, NULL, N'NotSubmitted', N'20T1020420')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2014, 3236, NULL, NULL, N'NotSubmitted', N'20T1020421')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2015, 3236, NULL, NULL, N'NotSubmitted', N'20T1020422')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2016, 3236, NULL, NULL, N'NotSubmitted', N'20T1020423')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2017, 3236, NULL, NULL, N'NotSubmitted', N'20T1020424')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2018, 3236, NULL, NULL, N'NotSubmitted', N'20T1020425')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2019, 3236, NULL, NULL, N'NotSubmitted', N'20T1020426')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2020, 3236, NULL, NULL, N'NotSubmitted', N'20T1020427')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2021, 3236, NULL, NULL, N'NotSubmitted', N'20T1020428')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2022, 3236, NULL, NULL, N'NotSubmitted', N'20T1020429')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2023, 3236, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2024, 3236, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2025, 3236, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2026, 3236, NULL, NULL, N'NotSubmitted', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2027, 4226, NULL, NULL, N'NotSubmitted', N'20T1020400')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2028, 4226, NULL, NULL, N'NotSubmitted', N'20T1020401')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2029, 4226, NULL, NULL, N'NotSubmitted', N'20T1020402')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2030, 4226, NULL, NULL, N'NotSubmitted', N'20T1020403')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2031, 4226, NULL, NULL, N'NotSubmitted', N'20T1020404')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2032, 4226, NULL, NULL, N'NotSubmitted', N'20T1020405')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2033, 4226, NULL, NULL, N'NotSubmitted', N'20T1020406')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2034, 4226, NULL, NULL, N'NotSubmitted', N'20T1020407')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2035, 4226, NULL, NULL, N'NotSubmitted', N'20T1020408')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2036, 4226, NULL, NULL, N'NotSubmitted', N'20T1020409')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2037, 4226, NULL, NULL, N'NotSubmitted', N'20T1020410')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2038, 4226, NULL, NULL, N'NotSubmitted', N'20T1020411')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2039, 4226, NULL, NULL, N'NotSubmitted', N'20T1020412')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2040, 4226, NULL, NULL, N'NotSubmitted', N'20T1020413')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2041, 4226, NULL, NULL, N'NotSubmitted', N'20T1020414')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2042, 4226, NULL, NULL, N'NotSubmitted', N'20T1020415')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2043, 4226, NULL, NULL, N'NotSubmitted', N'20T1020416')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2044, 4226, NULL, NULL, N'NotSubmitted', N'20T1020417')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2045, 4226, NULL, NULL, N'NotSubmitted', N'20T1020418')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2046, 4226, NULL, NULL, N'NotSubmitted', N'20T1020419')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2047, 4226, NULL, NULL, N'NotSubmitted', N'20T1020420')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2048, 4226, NULL, NULL, N'NotSubmitted', N'20T1020421')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2049, 4226, NULL, NULL, N'NotSubmitted', N'20T1020422')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2050, 4226, NULL, NULL, N'NotSubmitted', N'20T1020423')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2051, 4226, NULL, NULL, N'NotSubmitted', N'20T1020424')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2052, 4226, NULL, NULL, N'NotSubmitted', N'20T1020425')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2053, 4226, NULL, NULL, N'NotSubmitted', N'20T1020426')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2054, 4226, NULL, NULL, N'NotSubmitted', N'20T1020427')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2055, 4226, NULL, NULL, N'NotSubmitted', N'20T1020428')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2056, 4226, NULL, NULL, N'NotSubmitted', N'20T1020429')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2057, 4226, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2058, 4226, NULL, NULL, N'NotSubmitted', N'20T1020431')
GO
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2059, 4226, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2060, 4226, NULL, NULL, N'NotSubmitted', N'20T1020433')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2061, 4227, NULL, NULL, N'NotSubmitted', N'20T1020400')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2062, 4227, NULL, NULL, N'NotSubmitted', N'20T1020401')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2063, 4227, NULL, NULL, N'NotSubmitted', N'20T1020402')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2064, 4227, NULL, NULL, N'NotSubmitted', N'20T1020403')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2065, 4227, NULL, NULL, N'NotSubmitted', N'20T1020404')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2066, 4227, NULL, NULL, N'NotSubmitted', N'20T1020405')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2067, 4227, NULL, NULL, N'NotSubmitted', N'20T1020406')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2068, 4227, NULL, NULL, N'NotSubmitted', N'20T1020407')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2069, 4227, NULL, NULL, N'NotSubmitted', N'20T1020408')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2070, 4227, NULL, NULL, N'NotSubmitted', N'20T1020409')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2071, 4227, NULL, NULL, N'NotSubmitted', N'20T1020410')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2072, 4227, NULL, NULL, N'NotSubmitted', N'20T1020411')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2073, 4227, NULL, NULL, N'NotSubmitted', N'20T1020412')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2074, 4227, NULL, NULL, N'NotSubmitted', N'20T1020413')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2075, 4227, NULL, NULL, N'NotSubmitted', N'20T1020414')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2076, 4227, NULL, NULL, N'NotSubmitted', N'20T1020415')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2077, 4227, NULL, NULL, N'NotSubmitted', N'20T1020416')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2078, 4227, NULL, NULL, N'NotSubmitted', N'20T1020417')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2079, 4227, NULL, NULL, N'NotSubmitted', N'20T1020418')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2080, 4227, NULL, NULL, N'NotSubmitted', N'20T1020419')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2081, 4227, NULL, NULL, N'NotSubmitted', N'20T1020420')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2082, 4227, NULL, NULL, N'NotSubmitted', N'20T1020421')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2083, 4227, NULL, NULL, N'NotSubmitted', N'20T1020422')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2084, 4227, NULL, NULL, N'NotSubmitted', N'20T1020423')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2085, 4227, NULL, NULL, N'NotSubmitted', N'20T1020424')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2086, 4227, NULL, NULL, N'NotSubmitted', N'20T1020425')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2087, 4227, NULL, NULL, N'NotSubmitted', N'20T1020426')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2088, 4227, NULL, NULL, N'NotSubmitted', N'20T1020427')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2089, 4227, NULL, NULL, N'NotSubmitted', N'20T1020428')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2090, 4227, NULL, NULL, N'NotSubmitted', N'20T1020429')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2091, 4227, NULL, NULL, N'NotSubmitted', N'20T1020430')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2092, 4227, NULL, NULL, N'NotSubmitted', N'20T1020431')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2093, 4227, NULL, NULL, N'NotSubmitted', N'20T1020432')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [IPAddress], [SubmittedTime], [Status], [StudentId]) VALUES (2094, 4227, NULL, NULL, N'NotSubmitted', N'20T1020433')
SET IDENTITY_INSERT [dbo].[Submissions] OFF
GO
INSERT [dbo].[Teachers] ([TeacherId], [TeacherName], [Email]) VALUES (N'DHT0220', N'Trần Nguyên Phong', N'tnphong@husc.edu.vn')
INSERT [dbo].[Teachers] ([TeacherId], [TeacherName], [Email]) VALUES (N'DHT0221', N'Nguyễn Văn A', N'tnphong@husc.edu.vn')
GO
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'351fba23-5e51-49f3-b0c9-0519b01d8af9', N'351fba23-5e51-49f3-b0c9-0519b01d8af9_biên bản họp CM.jpg', N'H:\File\Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1\Test\351fba23-5e51-49f3-b0c9-0519b01d8af9_biên bản họp CM.jpg', N'image/jpeg', CAST(329359 AS Decimal(18, 0)), 3234, N'biên bản họp CM.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'c26977b4-0a3f-4132-a9d0-2541390bb46f', N'c26977b4-0a3f-4132-a9d0-2541390bb46f_ảnh TN ĐH.jpg', N'H:\File\Test\c26977b4-0a3f-4132-a9d0-2541390bb46f_ảnh TN ĐH.jpg', N'image/jpeg', CAST(160705 AS Decimal(18, 0)), 4228, N'ảnh TN ĐH.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'cf0cd695-45e9-4c81-baff-413e2649394f', N'cf0cd695-45e9-4c81-baff-413e2649394f_ảnh cc tin học.jpg', N'H:\File\Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1\Test\cf0cd695-45e9-4c81-baff-413e2649394f_ảnh cc tin học.jpg', N'image/jpeg', CAST(313561 AS Decimal(18, 0)), 3233, N'ảnh cc tin học.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'617707fb-74d8-4cbc-be95-456ecfa18873', N'617707fb-74d8-4cbc-be95-456ecfa18873_database insta.xlsx', N'H:\File\Kiểm tra giữa kỳ - Mẫu thiết kế- Nhóm 1,2\Test\617707fb-74d8-4cbc-be95-456ecfa18873_database insta.xlsx', N'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', CAST(12100 AS Decimal(18, 0)), 3225, N'database insta.xlsx')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'7c55bf8b-d946-4c27-bf78-6afa9c1b2b47', N'7c55bf8b-d946-4c27-bf78-6afa9c1b2b47_outline chuc nang.docx', N'H:\File\Kiểm tra giữa kỳ- Lập trình hướng đối tượng - Nhóm 1,2\Test\7c55bf8b-d946-4c27-bf78-6afa9c1b2b47_outline chuc nang.docx', N'application/vnd.openxmlformats-officedocument.wordprocessingml.document', CAST(15755 AS Decimal(18, 0)), 3205, N'outline chuc nang.docx')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'f9bf3cee-7a5c-411e-b042-9744ebc4c775', N'f9bf3cee-7a5c-411e-b042-9744ebc4c775_Converter.cs', N'D:\File\2004\Test\f9bf3cee-7a5c-411e-b042-9744ebc4c775_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2004, N'Converter.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'36a419fa-f42e-4395-b966-a04bca29ccd6', N'36a419fa-f42e-4395-b966-a04bca29ccd6_databasev2.sql', N'H:\File\Test\36a419fa-f42e-4395-b966-a04bca29ccd6_databasev2.sql', N'application/octet-stream', CAST(163570 AS Decimal(18, 0)), 3232, N'databasev2.sql')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'1eaf501b-77b8-41a9-b5eb-a8590e15b8ab', N'1eaf501b-77b8-41a9-b5eb-a8590e15b8ab_StudentRepository.cs', N'H:\File\Kiểm tra - Kỹ thuật lập trình\Test\1eaf501b-77b8-41a9-b5eb-a8590e15b8ab_StudentRepository.cs', N'text/plain', CAST(2266 AS Decimal(18, 0)), 2012, N'StudentRepository.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'72e329e8-60a7-4c6b-a200-aac566a36423', N'72e329e8-60a7-4c6b-a200-aac566a36423_databasev3.sql', N'H:\File\Thi kết thúc học phần - Nhập môn - Nhóm 3\Test\72e329e8-60a7-4c6b-a200-aac566a36423_databasev3.sql', N'application/octet-stream', CAST(173882 AS Decimal(18, 0)), 2004, N'databasev3.sql')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'3deaba47-757c-4b58-a5f5-b0ac34437919', N'3deaba47-757c-4b58-a5f5-b0ac34437919_databasev3.sql', N'H:\File\Test\3deaba47-757c-4b58-a5f5-b0ac34437919_databasev3.sql', N'application/octet-stream', CAST(173882 AS Decimal(18, 0)), 4226, N'databasev3.sql')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'736985bd-6b02-48d0-b4ea-b0e47c832812', N'736985bd-6b02-48d0-b4ea-b0e47c832812_WebSecurityModels.cs', N'D:\File\2012\Test\736985bd-6b02-48d0-b4ea-b0e47c832812_WebSecurityModels.cs', N'text/plain', CAST(6640 AS Decimal(18, 0)), 2012, N'WebSecurityModels.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'22b4b39e-180d-4171-a23a-b279f059304d', N'22b4b39e-180d-4171-a23a-b279f059304d_databasev2.sql', N'H:\File\Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1\Test\22b4b39e-180d-4171-a23a-b279f059304d_databasev2.sql', N'application/octet-stream', CAST(163570 AS Decimal(18, 0)), 3224, N'databasev2.sql')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'b50f7b8f-4e4f-4346-8e14-b35f0e94b9b0', N'b50f7b8f-4e4f-4346-8e14-b35f0e94b9b0_biên bản họp CM.jpg', N'H:\File\Test\b50f7b8f-4e4f-4346-8e14-b35f0e94b9b0_biên bản họp CM.jpg', N'image/jpeg', CAST(329359 AS Decimal(18, 0)), 4229, N'biên bản họp CM.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'075d09c9-473e-4bcf-acff-b48fe13f33d7', N'075d09c9-473e-4bcf-acff-b48fe13f33d7_Student.cs', N'H:\File\Test\075d09c9-473e-4bcf-acff-b48fe13f33d7_Student.cs', N'text/plain', CAST(380 AS Decimal(18, 0)), 4227, N'Student.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'd254035c-8c70-4ec9-a429-c430950a12b9', N'd254035c-8c70-4ec9-a429-c430950a12b9_outline chuc nang.docx', N'H:\File\Test\d254035c-8c70-4ec9-a429-c430950a12b9_outline chuc nang.docx', N'application/vnd.openxmlformats-officedocument.wordprocessingml.document', CAST(15755 AS Decimal(18, 0)), 3204, N'outline chuc nang.docx')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'd55dbe75-908e-4ed3-abde-d7e1ea696c9e', N'd55dbe75-908e-4ed3-abde-d7e1ea696c9e_outline chuc nang.docx', N'H:\File\Test\d55dbe75-908e-4ed3-abde-d7e1ea696c9e_outline chuc nang.docx', N'application/vnd.openxmlformats-officedocument.wordprocessingml.document', CAST(15755 AS Decimal(18, 0)), 3225, N'outline chuc nang.docx')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'73f36096-bc5c-4bae-9150-eb0a63234516', N'73f36096-bc5c-4bae-9150-eb0a63234516_databasev2.sql', N'H:\File\Thi kết thúc học phần - Lập trình web - Nhóm 1,2\Test\73f36096-bc5c-4bae-9150-eb0a63234516_databasev2.sql', N'application/octet-stream', CAST(163570 AS Decimal(18, 0)), 3202, N'databasev2.sql')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'a89aa148-5761-4be5-ac02-ffdffa5de870', N'a89aa148-5761-4be5-ac02-ffdffa5de870_databasev3.sql', N'H:\File\Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1\Test\a89aa148-5761-4be5-ac02-ffdffa5de870_databasev3.sql', N'application/octet-stream', CAST(173882 AS Decimal(18, 0)), 3236, N'databasev3.sql')
GO
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2004, N'Thi kết thúc học phần - Nhập môn - Nhóm 3', N'<ol><li><strong>Câu 1: Hãy kể tên các&nbsp;</strong></li><li><strong>Câu 2: </strong><i><strong>Nghiêng</strong></i></li></ol>', CAST(N'2024-05-21T20:00:00.000' AS DateTime), CAST(N'2024-05-22T20:00:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-04-09T13:42:16.293' AS DateTime), CAST(N'2024-05-20T20:00:11.223' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2012, N'Kiểm tra - Kỹ thuật lập trình', N'- Một cộng một bằng mấy?', CAST(N'2024-04-14T14:38:00.000' AS DateTime), CAST(N'2024-04-25T14:38:00.000' AS DateTime), N'InProgress', 0, 0, 0, CAST(N'2024-04-09T14:38:43.713' AS DateTime), CAST(N'2024-04-26T11:29:52.413' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3202, N'Kiểm tra học phần - Lập trình web - Nhóm 1,2', N'', CAST(N'2024-05-22T20:01:00.000' AS DateTime), CAST(N'2024-05-24T20:01:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-05-12T14:13:34.450' AS DateTime), CAST(N'2024-05-20T20:03:20.417' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3204, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', CAST(N'2024-05-18T19:38:00.000' AS DateTime), CAST(N'2024-05-19T19:38:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-05-12T14:27:47.860' AS DateTime), CAST(N'2024-05-18T19:38:59.233' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3205, N'Kiểm tra giữa kỳ- Lập trình hướng đối tượng - Nhóm 1,2', N'<p><strong>ABC</strong></p>', CAST(N'2024-05-18T13:40:00.000' AS DateTime), CAST(N'2024-05-19T13:40:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-05-12T14:55:21.523' AS DateTime), CAST(N'2024-05-17T13:41:18.563' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3224, N'Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1', N'<p><strong>Câu 1: Một cộng một bằng mấy?</strong></p>', CAST(N'2024-05-15T09:38:00.000' AS DateTime), CAST(N'2024-05-16T09:38:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-05-13T16:45:44.253' AS DateTime), CAST(N'2024-05-15T10:10:03.347' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3225, N'Kiểm tra giữa kỳ - Mẫu thiết kế- Nhóm 1,2', N'<p><strong>Câu 1: </strong><i><strong>Hãy đọc câu hỏi&nbsp;</strong></i></p>', CAST(N'2024-05-18T14:05:00.000' AS DateTime), CAST(N'2024-05-19T14:05:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-05-17T13:57:24.100' AS DateTime), CAST(N'2024-05-17T14:43:55.790' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3229, N'Lập trình hướng đối tượng - Kiểm tra giữa kỳ', N'', CAST(N'2024-05-21T20:04:00.000' AS DateTime), CAST(N'2024-05-22T20:04:00.000' AS DateTime), N'InProgress', 0, 0, 0, CAST(N'2024-05-17T18:52:59.980' AS DateTime), CAST(N'2024-05-20T20:05:04.100' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3232, N'Kiểm tra giữa kỳ - Thiết kế cơ sở dữ liệu', N'<p><strong>Câu 1: Hãy lập trình chức năng phân trang</strong></p>', CAST(N'2024-05-19T21:03:00.000' AS DateTime), CAST(N'2024-05-20T21:04:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-05-18T21:04:34.387' AS DateTime), CAST(N'2024-05-18T21:04:54.817' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3233, N'Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1', N'', CAST(N'2024-05-21T20:05:00.000' AS DateTime), CAST(N'2024-05-22T20:05:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-05-19T07:07:06.257' AS DateTime), CAST(N'2024-05-20T20:05:35.403' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3234, N'Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1', N'', NULL, NULL, N'InProgress', 1, 1, 0, CAST(N'2024-05-19T07:09:15.913' AS DateTime), CAST(N'2024-05-20T20:07:28.127' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3235, N'Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1', N'', NULL, NULL, N'InProgress', 0, 0, 0, CAST(N'2024-05-19T07:10:28.727' AS DateTime), CAST(N'2024-05-20T20:07:37.780' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3236, N'Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1', N'<p><strong>Kiểm tra giữa kỳ - Kỹ thuật lập trình - Nhóm 1</strong></p>', NULL, NULL, N'InProgress', 1, 1, 0, CAST(N'2024-05-19T07:11:11.743' AS DateTime), CAST(N'2024-05-20T20:07:59.393' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (4226, N'Java cơ bản - Kiểm tra giữa kỳ', N'', NULL, NULL, N'InProgress', 1, 1, 0, CAST(N'2024-05-20T20:09:06.033' AS DateTime), CAST(N'2024-05-20T20:09:40.487' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (4227, N'Java nâng cao - Kiểm tra giữa kỳ', N'', NULL, NULL, N'InProgress', 1, 1, 0, CAST(N'2024-05-20T20:10:01.697' AS DateTime), CAST(N'2024-05-20T20:10:07.227' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (4228, N'Kiểm tra học phần - Lập trình web - Nhóm 1,2', N'<ol><li><strong>Câu 1:&nbsp;</strong></li><li><strong>Câu 2:</strong></li></ol>', CAST(N'2024-05-23T20:17:00.000' AS DateTime), CAST(N'2024-05-24T20:17:00.000' AS DateTime), N'InProgress', 1, 1, 1, CAST(N'2024-05-20T20:17:33.123' AS DateTime), CAST(N'2024-05-20T20:18:08.043' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (4229, N'Kiểm tra học phần - Lập trình web - Nhóm 1,2', N'<ol><li>Xây dựng ứng dụng web</li><li>&nbsp;</li></ol>', CAST(N'2024-05-16T20:19:00.000' AS DateTime), NULL, N'InProgress', 1, 1, 0, CAST(N'2024-05-20T20:19:07.153' AS DateTime), CAST(N'2024-05-20T20:19:30.997' AS DateTime), N'Quiz', N'DHT0220')
SET IDENTITY_INSERT [dbo].[Tests] OFF
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
/****** Object:  StoredProcedure [dbo].[AddComment]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddStudent]    Script Date: 26/05/2024 8:20:19 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStudent]
    @StudentId VARCHAR(50),
    @FirstName NVARCHAR(50),
	 @LastName NVARCHAR(50),
    @Email NVARCHAR(50)
AS
BEGIN
    INSERT INTO Students (StudentId, FirstName, LastName, Email)
    VALUES (@StudentId, @FirstName, @LastName, @Email);
END;


GO
/****** Object:  StoredProcedure [dbo].[AddSubmission]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddSubmissionFile]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddTeacher]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddTest]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddTestFile]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[CheckIPAddressExists]    Script Date: 26/05/2024 8:20:19 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckIPAddressExists]
    @IPAddress NVARCHAR(50),
    @TestId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Exists BIT;

    -- Kiểm tra xem địa chỉ IP có tồn tại trong danh sách submission của TestId không
    IF EXISTS (SELECT 1 FROM Submissions WHERE IPAddress = @IPAddress AND TestId = @TestId)
        SET @Exists = 1;
    ELSE
        SET @Exists = 0;

    SELECT @Exists AS 'Exists';
END
GO
/****** Object:  StoredProcedure [dbo].[CountFilesBySubmissionId]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[CountSubmissions]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[CountTestsForStudentHome]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[CountTestsOfStudent]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[CountTestsOfTeacher]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteComment]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmission]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmissionFile]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteTest]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteTestFile]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetCommentById]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetCommentsBySubmissionId]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetFilesBySubmissionId]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionById]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestId]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestIdAndStudentId]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionFileById]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionFilesByTestId]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissions]    Script Date: 26/05/2024 8:20:19 SA ******/
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
        SELECT s.*, st.FirstName, st.LastName, st.Email, ROW_NUMBER() OVER (ORDER BY StartTime) AS RowNumber
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
/****** Object:  StoredProcedure [dbo].[GetTeacherById]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestById]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestFileById]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestFilesByTestId]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestsForStudentHome]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestsOfStudent]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestsOfTeacher]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[IsTestUsed]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateComment]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateSubmission]    Script Date: 26/05/2024 8:20:19 SA ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateTest]    Script Date: 26/05/2024 8:20:19 SA ******/
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
            TeacherId = @TeacherId
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
