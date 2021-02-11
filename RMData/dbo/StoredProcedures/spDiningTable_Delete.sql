CREATE PROCEDURE [dbo].[spDiningTable_Delete]
	@Id int

AS
	delete from dbo.DiningTable
	where Id = @Id;

RETURN 0
