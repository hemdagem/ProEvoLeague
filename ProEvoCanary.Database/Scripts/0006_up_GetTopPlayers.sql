IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_GetTopPlayers')
BEGIN
    exec ('create procedure up_GetTopPlayers as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_GetTopPlayers]

@RowsPerPage INT = 10,
@PageNumber INT =1

AS

DECLARE @Teams float = (SELECT COUNT(*) FROM Users)
DECLARE @Matches float = (SELECT COUNT(*) FROM Results WHERE HomeScore !=-1 AND AwayScore !=-1)
DECLARE @MatchesPerUser float = @Matches  /NULLIF(@Teams,0)

SELECT  * FROM (
             SELECT ROW_NUMBER() OVER(ORDER BY CONVERT(decimal(12, 2), CAST(SUM(ISNULL(v.TotalPoints,0)) AS FLOAT) / CAST(NULLIF(SUM(ISNULL(v.Played,0)),0) AS FLOAT)) DESC, CONVERT(decimal(12, 2), CAST(SUM(ISNULL(v.[For],0)) AS FLOAT) / CAST(NULLIF(SUM(ISNULL(v.Played,0)),0) AS FLOAT)) DESC) AS NUMBER,
					SUM(v.Played) AS MatchesPlayed,
					CONVERT(decimal(12, 2), CAST(SUM(ISNULL(v.TotalPoints,0)) AS FLOAT) / CAST(NULLIF(SUM(ISNULL(v.Played,0)),0) AS FLOAT)) AS PointsPerGame,
					U.Forename + ' ' + U.Surname AS Name, U.UserId,
					CONVERT(decimal(12, 2), CAST(SUM(ISNULL(v.[For],0)) AS FLOAT) / CAST(NULLIF(SUM(ISNULL(v.Played,0)),0) AS FLOAT)) AS GoalsPerGame
FROM				Users U INNER JOIN
                    vw_Standings v on V.Id = U.UserId

GROUP BY U.UserId,U.Forename, U.Surname) AS Results 


WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RowsPerPage + 1) AND (@PageNumber * @RowsPerPage)
GROUP BY Results.NUMBER,Results.MatchesPlayed,Results.PointsPerGame,Results.Name,Results.UserId,Results.GoalsPerGame
HAVING SUM(Results.MatchesPlayed) >= @MatchesPerUser
ORDER BY PointsPerGame DESC, GoalsPerGame DESC


	/* SET NOCOUNT ON */
	RETURN