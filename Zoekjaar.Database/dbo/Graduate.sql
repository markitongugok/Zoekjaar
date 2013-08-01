CREATE TABLE [dbo].[Graduate]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] INT NOT NULL,
	[FirstName] VARCHAR(50) NOT NULL, 
	[LastName] VARCHAR(50) NOT NULL, 
	[Profile] VARCHAR(MAX) NULL,
	[CurrentStatusId] INT NULL, 
	[VisaStatusId] INT NULL, 
	[AvailableFromDate] DATE NULL, 
	[PcSkills] VARCHAR(400) NULL, 
	[OtherSkills] VARCHAR(400) NULL, 
	[LinkedIn] VARCHAR(400) NULL, 
	[GooglePlus] VARCHAR(400) NULL, 
	[IsActive] BIT NOT NULL DEFAULT 1    
	CONSTRAINT [FK_Graduate_CurrentStatusId_Lookup] FOREIGN KEY ([CurrentStatusId]) REFERENCES [Lookup]([Id]), 
	[LogoUrl] VARCHAR(200) NULL, 
    CONSTRAINT [FK_Graduate_VisaStatusId_Lookup] FOREIGN KEY ([VisaStatusId]) REFERENCES [Lookup]([Id]), 
	CONSTRAINT [FK_Graduate_UserId_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
)
