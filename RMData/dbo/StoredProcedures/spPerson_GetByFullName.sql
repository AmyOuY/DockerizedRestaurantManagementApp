CREATE PROCEDURE [dbo].[spPerson_GetByFullName]
	@FirstName nvarchar(50),
	@LastName nvarchar(50)

AS
	select Id, EmployeeID, FirstName, LastName, Email, Phone
	from dbo.Person
	where FirstName = @FirstName and LastName = @LastName;

RETURN 0
