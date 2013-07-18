CREATE TABLE [dbo].[LookupMetadata]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [LookupId] INT NOT NULL, 
    [Metadata] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_LookupMetadata_Lookup] FOREIGN KEY ([LookupId]) REFERENCES [Lookup]([Id])
)
