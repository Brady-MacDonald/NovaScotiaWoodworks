CREATE PROCEDURE [dbo].[spOrders_GetAll]
AS
BEGIN
	SELECT [Id], [UserName], [EmailAddress], [Product], [OrderTime], [Status]
	FROM dbo.Orders
END
