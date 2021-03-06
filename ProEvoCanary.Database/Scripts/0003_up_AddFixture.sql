IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_AddFixture')
BEGIN
    exec ('create procedure up_AddFixture as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_AddFixture]
@TournamentId int,
@HomeTeamId int,
@AwayTeamId int,
@Round int

AS
	INSERT INTO Results
	(
		TournamentId,
		HomeUserId,
		AwayUserId,
		[Round]
	)
	VALUES
	(
		@TournamentId,
		@HomeTeamId,
		@AwayTeamId,
		@Round
	)
	/* SET NOCOUNT ON */
	RETURN;