CREATE PROCEDURE [dbo].[spFoodType_GetAll]

AS
	select Id, FoodType
	from dbo.FoodType;

RETURN 0
