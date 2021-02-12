CREATE PROCEDURE [dbo].[spBill_GetAllPaid]
	
AS
	select Id, OrderId, DiningTableId, AttendantId, SubTotal, Tax, Total, BillDate, BillPaid
	from dbo.Bill
	where BillPaid = 1;

RETURN 0
