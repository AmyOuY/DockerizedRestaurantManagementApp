CREATE PROCEDURE [dbo].[spFood_GetAll]
	
AS
	select Id, TypeId, FoodName, Price
	from dbo.Food
	order by TypeId;

RETURN 0
