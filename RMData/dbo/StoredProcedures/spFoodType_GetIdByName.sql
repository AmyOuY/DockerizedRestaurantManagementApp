CREATE PROCEDURE [dbo].[spFoodType_GetIdByName]
	@type nvarchar(50)

AS
	select Id
	from dbo.FoodType
	where FoodType = @type;

RETURN 0
