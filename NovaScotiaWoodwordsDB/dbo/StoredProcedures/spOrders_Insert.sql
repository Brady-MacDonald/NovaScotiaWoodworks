CREATE PROCEDURE [dbo].[spOrders_Insert]
	@UserName nvarchar(50),
	@EmailAddress nvarchar(50),
	@Product nvarchar(50),
	@OrderTime datetime,
	@Status nvarchar(50)
AS
BEGIN
	INSERT INTO dbo.Orders (UserName, EmailAddress, Product, OrderTime, [Status])
	VALUES(@UserName, @EmailAddress, @Product, @OrderTime, @Status);
END
