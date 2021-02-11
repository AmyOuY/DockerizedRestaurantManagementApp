CREATE PROCEDURE [dbo].[spDiningTable_GetById]
	@Id int
	
AS
	select Id, TableNumber, Seats
	from dbo.DiningTable
	where Id = @Id;

RETURN 0
