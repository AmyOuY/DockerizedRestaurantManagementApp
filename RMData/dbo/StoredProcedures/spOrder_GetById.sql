CREATE PROCEDURE [dbo].[spOrder_GetById]
	@Id int
	
AS
	select Id, DiningTableId, AttendantId, SubTotal, Tax, Total, CreatedDate, BillPaid
	from dbo.[Order]
	where Id = @Id;

RETURN 0
