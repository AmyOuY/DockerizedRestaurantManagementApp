CREATE PROCEDURE [dbo].[spOrder_Insert]
	@Id int = 0 output,
	@DiningTableId int,
	@AttendantId int,
	@SubTotal money,
	@Tax money,
	@Total money,
	@CreatedDate datetime2,
	@BillPaid bit

AS
	insert into dbo.[Order] (DiningTableId, AttendantId, SubTotal, Tax, Total, CreatedDate, BillPaid)
	values (@DiningTableId, @AttendantId, @SubTotal, @Tax, @Total, @CreatedDate, @BillPaid);

	select @Id = SCOPE_IDENTITY();

RETURN 0
