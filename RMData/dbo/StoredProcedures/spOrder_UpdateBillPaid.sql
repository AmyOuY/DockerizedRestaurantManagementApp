CREATE PROCEDURE [dbo].[spOrder_UpdateBillPaid]
	@Id int
	
AS
	update dbo.[Order]
	set BillPaid = 1
	where Id = @Id;

RETURN 0
