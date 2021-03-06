IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_AddUser')
BEGIN
    exec ('create procedure up_AddUser as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_AddUser]
@Username varchar(50),
@Forename varchar(50),
@Surname varchar(50),
@Email varchar(100),
@UserType int = 2,
@Password varchar(300)
AS

DECLARE @UserCount int = (SELECT COUNT(*) FROM Users WHERE Username = @Username)

IF(@UserCount >0)
RETURN 0
ELSE
BEGIN
INSERT INTO Users
(
	Username,
	Forename,
	Surname,
	Email,
	UserType,
	[Hash]
)
VALUES
(
	@Username,
	@Forename,
	@Surname,
	@Email,
	@UserType,
	@Password
)

SELECT @@IDENTITY
END
