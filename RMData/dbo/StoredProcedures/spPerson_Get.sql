CREATE PROCEDURE [dbo].[spPerson_Get]
	@Id int

AS
	select Id, EmployeeID, FirstName, LastName, Email, Phone
	from dbo.Person
	where Id = @Id;

RETURN 0
