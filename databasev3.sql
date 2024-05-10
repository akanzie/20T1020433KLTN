USE [QuanLyKyThi]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] NOT NULL,
	[Body] [nvarchar](255) NULL,
	[TeacherId] [varchar](50) NULL,
	[SubmissionId] [int] NULL,
	[CommentedTime] [datetime] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Students]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubmissionFiles]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  Table [dbo].[Submissions]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Submissions](
	[SubmissionId] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[StudentId] [varchar](50) NULL,
	[IPAddress] [nchar](15) NULL,
	[SubmittedTime] [datetime] NULL,
	[Status] [nvarchar](20) NULL,
 CONSTRAINT [PK_Submission] PRIMARY KEY CLUSTERED 
(
	[SubmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teachers](
	[TeacherId] [varchar](50) NOT NULL,
	[TeacherName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TestFiles]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  Table [dbo].[Tests]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Email]) VALUES (N'20T1020433', N'Kiệt', N'Châu Anh', N'20T1020433@husc.edu.vn')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'64cffb3e-6c86-4257-b2ed-0b71278d0e2e', N'64cffb3e-6c86-4257-b2ed-0b71278d0e2e_giaynhanxetthuctap.docx', N'D:\File\Thi kết thúc học phần - Nhập môn - Nhóm 3\Submission\64cffb3e-6c86-4257-b2ed-0b71278d0e2e_giaynhanxetthuctap.docx', N'application/vnd.openxmlformats-officedocument.word', CAST(19386 AS Decimal(18, 0)), 1006, N'giaynhanxetthuctap.docx')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'8e78f9cb-4737-4be6-8c97-b833f8b88993', N'8e78f9cb-4737-4be6-8c97-b833f8b88993_xemkythi.jpg', N'D:\File\Thi kết thúc học phần - Lập trình web - Nhóm 1,2\Submission\8e78f9cb-4737-4be6-8c97-b833f8b88993_xemkythi.jpg', N'image/jpeg', CAST(34580 AS Decimal(18, 0)), 2, N'xemkythi.jpg')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'92d30a22-0b9a-4b1d-a2da-d45bdca25521', N'92d30a22-0b9a-4b1d-a2da-d45bdca25521_nopbai (1).jpg', N'D:\File\Thi kết thúc học phần - Lập trình web - Nhóm 1,2\Submission\92d30a22-0b9a-4b1d-a2da-d45bdca25521_nopbai (1).jpg', N'image/jpeg', CAST(55878 AS Decimal(18, 0)), 2, N'nopbai (1).jpg')
INSERT [dbo].[SubmissionFiles] ([FileId], [FIleName], [FilePath], [MimeType], [Size], [SubmissionId], [OriginalName]) VALUES (N'3e4bca4f-3001-442b-b444-e5dc9c5dfa19', N'3e4bca4f-3001-442b-b444-e5dc9c5dfa19_nopbai.jpg', N'D:\File\Thi kết thúc học phần - Lập trình web - Nhóm 1,2\Submission\3e4bca4f-3001-442b-b444-e5dc9c5dfa19_nopbai.jpg', N'image/jpeg', CAST(45773 AS Decimal(18, 0)), 2, N'nopbai.jpg')
SET IDENTITY_INSERT [dbo].[Submissions] ON 

INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [StudentId], [IPAddress], [SubmittedTime], [Status]) VALUES (2, 1, N'20T1020433', N'::1            ', CAST(N'2024-05-06 19:45:57.293' AS DateTime), N'Submitted')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [StudentId], [IPAddress], [SubmittedTime], [Status]) VALUES (1005, 2012, N'20T1020433', NULL, CAST(N'2024-04-23 00:00:00.000' AS DateTime), N'Absent')
INSERT [dbo].[Submissions] ([SubmissionId], [TestId], [StudentId], [IPAddress], [SubmittedTime], [Status]) VALUES (1006, 2004, N'20T1020433', N'::1            ', CAST(N'2024-05-04 19:33:44.323' AS DateTime), N'Submitted')
SET IDENTITY_INSERT [dbo].[Submissions] OFF
INSERT [dbo].[Teachers] ([TeacherId], [TeacherName], [Email]) VALUES (N'1', N'Trần Nguyên Phong', N'tnphong@husc.edu.vn')
INSERT [dbo].[Teachers] ([TeacherId], [TeacherName], [Email]) VALUES (N'DHT0220', N'Trần Nguyên Phong', N'tnphong@husc.edu.vn')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'3dd72eec-f42c-4b04-9bff-0c44b6c8e40d', N'3dd72eec-f42c-4b04-9bff-0c44b6c8e40d_xembainop.jpg', N'H:\File\Test\3dd72eec-f42c-4b04-9bff-0c44b6c8e40d_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 3184, N'xembainop.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'7d6fcbc3-4e3a-43a4-8fac-0da907b669a0', N'7d6fcbc3-4e3a-43a4-8fac-0da907b669a0_xembainop.jpg', N'H:\File\Test\7d6fcbc3-4e3a-43a4-8fac-0da907b669a0_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 3095, N'xembainop.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'7007aa4e-9f8d-434e-9928-0eec6781f4f2', N'7007aa4e-9f8d-434e-9928-0eec6781f4f2_xoakythi.jpg', N'H:\File\Test\7007aa4e-9f8d-434e-9928-0eec6781f4f2_xoakythi.jpg', N'image/jpeg', CAST(33515 AS Decimal(18, 0)), 3185, N'xoakythi.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'267b0e69-c81e-4bdf-85ae-2002cd061bf6', N'267b0e69-c81e-4bdf-85ae-2002cd061bf6_xembainop.jpg', N'H:\File\Test\267b0e69-c81e-4bdf-85ae-2002cd061bf6_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 3188, N'xembainop.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'e204cbb8-c9be-4565-add8-23ee5a098f0e', N'e204cbb8-c9be-4565-add8-23ee5a098f0e_CV_ChauAnhKiet.pdf', N'H:\File\Test\e204cbb8-c9be-4565-add8-23ee5a098f0e_CV_ChauAnhKiet.pdf', N'application/pdf', CAST(124858 AS Decimal(18, 0)), 3091, N'CV_ChauAnhKiet.pdf')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'a871d303-4a0c-4bbf-a33e-29d15c1dd9d3', N'a871d303-4a0c-4bbf-a33e-29d15c1dd9d3_SelectListHelper.cs', N'D:\File\2009\Test\a871d303-4a0c-4bbf-a33e-29d15c1dd9d3_SelectListHelper.cs', N'text/plain', CAST(2845 AS Decimal(18, 0)), 2009, N'SelectListHelper.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'a415648c-3904-41e1-9fee-374bbd6df2a4', N'a415648c-3904-41e1-9fee-374bbd6df2a4_xembainop.jpg', N'H:\File\Test\a415648c-3904-41e1-9fee-374bbd6df2a4_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 3089, N'xembainop.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'81b56dff-f4a9-4eac-9003-46e7d6f257e7', N'81b56dff-f4a9-4eac-9003-46e7d6f257e7_CreateExam-Page-1.jpg', N'D:\File\Test\81b56dff-f4a9-4eac-9003-46e7d6f257e7_CreateExam-Page-1.jpg', N'image/jpeg', CAST(120441 AS Decimal(18, 0)), 2016, N'CreateExam-Page-1.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'8fb08cd1-45a4-4974-bb44-5b269add6e17', N'8fb08cd1-45a4-4974-bb44-5b269add6e17_SelectListHelper.cs', N'D:\File\2005\Test\8fb08cd1-45a4-4974-bb44-5b269add6e17_SelectListHelper.cs', N'text/plain', CAST(2845 AS Decimal(18, 0)), 2005, N'SelectListHelper.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'0063b28c-0800-4250-8cc3-5cf99cb6f559', N'0063b28c-0800-4250-8cc3-5cf99cb6f559_SelectListHelper.cs', N'D:\File\2011\Test\0063b28c-0800-4250-8cc3-5cf99cb6f559_SelectListHelper.cs', N'text/plain', CAST(2845 AS Decimal(18, 0)), 2011, N'SelectListHelper.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'27a87177-5b08-4f0e-8d12-5e09f6bcbd39', N'27a87177-5b08-4f0e-8d12-5e09f6bcbd39_Converter.cs', N'D:\File\2006\Test\27a87177-5b08-4f0e-8d12-5e09f6bcbd39_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2006, N'Converter.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'96dff7d9-1002-4fac-a45f-6525db0a783b', N'96dff7d9-1002-4fac-a45f-6525db0a783b_CreateExam-Page-1.jpg', N'D:\File\Test\96dff7d9-1002-4fac-a45f-6525db0a783b_CreateExam-Page-1.jpg', N'image/jpeg', CAST(120441 AS Decimal(18, 0)), 2015, N'CreateExam-Page-1.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'6c4f4883-aa8c-4cc4-a501-685b2e5a3d22', N'6c4f4883-aa8c-4cc4-a501-685b2e5a3d22_CV_ChauAnhKiet.pdf', N'H:\File\Test\6c4f4883-aa8c-4cc4-a501-685b2e5a3d22_CV_ChauAnhKiet.pdf', N'application/pdf', CAST(124858 AS Decimal(18, 0)), 3093, N'CV_ChauAnhKiet.pdf')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'2b9f45b5-f4ad-4c2b-ab2b-73e96f23703a', N'2b9f45b5-f4ad-4c2b-ab2b-73e96f23703a_xembainop.jpg', N'H:\File\Test\2b9f45b5-f4ad-4c2b-ab2b-73e96f23703a_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 2018, N'xembainop.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'91cd41c6-d3bd-4e2e-9ed5-7a32a76dc969', N'91cd41c6-d3bd-4e2e-9ed5-7a32a76dc969_xembainop.jpg', N'H:\File\Thi kết thúc học phần - Lập trình web - Nhóm 1,2\Test\91cd41c6-d3bd-4e2e-9ed5-7a32a76dc969_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 3152, N'xembainop.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'f39803dd-256d-419c-9aaa-7faf81d4ff1f', N'f39803dd-256d-419c-9aaa-7faf81d4ff1f_database_SQLServer.sql', N'H:\File\Test\f39803dd-256d-419c-9aaa-7faf81d4ff1f_database_SQLServer.sql', N'application/octet-stream', CAST(55566 AS Decimal(18, 0)), 2017, N'database_SQLServer.sql')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'ef959ecf-d415-4b08-85a0-858bc53e1e6e', N'ef959ecf-d415-4b08-85a0-858bc53e1e6e_xoakythi.jpg', N'H:\File\Test\ef959ecf-d415-4b08-85a0-858bc53e1e6e_xoakythi.jpg', N'image/jpeg', CAST(33515 AS Decimal(18, 0)), 3189, N'xoakythi.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'a861d1eb-b3b4-41a4-8b95-8c8662a03d4d', N'a861d1eb-b3b4-41a4-8b95-8c8662a03d4d_xoakythi.jpg', N'H:\File\Test\a861d1eb-b3b4-41a4-8b95-8c8662a03d4d_xoakythi.jpg', N'image/jpeg', CAST(33515 AS Decimal(18, 0)), 3018, N'xoakythi.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'6bc6670c-2bb6-49ea-ac3d-954907e5a7b6', N'6bc6670c-2bb6-49ea-ac3d-954907e5a7b6_CreateExam-Page-1.jpg', N'D:\File\Test\6bc6670c-2bb6-49ea-ac3d-954907e5a7b6_CreateExam-Page-1.jpg', N'image/jpeg', CAST(120441 AS Decimal(18, 0)), 2015, N'CreateExam-Page-1.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'f9bf3cee-7a5c-411e-b042-9744ebc4c775', N'f9bf3cee-7a5c-411e-b042-9744ebc4c775_Converter.cs', N'D:\File\2004\Test\f9bf3cee-7a5c-411e-b042-9744ebc4c775_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2004, N'Converter.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'7770e34b-ceae-4114-9b88-a2706e6139f1', N'7770e34b-ceae-4114-9b88-a2706e6139f1_xoakythi.jpg', N'H:\File\Test\7770e34b-ceae-4114-9b88-a2706e6139f1_xoakythi.jpg', N'image/jpeg', CAST(33515 AS Decimal(18, 0)), 3186, N'xoakythi.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'736985bd-6b02-48d0-b4ea-b0e47c832812', N'736985bd-6b02-48d0-b4ea-b0e47c832812_WebSecurityModels.cs', N'D:\File\2012\Test\736985bd-6b02-48d0-b4ea-b0e47c832812_WebSecurityModels.cs', N'text/plain', CAST(6640 AS Decimal(18, 0)), 2012, N'WebSecurityModels.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'0fd11970-ed59-4749-91c3-b14328a95672', N'0fd11970-ed59-4749-91c3-b14328a95672_CV_ChauAnhKiet.pdf', N'H:\File\Test\0fd11970-ed59-4749-91c3-b14328a95672_CV_ChauAnhKiet.pdf', N'application/pdf', CAST(124858 AS Decimal(18, 0)), 3084, N'CV_ChauAnhKiet.pdf')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'978e248e-8fdc-4133-9ca0-b8290cd81d32', N'978e248e-8fdc-4133-9ca0-b8290cd81d32_CreateExam-Page-1.jpg', N'D:\File\Test\978e248e-8fdc-4133-9ca0-b8290cd81d32_CreateExam-Page-1.jpg', N'image/jpeg', CAST(120441 AS Decimal(18, 0)), 2015, N'CreateExam-Page-1.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'3ed2305a-e555-4414-86f2-b84917ebfed8', N'3ed2305a-e555-4414-86f2-b84917ebfed8_Converter.cs', N'D:\File\2008\Test\3ed2305a-e555-4414-86f2-b84917ebfed8_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2008, N'Converter.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'cb20a178-bf36-43a6-abb8-bc3c4f2d7fd8', N'cb20a178-bf36-43a6-abb8-bc3c4f2d7fd8_xembainop.jpg', N'H:\File\Test\cb20a178-bf36-43a6-abb8-bc3c4f2d7fd8_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 3187, N'xembainop.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'6052732b-499a-45ac-9270-c060a4c539dd', N'6052732b-499a-45ac-9270-c060a4c539dd_Converter.cs', N'D:\File\2010\Test\6052732b-499a-45ac-9270-c060a4c539dd_Converter.cs', N'text/plain', CAST(779 AS Decimal(18, 0)), 2010, N'Converter.cs')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'38043e1b-06ad-42f9-817b-de8b9cb4950b', N'38043e1b-06ad-42f9-817b-de8b9cb4950b_CreateExam-Page-1.jpg', N'D:\File\Test\38043e1b-06ad-42f9-817b-de8b9cb4950b_CreateExam-Page-1.jpg', N'image/jpeg', CAST(120441 AS Decimal(18, 0)), 2015, N'CreateExam-Page-1.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'6ee03f8b-d97e-47d3-ba68-e175ac9bb7c3', N'6ee03f8b-d97e-47d3-ba68-e175ac9bb7c3_CreateExam-Page-1.jpg', N'D:\File\Test\6ee03f8b-d97e-47d3-ba68-e175ac9bb7c3_CreateExam-Page-1.jpg', N'image/jpeg', CAST(120441 AS Decimal(18, 0)), 2013, N'CreateExam-Page-1.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'14f2e6f8-7f24-4040-90e4-e7efece3a797', N'14f2e6f8-7f24-4040-90e4-e7efece3a797_xembainop.jpg', N'H:\File\Test\14f2e6f8-7f24-4040-90e4-e7efece3a797_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 3188, N'xembainop.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'364660fd-e50b-4df7-b35a-e903155a3fe8', N'364660fd-e50b-4df7-b35a-e903155a3fe8_CV_ChauAnhKiet.pdf', N'H:\File\Test\364660fd-e50b-4df7-b35a-e903155a3fe8_CV_ChauAnhKiet.pdf', N'application/pdf', CAST(124858 AS Decimal(18, 0)), 3087, N'CV_ChauAnhKiet.pdf')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'6e78bb3d-5cc5-44bb-a18f-ee1cfa126302', N'6e78bb3d-5cc5-44bb-a18f-ee1cfa126302_bangdiem_1_3 .xls', N'H:\File\Test\6e78bb3d-5cc5-44bb-a18f-ee1cfa126302_bangdiem_1_3 .xls', N'application/vnd.ms-excel', CAST(47104 AS Decimal(18, 0)), 3020, N'bangdiem_1_3 .xls')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'80fb957b-ed99-4705-b340-ef275f7bfdcf', N'80fb957b-ed99-4705-b340-ef275f7bfdcf_CreateExam-Page-1.jpg', N'D:\File\Test\80fb957b-ed99-4705-b340-ef275f7bfdcf_CreateExam-Page-1.jpg', N'image/jpeg', CAST(120441 AS Decimal(18, 0)), 2014, N'CreateExam-Page-1.jpg')
INSERT [dbo].[TestFiles] ([FileId], [FileName], [FilePath], [MimeType], [Size], [TestId], [OriginalName]) VALUES (N'93d0038e-aceb-4c64-816a-f79893cbb32a', N'93d0038e-aceb-4c64-816a-f79893cbb32a_xembainop.jpg', N'H:\File\Test\93d0038e-aceb-4c64-816a-f79893cbb32a_xembainop.jpg', N'image/jpeg', CAST(39224 AS Decimal(18, 0)), 3188, N'xembainop.jpg')
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (1, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'- Một cộng một bằng mấy?', NULL, NULL, N'InProgress', 0, 0, 0, CAST(N'2024-03-27 00:00:00.000' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2004, N'Thi kết thúc học phần - Nhập môn - Nhóm 3', N'', CAST(N'2024-04-14 14:38:00.000' AS DateTime), NULL, N'InProgress', 0, 0, 0, CAST(N'2024-04-09 13:42:16.293' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2005, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-09 13:42:45.750' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2006, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-09 13:44:20.903' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2007, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-09 13:47:25.523' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2008, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-09 13:50:48.823' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2009, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-09 13:58:33.347' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2010, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-09 14:15:50.483' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2011, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-09 14:36:21.650' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2012, N'Kiểm tra - Kỹ thuật lập trình', N'- Một cộng một bằng mấy?', CAST(N'2024-04-14 14:38:00.000' AS DateTime), CAST(N'2024-04-25 14:38:00.000' AS DateTime), N'InProgress', 0, 0, 0, CAST(N'2024-04-09 14:38:43.713' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2013, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-26 11:29:52.413' AS DateTime), CAST(N'2024-04-26 11:29:52.413' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2014, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-26 11:31:50.277' AS DateTime), CAST(N'2024-04-26 11:31:50.280' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2015, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-26 11:43:09.970' AS DateTime), CAST(N'2024-04-26 11:43:09.970' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2016, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-26 11:49:48.007' AS DateTime), CAST(N'2024-04-26 11:49:48.010' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2017, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-27 07:59:19.433' AS DateTime), CAST(N'2024-04-27 07:59:19.433' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (2018, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-04-29 08:24:24.520' AS DateTime), CAST(N'2024-04-29 08:24:24.520' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3018, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 08:40:59.763' AS DateTime), CAST(N'2024-05-02 08:40:59.763' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3020, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 08:49:04.297' AS DateTime), CAST(N'2024-05-02 08:49:04.297' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3084, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:13:15.137' AS DateTime), CAST(N'2024-05-02 17:13:15.137' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3087, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:14:36.527' AS DateTime), CAST(N'2024-05-02 17:14:36.527' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3089, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:18:29.907' AS DateTime), CAST(N'2024-05-02 17:18:29.907' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3091, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:18:55.687' AS DateTime), CAST(N'2024-05-02 17:18:55.687' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3093, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:21:42.667' AS DateTime), CAST(N'2024-05-02 17:21:42.667' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3095, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:22:21.157' AS DateTime), CAST(N'2024-05-02 17:22:21.157' AS DateTime), N'Exam', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3099, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:31:28.737' AS DateTime), CAST(N'2024-05-02 17:31:28.737' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3100, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:35:31.000' AS DateTime), CAST(N'2024-05-02 17:35:31.000' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3101, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:36:50.160' AS DateTime), CAST(N'2024-05-02 17:36:50.160' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3102, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:37:04.427' AS DateTime), CAST(N'2024-05-02 17:37:04.430' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3103, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:37:31.617' AS DateTime), CAST(N'2024-05-02 17:37:31.620' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3104, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:39:00.203' AS DateTime), CAST(N'2024-05-02 17:39:00.207' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3105, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:39:24.133' AS DateTime), CAST(N'2024-05-02 17:39:24.133' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3106, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:39:49.283' AS DateTime), CAST(N'2024-05-02 17:39:49.287' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3107, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:41:06.070' AS DateTime), CAST(N'2024-05-02 17:41:06.070' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3108, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:41:25.527' AS DateTime), CAST(N'2024-05-02 17:41:25.527' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3109, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:43:59.850' AS DateTime), CAST(N'2024-05-02 17:43:59.850' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3110, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:44:16.143' AS DateTime), CAST(N'2024-05-02 17:44:16.143' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3111, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:45:33.733' AS DateTime), CAST(N'2024-05-02 17:45:33.733' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3112, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:46:20.563' AS DateTime), CAST(N'2024-05-02 17:46:20.567' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3113, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:47:18.453' AS DateTime), CAST(N'2024-05-02 17:47:18.453' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3114, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:49:46.517' AS DateTime), CAST(N'2024-05-02 17:49:46.517' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3115, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:50:29.130' AS DateTime), CAST(N'2024-05-02 17:50:29.133' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3116, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-02 17:51:06.893' AS DateTime), CAST(N'2024-05-02 17:51:06.893' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3117, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 08:36:41.210' AS DateTime), CAST(N'2024-05-04 08:36:41.210' AS DateTime), N'Quiz', N'DHT0220')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3118, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 13:48:02.103' AS DateTime), CAST(N'2024-05-04 13:48:02.107' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3119, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 13:50:20.217' AS DateTime), CAST(N'2024-05-04 13:50:20.223' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3120, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 15:27:27.220' AS DateTime), CAST(N'2024-05-04 15:29:37.673' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3121, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 15:29:44.740' AS DateTime), CAST(N'2024-05-04 15:30:32.373' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3122, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 15:45:23.773' AS DateTime), CAST(N'2024-05-04 15:45:23.773' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3123, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 15:47:21.370' AS DateTime), CAST(N'2024-05-04 15:47:29.727' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3124, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 15:48:14.540' AS DateTime), CAST(N'2024-05-04 15:48:14.543' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3125, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 15:49:07.187' AS DateTime), CAST(N'2024-05-04 15:49:07.190' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3126, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 15:49:36.317' AS DateTime), CAST(N'2024-05-04 15:49:36.317' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3127, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 15:50:21.337' AS DateTime), CAST(N'2024-05-04 15:50:21.337' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3128, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 16:01:02.280' AS DateTime), CAST(N'2024-05-04 16:01:12.340' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3129, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 16:40:21.627' AS DateTime), CAST(N'2024-05-04 16:40:21.627' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3130, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 16:48:28.203' AS DateTime), CAST(N'2024-05-04 16:48:28.203' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3131, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 16:49:12.673' AS DateTime), CAST(N'2024-05-04 16:50:45.993' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3132, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 16:51:40.047' AS DateTime), CAST(N'2024-05-04 16:51:40.050' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3133, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 16:53:01.890' AS DateTime), CAST(N'2024-05-04 17:09:33.967' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3134, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 17:12:41.183' AS DateTime), CAST(N'2024-05-04 17:12:41.187' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3135, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 19:21:18.930' AS DateTime), CAST(N'2024-05-04 19:21:18.930' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3136, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 19:34:11.923' AS DateTime), CAST(N'2024-05-04 19:34:11.923' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3137, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 19:36:27.233' AS DateTime), CAST(N'2024-05-04 19:36:27.237' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3138, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 19:44:21.587' AS DateTime), CAST(N'2024-05-04 19:44:21.590' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3139, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 19:54:01.677' AS DateTime), CAST(N'2024-05-04 19:54:01.680' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3140, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 20:35:25.253' AS DateTime), CAST(N'2024-05-04 20:35:25.257' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3141, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 20:37:33.260' AS DateTime), CAST(N'2024-05-04 20:37:33.263' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3142, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 20:40:09.107' AS DateTime), CAST(N'2024-05-04 20:40:09.110' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3143, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 20:45:38.290' AS DateTime), CAST(N'2024-05-04 20:45:38.290' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3144, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 20:48:03.310' AS DateTime), CAST(N'2024-05-04 20:48:03.310' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3145, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 20:49:45.693' AS DateTime), CAST(N'2024-05-04 20:49:45.697' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3146, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:01:02.883' AS DateTime), CAST(N'2024-05-04 21:01:02.883' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3147, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:01:49.470' AS DateTime), CAST(N'2024-05-04 21:02:38.440' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3148, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:06:02.373' AS DateTime), CAST(N'2024-05-04 21:06:02.377' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3149, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:07:56.630' AS DateTime), CAST(N'2024-05-04 21:07:56.630' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3150, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:12:23.480' AS DateTime), CAST(N'2024-05-04 21:12:23.480' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3151, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:13:44.823' AS DateTime), CAST(N'2024-05-04 21:14:00.500' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3152, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:14:01.487' AS DateTime), CAST(N'2024-05-04 21:18:13.213' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3153, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:21:58.653' AS DateTime), CAST(N'2024-05-04 21:21:58.653' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3154, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:25:56.703' AS DateTime), CAST(N'2024-05-04 21:25:56.703' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3155, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:29:30.500' AS DateTime), CAST(N'2024-05-04 21:29:30.500' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3156, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-04 21:38:11.557' AS DateTime), CAST(N'2024-05-04 21:38:11.560' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3157, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:00:40.100' AS DateTime), CAST(N'2024-05-05 08:00:40.100' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3158, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:01:15.640' AS DateTime), CAST(N'2024-05-05 08:01:15.640' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3159, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:03:20.123' AS DateTime), CAST(N'2024-05-05 08:03:20.123' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3160, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:03:46.770' AS DateTime), CAST(N'2024-05-05 08:03:46.770' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3161, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:07:39.210' AS DateTime), CAST(N'2024-05-05 08:07:39.217' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3162, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:07:57.923' AS DateTime), CAST(N'2024-05-05 08:07:57.923' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3163, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:09:12.267' AS DateTime), CAST(N'2024-05-05 08:09:12.267' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3164, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:12:13.120' AS DateTime), CAST(N'2024-05-05 08:12:13.120' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3165, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:15:58.027' AS DateTime), CAST(N'2024-05-05 08:20:25.083' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3166, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:20:25.653' AS DateTime), CAST(N'2024-05-05 08:20:26.040' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3167, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:22:03.560' AS DateTime), CAST(N'2024-05-05 08:22:03.560' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3168, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:25:29.640' AS DateTime), CAST(N'2024-05-05 08:25:29.640' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3169, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:30:09.840' AS DateTime), CAST(N'2024-05-05 08:30:09.840' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3170, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:31:04.897' AS DateTime), CAST(N'2024-05-05 08:31:04.897' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3171, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:32:08.573' AS DateTime), CAST(N'2024-05-05 08:32:08.573' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3172, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:32:32.823' AS DateTime), CAST(N'2024-05-05 08:32:32.823' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3173, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:33:56.783' AS DateTime), CAST(N'2024-05-05 08:33:56.783' AS DateTime), N'Quiz', N'DHT0220   ')
GO
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3174, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:34:16.393' AS DateTime), CAST(N'2024-05-05 08:34:16.397' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3175, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:35:05.217' AS DateTime), CAST(N'2024-05-05 08:35:05.217' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3176, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:35:14.527' AS DateTime), CAST(N'2024-05-05 08:35:14.527' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3177, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:36:28.123' AS DateTime), CAST(N'2024-05-05 08:36:28.127' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3178, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:36:37.953' AS DateTime), CAST(N'2024-05-05 08:36:37.957' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3179, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:36:56.637' AS DateTime), CAST(N'2024-05-05 08:36:56.640' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3180, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:41:07.300' AS DateTime), CAST(N'2024-05-05 08:41:07.300' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3181, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:42:30.647' AS DateTime), CAST(N'2024-05-05 08:42:30.647' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3182, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:47:50.640' AS DateTime), CAST(N'2024-05-05 08:47:50.643' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3183, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-05 08:48:56.647' AS DateTime), CAST(N'2024-05-05 08:48:56.650' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3184, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-07 09:35:47.267' AS DateTime), CAST(N'2024-05-07 09:35:47.270' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3185, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-07 09:37:49.413' AS DateTime), CAST(N'2024-05-07 09:37:49.417' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3186, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-07 09:38:36.807' AS DateTime), CAST(N'2024-05-07 09:38:36.807' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3187, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-07 10:39:08.740' AS DateTime), CAST(N'2024-05-07 10:39:08.740' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3188, N'', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-07 10:41:23.327' AS DateTime), CAST(N'2024-05-07 10:41:23.330' AS DateTime), N'Quiz', N'DHT0220   ')
INSERT [dbo].[Tests] ([TestId], [Title], [Instruction], [StartTime], [EndTime], [Status], [IsCheckIP], [IsConductedAtSchool], [CanSubmitLate], [CreatedTime], [LastUpdateTime], [TestType], [TeacherId]) VALUES (3189, N'Thi kết thúc học phần - Lập trình web - Nhóm 1,2', N'', NULL, NULL, N'Creating', 0, 0, 0, CAST(N'2024-05-07 10:55:08.440' AS DateTime), CAST(N'2024-05-07 10:55:13.893' AS DateTime), N'Quiz', N'DHT0220')
SET IDENTITY_INSERT [dbo].[Tests] OFF
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
ALTER TABLE [dbo].[Submissions]  WITH CHECK ADD  CONSTRAINT [FK_Submissions_Students1] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Submissions] CHECK CONSTRAINT [FK_Submissions_Students1]
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
/****** Object:  StoredProcedure [dbo].[AddComment]    Script Date: 07/05/2024 10:59:32 SA ******/
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
    SELECT SCOPE_IDENTITY() AS CommentId;
