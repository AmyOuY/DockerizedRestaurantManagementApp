CREATE TABLE [dbo].[Bill]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderId] INT NOT NULL, 
    [DiningTableId] INT NOT NULL, 
    [AttendantId] INT NOT NULL, 
    [SubTotal] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    [Total] MONEY NOT NULL, 
    [BillDate] DATETIME2 NOT NULL, 
    [BillPaid] BIT NOT NULL, 
    CONSTRAINT [FK_Bill_ToOrder] FOREIGN KEY (OrderId) REFERENCES [Order](Id), 
    CONSTRAINT [FK_Bill_ToDiningTable] FOREIGN KEY (DiningTableId) REFERENCES DiningTable(Id), 
    CONSTRAINT [FK_Bill_ToPerson] FOREIGN KEY (AttendantId) REFERENCES Person(Id)
)
