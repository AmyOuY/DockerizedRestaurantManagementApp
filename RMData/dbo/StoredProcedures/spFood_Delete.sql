CREATE PROCEDURE [dbo].[spFood_Delete]
	@Id int

AS
	delete from dbo.Food
	where Id = @Id;

RETURN 0
