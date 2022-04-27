CREATE PROCEDURE [dbo].[spUsers_GetAll]
AS
BEGIN
	SELECT Id, FirstName, LastName, EmailAddress, UserName, [Password], Salt
	FROM dbo.Users;
END
