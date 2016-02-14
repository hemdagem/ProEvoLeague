IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_AddResult')
BEGIN
    exec ('create procedure up_AddResult as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_AddResult]
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

RETURN;