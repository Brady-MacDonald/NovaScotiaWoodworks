CREATE PROCEDURE [dbo].[spOrders_GetByProduct]
	@Product nvarchar(50)
AS
BEGIN 
	SELECT [Id], [UserName], [EmailAddress], [Product], [OrderTime], [Status]
	FROM dbo.Orders
	WHERE Product = @Product;
END