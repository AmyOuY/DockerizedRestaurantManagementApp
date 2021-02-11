CREATE PROCEDURE [dbo].[spOrderDetail_Insert]
	@Id int = 0 output,
	@DiningTableId int,
	@AttendantId int,
	@FoodId int,
	@Quantity int,
	@OrderDate datetime2,
	@OrderId int

AS
	insert into dbo.OrderDetail (DiningTableId, AttendantId, FoodId, Quantity, OrderDate, OrderId)
	values (@DiningTableId, @AttendantId, @FoodId, @Quantity, @OrderDate, @OrderId);

	select @Id = SCOPE_IDENTITY();

RETURN 0
