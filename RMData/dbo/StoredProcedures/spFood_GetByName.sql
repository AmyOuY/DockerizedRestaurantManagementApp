CREATE PROCEDURE [dbo].[spFood_GetByName]
	@FoodName nvarchar(50)
	
AS
	select Id, TypeId, FoodName, Price
	from dbo.Food
	where FoodName = @FoodName;

RETURN 0
