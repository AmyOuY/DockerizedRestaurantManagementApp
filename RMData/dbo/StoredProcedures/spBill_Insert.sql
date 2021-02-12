CREATE PROCEDURE [dbo].[spBill_Insert]
	@Id int = 0 output,
	@OrderId int,
	@DiningTableId int,
	@AttendantId int,
	@SubTotal money,
	@Tax money,
	@Total money,
	@BillDate datetime2,
	@BillPaid bit

AS
	insert into dbo.Bill (OrderId, DiningTableId, AttendantId, SubTotal, Tax, Total, BillDate, BillPaid)
	values (@OrderId, @DiningTableId, @AttendantId, @SubTotal, @Tax, @Total, @BillDate, @BillPaid);

	select @Id = SCOPE_IDENTITY();

RETURN 0
