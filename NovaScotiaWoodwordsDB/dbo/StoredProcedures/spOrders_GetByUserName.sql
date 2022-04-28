CREATE PROCEDURE [dbo].[spOrders_GetByUserName]
	@UserName nvarchar(50)
AS
BEGIN 
	SELECT [Id], [UserName], [EmailAddress], [Product], [OrderTime], [Status]
	FROM dbo.Orders
	WHERE UserName = @UserName;
END