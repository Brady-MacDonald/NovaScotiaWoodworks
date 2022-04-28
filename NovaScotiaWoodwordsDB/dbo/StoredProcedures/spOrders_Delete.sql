CREATE PROCEDURE [dbo].[spOrders_Delete]
	@Id int
AS
BEGIN
	DELETE 
	FROM dbo.Orders
	WHERE Id = @Id
END 