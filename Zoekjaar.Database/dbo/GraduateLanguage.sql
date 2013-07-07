CREATE TABLE [dbo].[GraduateLanguage]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[GraduateId] INT NOT NULL, 
	[Language] VARCHAR(100) NOT NULL, 
	[ProficiencyId] INT NOT NULL
	CONSTRAINT [FK_GraduateLanguage_Graduate] FOREIGN KEY ([GraduateId]) REFERENCES [Graduate]([Id]), 
	CONSTRAINT [FK_GraduateLanguage_Lookup] FOREIGN KEY ([ProficiencyId]) REFERENCES [Lookup]([Id])
)
