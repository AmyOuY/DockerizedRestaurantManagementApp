CREATE PROCEDURE [dbo].[spOrderDetail_GetByDiningTableIdUnpaid]
	@DiningTableId int

AS
	select Id, DiningTableId, AttendantId, FoodId, Quantity, OrderDate, OrderId
	from dbo.OrderDetail
	where DiningTableId = @DiningTableId and (OrderId IS NULL or OrderId = 0);

RETURN 0
