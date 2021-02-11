CREATE PROCEDURE [dbo].[spDiningTable_GetAll]

AS
	select Id, TableNumber, Seats
	from dbo.DiningTable;

RETURN 0
