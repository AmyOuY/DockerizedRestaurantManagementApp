CREATE PROCEDURE [dbo].[spFood_Insert]
	@Id int = 0 output,
	@TypeId int,
	@FoodName nvarchar(50),
	@Price money

AS
	insert into dbo.Food (TypeId, FoodName, Price)
	values (@TypeId, @FoodName, @Price);

	select @Id = SCOPE_IDENTITY();

RETURN 0
