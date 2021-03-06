IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_GetStandings')
BEGIN
    exec ('create procedure up_GetStandings as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_GetStandings]
	@TournamentId int
AS

	
	SELECT U.Forename + ' ' + U.Surname AS [Name],U.UserId AS UserID, v.Played, v.Won, v.Draw, v.Lost, ISNULL(v.[For],0) AS [For],
	ISNULL(v.Against,0) AS Against, ISNULL(v.GoalDifference,0) AS GoalDifference,
			v.TotalPoints AS Points, ISNULL(v.PreviousPosition,0) AS PreviousPosition FROM Users U INNER JOIN vw_Standings v ON v.Id = U.UserId	
			WHERE v.TournamentID=@TournamentId
	ORDER BY v.TournamentID, v.TotalPoints DESC, v.GoalDifference DESC
	RETURN