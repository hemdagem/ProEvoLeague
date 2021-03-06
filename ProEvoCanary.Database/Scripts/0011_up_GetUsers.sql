IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_GetUsers')
BEGIN
    exec ('create procedure up_GetUsers as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_GetUsers]

AS

SELECT TOP(100) Forename + ' ' + Surname As [Name], UserId FROM Users ORDER BY [Name]