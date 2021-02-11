CREATE PROCEDURE [dbo].[spOrder_Update]
	@Id int,
	@DiningTableId int,
	@AttendantId int,
	@SubTotal money,
	@Tax money,
	@Total money,
	@CreatedDate datetime2,
	@BillPaid bit

AS
	update dbo.[Order]
	set DiningTableId = @DiningTableId, AttendantId = @AttendantId, SubTotal = @SubTotal, Tax = @Tax, Total = @Total, CreatedDate = @CreatedDate, BillPaid = @BillPaid
	where Id = @Id;

RETURN 0
