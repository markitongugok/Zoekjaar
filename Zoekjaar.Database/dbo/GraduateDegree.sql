CREATE TABLE [dbo].[GraduateDegree]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GraduateId] INT NOT NULL, 
    [University] VARCHAR(50) NOT NULL, 
    [Degree] VARCHAR(50) NOT NULL, 
    [Specialisation] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_GraduateDegree_Graduate] FOREIGN KEY ([GraduateId]) REFERENCES [Graduate]([Id])
)
