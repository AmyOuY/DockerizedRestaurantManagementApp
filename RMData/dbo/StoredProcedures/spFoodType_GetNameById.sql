CREATE PROCEDURE [dbo].[spFoodType_GetNameById]
	@Id int

AS
	select FoodType
	from dbo.FoodType
	where Id = @Id;

RETURN 0
