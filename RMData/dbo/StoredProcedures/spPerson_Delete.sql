CREATE PROCEDURE [dbo].[spPerson_Delete]
	@Id int

AS
	delete from dbo.Person
	where Id = @Id;

RETURN 0
