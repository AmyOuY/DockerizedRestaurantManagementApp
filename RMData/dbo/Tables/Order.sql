CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DiningTableId] INT NOT NULL, 
    [AttendantId] INT NOT NULL, 
    [SubTotal] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    [Total] MONEY NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL, 
    [BillPaid] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Order_ToDiningTable] FOREIGN KEY (DiningTableId) REFERENCES DiningTable(Id), 
    CONSTRAINT [FK_Order_ToPerson] FOREIGN KEY (AttendantId) REFERENCES Person(Id)
)
