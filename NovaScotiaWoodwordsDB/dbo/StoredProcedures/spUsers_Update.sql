CREATE PROCEDURE [dbo].[spUsers_Update]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@RememberMe bit,
	@EmailAddress nvarchar(50),
	@UserName nvarchar(50),
	@Password nvarchar(100),
	@Salt nvarchar(50)
AS
IF (@EmailAddress IS NULL AND @Password IS NULL AND @Salt IS NULL AND @UserName IS NULL)
BEGIN 
	UPDATE dbo.Users
	SET FirstName = @FirstName, LastName = @LastName
	/* EmailAddress = @EmailAddress, UserName = @UserName, [Password] = @Password, Salt = @Salt */
	WHERE Id = @Id;
END