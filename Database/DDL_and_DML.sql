USE [DB_Challenge_Imusica]
GO
/****** Object:  Table [dbo].[Dependent]    Script Date: 28/01/2018 22:14:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dependent](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Employee] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 28/01/2018 22:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
	[Genre] [bit] NOT NULL,
	[Birth] [datetime] NULL,
	[Role] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 28/01/2018 22:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Dependent] ON 

INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (29, N'Yuri Murilo', N'50b2bb14-1e53-46a5-bfed-9122993bca57')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (30, N'Lara ', N'50b2bb14-1e53-46a5-bfed-9122993bca57')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (32, N'Paula', N'b69559b2-1bac-490d-abb8-87b24a34a55b')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (35, N'Tome', N'761ca3fd-79a0-4173-b2d2-db0c256a0754')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (36, N'Antonio', N'761ca3fd-79a0-4173-b2d2-db0c256a0754')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (37, N'Chica', N'82525786-ff41-47cf-868e-4856bbae7184')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (38, N'Marilia', N'196cd244-42d2-4e24-b4a1-b70388756e6f')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (39, N'Mauricio', N'196cd244-42d2-4e24-b4a1-b70388756e6f')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (40, N'Maria', N'1b91fd3a-6bff-4375-ab59-fe0e615f38e1')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (41, N'Jose', N'1b91fd3a-6bff-4375-ab59-fe0e615f38e1')
INSERT [dbo].[Dependent] ([Id], [Name], [Employee]) VALUES (42, N'Vanessa', N'abd3697f-acab-4e5b-baf2-b29a013b500f')
SET IDENTITY_INSERT [dbo].[Dependent] OFF
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'7a890e51-e92c-44b1-bb31-0554584ab1b3', N'Gustavo', N'gustavo@email.com', 1, CAST(N'1990-05-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'9f4d586f-ed48-40e7-9f3e-147edafe4135', N'Joel', N'joel@email.com', 1, CAST(N'1960-12-26 00:00:00.000' AS DateTime), 4)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'd135205c-8b5f-40c5-9a46-1dc26b37f6d9', N'Eloy', N'eloy@email.com', 0, CAST(N'1989-12-12 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'82525786-ff41-47cf-868e-4856bbae7184', N'Nadal', N'nadal@email.com', 1, CAST(N'1987-06-02 00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'f395a296-3fc7-466a-9a6e-51e49100b35d', N'Novak', N'novak@email.com', 1, CAST(N'1987-06-05 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'00b9a6aa-ec87-4bf5-b042-52173fd213a4', N'Elie', N'elie@email.com', 0, CAST(N'1986-08-16 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'6b7e1f76-037c-4793-91d5-5e99aa2f2e27', N'Thales', NULL, 1, NULL, 3)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'7b3c66f6-a81d-472c-97a0-650e8b20fdb4', N'Federer', N'federer@email.com', 1, NULL, 3)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'9410bd36-c6ba-443f-913f-818e14c3c6e2', N'Hagnar', N'hagnar@email.com', 1, CAST(N'1985-11-06 00:00:00.000' AS DateTime), 5)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'98b32e1c-1870-43d7-98df-8378933b8064', N'Fabio ', N'fabio@email.com', 1, NULL, 2)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'b69559b2-1bac-490d-abb8-87b24a34a55b', N'Jaqueline', N'jaqueline@email.com', 0, CAST(N'1974-04-07 00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'50b2bb14-1e53-46a5-bfed-9122993bca57', N'Fabio Murilo', N'fmurilo@email.com', 1, CAST(N'1988-07-21 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'64d97411-b28c-4a25-979f-931add7f9bee', N'Rafael', N'rafael@email.com', 1, NULL, 3)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'dc830456-4319-4fd2-8ba6-93ae79e3ba70', N'Djokovic', N'djokovic@email.com', 1, CAST(N'1975-09-04 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'e69d0d2f-0b29-40c0-b6ee-9de9fc719610', N'Karla', N'karla@email.com', 1, NULL, 2)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'356fd8c0-85d1-4938-9815-a437265c9da6', N'Pedro', NULL, 1, CAST(N'1985-12-04 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'abd3697f-acab-4e5b-baf2-b29a013b500f', N'Aline', N'aline@email.com', 0, CAST(N'1988-12-02 00:00:00.000' AS DateTime), 5)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'196cd244-42d2-4e24-b4a1-b70388756e6f', N'Bruna', N'bruna@email.com', 0, NULL, 5)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'607da14f-381b-4763-9026-bebf11de27f2', N'Keli', N'keli@email.com', 0, CAST(N'1999-12-05 00:00:00.000' AS DateTime), 5)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'41768ad5-5a1a-41bf-9083-d189e61895d8', N'Murilo', N'murilo@email.com', 1, NULL, 4)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'761ca3fd-79a0-4173-b2d2-db0c256a0754', N'Roger', N'roger@email.com', 1, CAST(N'1964-05-04 00:00:00.000' AS DateTime), 4)
INSERT [dbo].[Employee] ([Id], [Name], [Email], [Genre], [Birth], [Role]) VALUES (N'1b91fd3a-6bff-4375-ab59-fe0e615f38e1', N'Larissa', N'larissa@email.com', 0, NULL, 2)
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Analista de negócio')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'Analista de sistemas')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (3, N'Gerente de projetos')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (4, N'Diretor de TI')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (5, N'Recursos Humanos')
SET IDENTITY_INSERT [dbo].[Role] OFF
ALTER TABLE [dbo].[Dependent]  WITH CHECK ADD  CONSTRAINT [FKDependentEmployee] FOREIGN KEY([Employee])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Dependent] CHECK CONSTRAINT [FKDependentEmployee]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FKEmployeeRole] FOREIGN KEY([Role])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FKEmployeeRole]
GO
