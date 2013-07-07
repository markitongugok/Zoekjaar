CREATE TABLE [dbo].[Lookup]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[LookupTypeId] INT NOT NULL,
    [Name] VARCHAR(100) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDefault] BIT NOT NULL DEFAULT 0
)
