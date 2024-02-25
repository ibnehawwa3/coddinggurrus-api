USE [CoddingGurrusDb]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 25/02/2024 7:39:56 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Url] [nvarchar](200) NULL,
	[ParentId] [bigint] NULL,
	[MenuOrder] [tinyint] NULL,
	[MenuImage] [nvarchar](200) NULL,
	[Status] [int] NULL,
	[ApiPath] [nvarchar](250) NULL,
	[Caption] [nvarchar](250) NULL,
	[IsShow] [bit] NULL,
	[IsSupperUser] [bit] NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [bigint] NULL,
	[ModifiedOn] [datetime2](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[Archived] [bit] NULL,
	[ArchivedOn] [datetime2](7) NULL,
	[ArchivedBy] [bigint] NULL,
	[IsSupperUserMenu] [bit] NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMenuPermissions]    Script Date: 25/02/2024 7:39:56 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenuPermissions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[MenuId] [bigint] NOT NULL,
	[Add] [bit] NOT NULL,
	[Update] [bit] NOT NULL,
	[Delete] [bit] NOT NULL,
	[Access] [bit] NOT NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [bigint] NULL,
	[ModifiedOn] [datetime2](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[Archived] [bit] NULL,
	[ArchivedOn] [datetime2](7) NULL,
	[ArchivedBy] [bigint] NULL,
 CONSTRAINT [PK_RoleMenuPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([Id], [Name], [Url], [ParentId], [MenuOrder], [MenuImage], [Status], [ApiPath], [Caption], [IsShow], [IsSupperUser], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy], [IsSupperUserMenu]) VALUES (3, N'User', N'user/list', NULL, 0, N'string', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[Menus] ([Id], [Name], [Url], [ParentId], [MenuOrder], [MenuImage], [Status], [ApiPath], [Caption], [IsShow], [IsSupperUser], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy], [IsSupperUserMenu]) VALUES (4, N'Role', N'role/list', 4, 1, N'newicon', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[Menus] ([Id], [Name], [Url], [ParentId], [MenuOrder], [MenuImage], [Status], [ApiPath], [Caption], [IsShow], [IsSupperUser], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy], [IsSupperUserMenu]) VALUES (6, N'Supplier', N'ferreee/ererre', NULL, 8, N'newicon', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Menus] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleMenuPermissions] ON 

INSERT [dbo].[RoleMenuPermissions] ([Id], [RoleId], [MenuId], [Add], [Update], [Delete], [Access], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy]) VALUES (8, 1, 3, 1, 1, 1, 1, NULL, NULL, NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[RoleMenuPermissions] ([Id], [RoleId], [MenuId], [Add], [Update], [Delete], [Access], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy]) VALUES (9, 1, 4, 0, 1, 1, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[RoleMenuPermissions] ([Id], [RoleId], [MenuId], [Add], [Update], [Delete], [Access], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy]) VALUES (10, 1, 5, 0, 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[RoleMenuPermissions] ([Id], [RoleId], [MenuId], [Add], [Update], [Delete], [Access], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy]) VALUES (11, 2, 3, 0, 1, 0, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[RoleMenuPermissions] ([Id], [RoleId], [MenuId], [Add], [Update], [Delete], [Access], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy]) VALUES (12, 2, 4, 0, 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[RoleMenuPermissions] ([Id], [RoleId], [MenuId], [Add], [Update], [Delete], [Access], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [Archived], [ArchivedOn], [ArchivedBy]) VALUES (13, 2, 5, 0, 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RoleMenuPermissions] OFF
GO
/****** Object:  Index [RM_RoleMenuPermissions]    Script Date: 25/02/2024 7:39:56 pm ******/
ALTER TABLE [dbo].[RoleMenuPermissions] ADD  CONSTRAINT [RM_RoleMenuPermissions] UNIQUE NONCLUSTERED 
(
	[RoleId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Menus] ADD  CONSTRAINT [DF__Menus__Archived__27F8EE98]  DEFAULT ((0)) FOR [Archived]
GO
ALTER TABLE [dbo].[RoleMenuPermissions] ADD  CONSTRAINT [DF__RoleMenuP__Archi__1AF3F935]  DEFAULT ((0)) FOR [Archived]
GO
