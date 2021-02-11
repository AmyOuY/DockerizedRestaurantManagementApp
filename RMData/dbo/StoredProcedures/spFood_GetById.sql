CREATE PROCEDURE [dbo].[spFood_GetById]
	@Id int

AS
	select Id, TypeId, FoodName, Price
	from dbo.Food
	where Id = @Id;

RETURN 0
