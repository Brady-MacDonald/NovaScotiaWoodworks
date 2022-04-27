CREATE PROCEDURE [dbo].[spUsers_Get]
	@UserName nvarchar(50)
AS
BEGIN 
	SELECT [Id], [FirstName], [LastName], [EmailAddress], [UserName], [Password], [Salt]
	FROM dbo.Users
	WHERE UserName = @UserName;
END