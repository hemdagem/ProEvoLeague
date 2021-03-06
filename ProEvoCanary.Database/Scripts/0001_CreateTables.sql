IF NOT EXISTS(SELECT 1 FROM sys.tables  WHERE Name = 'Users')
BEGIN
    CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Forename] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[UserType] [int] NOT NULL DEFAULT ((2)),
	[Hash][varchar](300) NOT NULL
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END

IF NOT EXISTS(SELECT 1 FROM sys.tables  WHERE Name = 'Tournament')
BEGIN
CREATE TABLE [dbo].[Tournament](
	[TournamentId] [int] IDENTITY(1,1) NOT NULL,
	[TournamentName] [varchar](100) NOT NULL,
	[Date] [date] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[Completed] [bit] NOT NULL,
	[TournamentType] [int] NOT NULL,
 CONSTRAINT [PK_tblTournament] PRIMARY KEY CLUSTERED 
(
	[TournamentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Tournament]  WITH CHECK ADD  CONSTRAINT [FK_Tournament_Users] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Users] ([UserId])

ALTER TABLE [dbo].[Tournament] CHECK CONSTRAINT [FK_Tournament_Users]

END

IF NOT EXISTS(SELECT 1 FROM sys.tables  WHERE Name = 'Results')
BEGIN
CREATE TABLE [dbo].[Results](
	[ResultId] [int] IDENTITY(1,1) NOT NULL,
	[TournamentId] [int] NOT NULL,
	[HomeScore] [int] NOT NULL,
	[AwayScore] [int] NOT NULL,
	[Round] [int] NULL,
	[HomeUserId] [int] NULL,
	[AwayUserId] [int] NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Tournament] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournament] ([TournamentId])

ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Tournament]
END

IF NOT EXISTS(SELECT 1 FROM sys.tables  WHERE Name = 'TournamentUsers')
BEGIN
CREATE TABLE [dbo].[TournamentUsers](
	[UserId] [int] NOT NULL,
	[TournamentId] [int] NOT NULL,
	[PreviousPosition] [int] NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[TournamentUsers]  WITH CHECK ADD  CONSTRAINT [FK_TournamentUsers_Tournament] FOREIGN KEY([TournamentId])
REFERENCES [dbo].[Tournament] ([TournamentId])

ALTER TABLE [dbo].[TournamentUsers] CHECK CONSTRAINT [FK_TournamentUsers_Tournament]


ALTER TABLE [dbo].[TournamentUsers]  WITH CHECK ADD  CONSTRAINT [FK_TournamentUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])

ALTER TABLE [dbo].[TournamentUsers] CHECK CONSTRAINT [FK_TournamentUsers_Users]

END