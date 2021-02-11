CREATE PROCEDURE [dbo].[spFoodType_Delete]
	@Id int

AS
	delete from dbo.FoodType
	where Id = @Id;

RETURN 0
