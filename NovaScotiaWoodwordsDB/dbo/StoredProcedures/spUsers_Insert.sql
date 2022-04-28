CREATE PROCEDURE [dbo].[spUsers_Insert]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@RememberMe bit,
	@EmailAddress nvarchar(50),
	@UserName nvarchar(50),
	@Password nvarchar(100),
	@Salt nvarchar(50)
AS
BEGIN
	INSERT INTO dbo.Users (FirstName, LastName, EmailAddress, UserName, [Password], Salt)
	VALUES (@FirstName, @LastName, @EmailAddress, @UserName, @Password, @Salt);
END