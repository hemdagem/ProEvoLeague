IF NOT EXISTS(SELECT 1 FROM sys.views  WHERE Name = 'vw_Standings')
BEGIN
    exec ('create view vw_Standings as select 1 as column1')
END

GO
ALTER VIEW [dbo].[vw_Standings]
AS
SELECT     TOP (100) PERCENT U_1.Id, U_1.TournamentId, SUM(U_1.HomePlayed) + SUM(U_1.AwayPlayed) AS Played, SUM(U_1.HomePlayed) AS HomePlayed, SUM(U_1.HomeWon) AS HWon, 
                      SUM(U_1.HomeDraw) AS HDraw, SUM(U_1.HomeLost) AS HLost, SUM(U_1.HomeGoalsFor) AS HFor, SUM(U_1.HomeGoalsAgainst) AS HAgainst, SUM(U_1.AwayPlayed) AS AwayPlayed, 
                      SUM(U_1.AwayWon) AS AWon, SUM(U_1.AwayDraw) AS ADraw, SUM(U_1.AwayLost) AS ALost, SUM(U_1.AwayGoalsFor) AS AFor, SUM(U_1.AwayGoalsAgainst) AS AAgainst, 
                      ISNULL(SUM(U_1.HomeGoalsFor + U_1.AwayGoalsFor) - SUM(U_1.HomeGoalsAgainst + U_1.AwayGoalsAgainst), 0) AS GoalDifference, SUM(U_1.HomeWon + U_1.AwayWon) AS Won, 
                      SUM(U_1.HomeDraw + U_1.AwayDraw) AS Draw, SUM(U_1.HomeLost + U_1.AwayLost) AS Lost, ISNULL(SUM(U_1.HomeGoalsFor + U_1.AwayGoalsFor), 0) AS [For], 
                      ISNULL(SUM(U_1.HomeGoalsAgainst + U_1.AwayGoalsAgainst), 0) AS Against, SUM(U_1.HomeWon + U_1.AwayWon) * 3 + SUM(U_1.AwayDraw + U_1.HomeDraw) AS TotalPoints, 
                      U_1.PreviousPosition, T.TournamentName, T.Date, T.Completed, T.OwnerId, uOwner.Forename + ' ' + uOwner.Surname AS Name
FROM         (SELECT     R.HomeUserId AS Id, CASE WHEN R.HomeScore = R.AwayScore THEN 1 ELSE 0 END AS HomeDraw, 0 AS AwayDraw, R.TournamentId, CASE ISNULL(R.HomeScore, - 1) 
                                              WHEN - 1 THEN 0 ELSE 1 END AS HomePlayed, CASE WHEN R.HomeScore > R.AwayScore THEN 1 ELSE 0 END AS HomeWon, 
                                              CASE WHEN R.HomeScore < R.AwayScore THEN 1 ELSE 0 END AS HomeLost, R.HomeScore AS HomeGoalsFor, R.AwayScore AS HomeGoalsAgainst, 0 AS AwayPlayed, 0 AS AwayWon, 
                                              0 AS AwayLost, 0 AS AwayGoalsFor, TU.PreviousPosition, 0 AS AwayGoalsAgainst
                       FROM          dbo.Results AS R INNER JOIN
                                              dbo.Users AS U ON U.UserId = R.HomeUserId INNER JOIN
                                              dbo.TournamentUsers AS TU ON TU.UserId = R.HomeUserId AND TU.TournamentId = R.TournamentId
                       GROUP BY R.TournamentId, R.HomeUserId, R.HomeScore, R.AwayScore, R.ResultId, TU.PreviousPosition
                       UNION ALL
                       SELECT     R2.AwayUserId AS Id, 0 AS HomeDraw, CASE WHEN R2.AwayScore = R2.HomeScore THEN 1 ELSE 0 END AS AwayDraw, R2.TournamentId, 0 AS HomePlayed, 0 AS HomeWon, 
                                             0 AS HomeLost, 0 AS HomeGoalsFor, 0 AS HomeGoalsAgainst, CASE ISNULL(R2.AwayScore, - 1) WHEN - 1 THEN 0 ELSE 1 END AS AwayPlayed, 
                                             CASE WHEN R2.HomeScore < R2.AwayScore THEN 1 ELSE 0 END AS AwayWon, CASE WHEN R2.HomeScore > R2.AwayScore THEN 1 ELSE 0 END AS AwayLost, 
                                             R2.AwayScore AS AwayGoalsFor, TU.PreviousPosition, R2.HomeScore AS AwayGoalsAgainst
                       FROM         dbo.Results AS R2 INNER JOIN
                                             dbo.Users AS U ON U.UserId = R2.AwayUserId INNER JOIN
                                             dbo.TournamentUsers AS TU ON TU.UserId = R2.AwayUserId AND TU.TournamentId = R2.TournamentId
                       GROUP BY R2.TournamentId, R2.AwayUserId, R2.HomeScore, R2.AwayScore, R2.ResultId, TU.PreviousPosition) AS U_1 INNER JOIN
                      dbo.Tournament AS T ON U_1.TournamentId = T.TournamentId INNER JOIN
                      dbo.Users AS uOwner ON uOwner.UserId = T.OwnerId
GROUP BY U_1.TournamentId, U_1.Id, T.TournamentName, T.Date, T.OwnerId, T.Completed, T.OwnerId, uOwner.Forename, uOwner.Surname, U_1.PreviousPosition
ORDER BY U_1.TournamentId DESC, TotalPoints DESC, GoalDifference DESC, [For] DESC

GO
