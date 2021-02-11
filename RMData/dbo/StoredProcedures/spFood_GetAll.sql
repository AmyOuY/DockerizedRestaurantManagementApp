CREATE PROCEDURE [dbo].[spFood_GetAll]
	
AS
	select Id, TypeId, FoodName, Price
	from dbo.Food;

RETURN 0
