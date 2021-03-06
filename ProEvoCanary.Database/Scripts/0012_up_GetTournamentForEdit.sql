IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_GetTournamentForEdit')
BEGIN
    exec ('create procedure up_GetTournamentForEdit as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_GetTournamentForEdit]
@TournamentId int
AS

SELECT TournamentId, OwnerId, TournamentName, Date, Completed,TournamentType
FROM Tournament
WHERE TournamentId = @TournamentId

SELECT R.ResultId, R.HomeScore, R.AwayScore, HomeTeam = HU.Forename + ' ' + HU.Surname, AwayTeam = AU.Forename + ' ' + AU.Surname, R.TournamentId
FROM Results R JOIN Users HU ON HU.UserId =R.HomeUserId JOIN Users AU ON AU.UserId = R.AwayUserId  
WHERE R.TournamentId = @TournamentId