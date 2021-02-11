CREATE PROCEDURE [dbo].[spOrderDetail_Update]
	@Id int,
	@DiningTableId int,
	@AttendantId int,
	@FoodId int,
	@Quantity int,
	@OrderDate datetime2,
	@OrderId int

AS
	update dbo.OrderDetail
	set DiningTableId = @DiningTableId, AttendantId = @AttendantId, FoodId = @FoodId, Quantity = @Quantity, OrderDate = @OrderDate, OrderId = @OrderId
	where Id = @Id;

RETURN 0
