IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_GetResult')
BEGIN
    exec ('create procedure up_GetResult as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_GetResult]

@ResultId int

AS
SELECT        T1.Forename + ' ' + T1.Surname AS HomeTeam, ISNULL(R.HomeScore,-1) AS HomeScore,
ISNULL(R.AwayScore,-1) AS AwayScore, T.Forename + ' ' + T.Surname AS AwayTeam, R.TournamentId,                       
				R.Round, R.ResultId
FROM            Results R INNER JOIN
                         Users AS T1 ON R.HomeUserID = T1.UserId INNER JOIN
                         Users T ON R.AwayUserID = T.UserId
WHERE			R.ResultId =@ResultId

