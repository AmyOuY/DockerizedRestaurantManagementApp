CREATE PROCEDURE [dbo].[spDiningTable_GetByTableNumber]
	@TableNumber int
	
AS
	select Id, TableNumber, Seats
	from dbo.DiningTable
	where TableNumber = @TableNumber;

RETURN 0
