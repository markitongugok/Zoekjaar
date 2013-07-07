CREATE TABLE [dbo].[GraduateExperience]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GraduateId] INT NOT NULL, 
    [CompanyName] VARCHAR(50) NOT NULL, 
    [CompanyProfile] VARCHAR(200) NOT NULL, 
    [Experience] VARCHAR(MAX) NOT NULL
	CONSTRAINT [FK_GraduateExperience_Graduate] FOREIGN KEY ([GraduateId]) REFERENCES [Graduate]([Id])
)
