CREATE PROCEDURE [dbo].[spOrderDetail_GetById]
	@Id int

AS
	select Id, DiningTableId, AttendantId, FoodId, Quantity, OrderDate, OrderId
	from dbo.OrderDetail
	where Id = @Id;

RETURN 0
