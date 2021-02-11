CREATE PROCEDURE [dbo].[spFood_GetByTypeId]
	@TypeId int

AS
	select Id, TypeId, FoodName, Price
	from dbo.Food
	where TypeId = @TypeId;

RETURN 0
