CREATE PROCEDURE [dbo].[spFood_Update]
	@Id int,
	@TypeId int,
	@FoodName nvarchar(50),
	@Price money

AS
	update dbo.Food
	set TypeId = @TypeId, FoodName = @FoodName, Price = @Price
	where Id = @Id;

RETURN 0
