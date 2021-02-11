CREATE PROCEDURE [dbo].[spPerson_GetAll]
	
AS
	select Id, EmployeeID, FirstName, LastName, Email, Phone
	from dbo.Person;

RETURN 0
