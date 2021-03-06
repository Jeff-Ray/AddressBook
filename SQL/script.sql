USE [AddressBook]
GO
/****** Object:  Table [dbo].[AddressInfo]    Script Date: 2020/12/22 14:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Sex] [nvarchar](10) NULL,
	[Phone] [nvarchar](30) NULL,
	[Address] [nvarchar](50) NULL,
	[Memo] [nvarchar](30) NULL,
 CONSTRAINT [PK_AddressInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginUser]    Script Date: 2020/12/22 14:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginUser](
	[loginId] [int] IDENTITY(1,1) NOT NULL,
	[loginName] [nvarchar](20) NULL,
	[loginPassword] [nvarchar](30) NULL,
	[phone] [nvarchar](15) NULL,
 CONSTRAINT [PK_LoginUser] PRIMARY KEY CLUSTERED 
(
	[loginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AddressInfo] ON 

INSERT [dbo].[AddressInfo] ([Id], [Name], [Sex], [Phone], [Address], [Memo]) VALUES (1, N'雷建峰', N'男', N'15399960933', N'湖南省长沙市岳麓区', N'')
INSERT [dbo].[AddressInfo] ([Id], [Name], [Sex], [Phone], [Address], [Memo]) VALUES (2, N'雷明', N'男', N'13314791023', N'广东省广州市白云区', N'')
INSERT [dbo].[AddressInfo] ([Id], [Name], [Sex], [Phone], [Address], [Memo]) VALUES (3, N'小宇', N'男', N'18813340126', N'湖南省长沙市开福区', N'')
INSERT [dbo].[AddressInfo] ([Id], [Name], [Sex], [Phone], [Address], [Memo]) VALUES (4, N'小美', N'女', N'13307470312', N'广东省佛山市顺德区', N'')
INSERT [dbo].[AddressInfo] ([Id], [Name], [Sex], [Phone], [Address], [Memo]) VALUES (5, N'李明一', N'男', N'15007311359', N'广东省佛山市南海区', N'')
INSERT [dbo].[AddressInfo] ([Id], [Name], [Sex], [Phone], [Address], [Memo]) VALUES (6, N'李婷', N'女', N'18814722014', N'湖南省长沙市天心区', N'')
INSERT [dbo].[AddressInfo] ([Id], [Name], [Sex], [Phone], [Address], [Memo]) VALUES (7, N'小莉', N'女', N'18820145210', N'湖南省郴州市苏仙区', N'')
SET IDENTITY_INSERT [dbo].[AddressInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginUser] ON 

INSERT [dbo].[LoginUser] ([loginId], [loginName], [loginPassword], [phone]) VALUES (1, N'admin', N'123456', N'15399960933')
INSERT [dbo].[LoginUser] ([loginId], [loginName], [loginPassword], [phone]) VALUES (3, N'Ray', N'123456', N'15399960933')
INSERT [dbo].[LoginUser] ([loginId], [loginName], [loginPassword], [phone]) VALUES (4, N'xiaoming', N'123', N'18812345678')
INSERT [dbo].[LoginUser] ([loginId], [loginName], [loginPassword], [phone]) VALUES (9, N'Jeff', N'123', N'13312345678')
SET IDENTITY_INSERT [dbo].[LoginUser] OFF
GO
