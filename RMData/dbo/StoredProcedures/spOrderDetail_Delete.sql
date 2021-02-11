CREATE PROCEDURE [dbo].[spOrderDetail_Delete]
	@Id int

AS
	delete from dbo.OrderDetail
	where Id = @Id;

RETURN 0
