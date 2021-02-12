CREATE PROCEDURE [dbo].[spOrder_GetAll]
	
AS
	select Id, DiningTableId, AttendantId, SubTotal, Tax, Total, CreatedDate, BillPaid
	from dbo.[Order];

RETURN 0