END;



GO
/****** Object:  StoredProcedure [dbo].[AddStudent]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddSubmission]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSubmission]
    @StudentId VARCHAR(50),
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
/****** Object:  StoredProcedure [dbo].[AddSubmissionFile]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddTeacher]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddTest]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[AddTestFile]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[CountSubmissions]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[CountTestsOfStudent]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountTestsOfStudent]
    @StudentId VARCHAR(50),
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
/****** Object:  StoredProcedure [dbo].[DeleteComment]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmission]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSubmissionFile]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteTest]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteTestFile]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetCommentById]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetCommentsBySubmissionId]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionById]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestId]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionByTestIdAndStudentId]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionFileById]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissionFilesBySubmissionId]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetSubmissions]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubmissions]
    @Page INT,
    @PageSize INT,
    @TestId INT,
    @SearchValue NVARCHAR(MAX),
    @SubmissionStatus NVARCHAR(20)
AS
BEGIN
WITH cte AS
    (
        SELECT s.*, st.FirstName, st.LastName, st.Email, ROW_NUMBER() OVER (ORDER BY StartTime) AS RowNumber
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
/****** Object:  StoredProcedure [dbo].[GetTeacherById]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestById]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestFileById]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestFilesByTestId]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[GetTestsOfStudent]    Script Date: 07/05/2024 10:59:32 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTestsOfStudent]
    @Page INT,
    @PageSize INT,
    @StudentId VARCHAR(50),
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
/****** Object:  StoredProcedure [dbo].[GetTestsOfTeacher]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[IsTestUsed]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateComment]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateSubmission]    Script Date: 07/05/2024 10:59:32 SA ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateTest]    Script Date: 07/05/2024 10:59:32 SA ******/
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
    @CreatedTime DATETIME,
    @LastUpdateTime DATETIME,
    @TestType NVARCHAR(20),
    @TeacherId VARCHAR(50)
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
		CanSubmitLate = @CanSubmitLate,
        TestType = @TestType,
        TeacherId = @TeacherId
    WHERE TestId = @TestId;
	    COMMIT;
END;



GO
