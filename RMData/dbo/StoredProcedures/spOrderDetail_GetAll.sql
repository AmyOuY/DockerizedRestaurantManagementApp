CREATE PROCEDURE [dbo].[spOrderDetail_GetAll]
	
AS
	select Id, DiningTableId, AttendantId, FoodId, Quantity, OrderDate, OrderId
	from dbo.OrderDetail;

RETURN 0
