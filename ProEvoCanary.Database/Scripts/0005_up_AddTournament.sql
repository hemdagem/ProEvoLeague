IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_AddTournament')
BEGIN
    exec ('create procedure up_AddTournament as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_AddTournament]

	@TournamentType int = 1,
	@TournamentName varchar(100),
	@Date date,
	@OwnerId int

AS
BEGIN
	INSERT INTO Tournament 
	(
	TournamentType,
	TournamentName,
	[Date],
	OwnerId,
	Completed
	)
	VALUES
	(
	@TournamentType,
	@TournamentName,
	@Date,
	@OwnerId,
	0
	)
	
	SELECT @@IDENTITY

END
