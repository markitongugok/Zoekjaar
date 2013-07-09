CREATE TABLE [dbo].[JobApplication]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[JobId] INT NOT NULL, 
	[GraduateId] INT NOT NULL, 
	[StatusId] INT NULL, 
	CONSTRAINT [FK_JobApplication_CompanyJob] FOREIGN KEY ([JobId]) REFERENCES [Job]([Id]), 
	CONSTRAINT [FK_JobApplication_Graduate] FOREIGN KEY ([GraduateId]) REFERENCES [Graduate]([Id]), 
	CONSTRAINT [FK_JobApplication_Lookup] FOREIGN KEY ([StatusId]) REFERENCES [Lookup]([Id])
)
