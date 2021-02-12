CREATE PROCEDURE [dbo].[spOrder_GetByDiningTableIdUnpaid]
	@DiningTableId int 
	
AS
	select Id, DiningTableId, AttendantId, SubTotal, Tax, Total, CreatedDate, BillPaid
	from dbo.[Order]
	where DiningTableId = @DiningTableId and BillPaid = 0;

RETURN 0
