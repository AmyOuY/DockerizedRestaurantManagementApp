CREATE PROCEDURE [dbo].[spPerson_Insert]
	@Id int = 0 output,
	@EmployeeID int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(50),
	@Phone nvarchar(20)

AS
	insert into dbo.Person (EmployeeID, FirstName, LastName, Email, Phone)
	values (@EmployeeID, @FirstName, @LastName, @Email, @Phone);

	select @Id = SCOPE_IDENTITY();

RETURN 0
