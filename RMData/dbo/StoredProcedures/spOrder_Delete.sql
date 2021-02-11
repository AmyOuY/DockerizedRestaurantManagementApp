CREATE PROCEDURE [dbo].[spOrder_Delete]
	@Id int

AS
	delete from dbo.[Order]
	where Id = @Id;

RETURN 0
