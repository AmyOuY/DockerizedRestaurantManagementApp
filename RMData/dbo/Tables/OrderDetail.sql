CREATE TABLE [dbo].[OrderDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DiningTableId] INT NOT NULL, 
    [AttendantId] INT NOT NULL, 
    [FoodId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [OrderDate] DATETIME2 NOT NULL, 
    [OrderId] INT NULL, 
    CONSTRAINT [FK_OrderDetail_ToDiningTable] FOREIGN KEY (DiningTableId) REFERENCES DiningTable(Id), 
    CONSTRAINT [FK_OrderDetail_ToPerson] FOREIGN KEY (AttendantId) REFERENCES Person(Id), 
    CONSTRAINT [FK_OrderDetail_ToFood] FOREIGN KEY (FoodId) REFERENCES Food(Id)
)
