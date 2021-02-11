CREATE PROCEDURE [dbo].[spOrder_GetAllUnpaid]
	
AS
	select Id, DiningTableId, AttendantId, SubTotal, Tax, Total, CreatedDate, BillPaid
	from dbo.[Order]
	where BillPaid = 0;
	
RETURN 0
