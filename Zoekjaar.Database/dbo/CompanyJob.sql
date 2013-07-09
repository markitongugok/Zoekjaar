CREATE TABLE [dbo].[CompanyJob]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyId] INT NOT NULL, 
    [JobNumber] VARCHAR(20) NOT NULL, 
	[Title] VARCHAR(100) NOT NULL, 
    [HiringManager] VARCHAR(100) NOT NULL, 
    [HrManager] VARCHAR(100) NOT NULL, 
    [JobType] VARCHAR(100) NOT NULL, 
    [JobFunction] VARCHAR(100) NOT NULL, 
    [OrgLevel] VARCHAR(100) NOT NULL, 
    [JobDescription] VARCHAR(MAX) NOT NULL, 
    [Criteria] VARCHAR(MAX) NOT NULL, 
    [Degree] VARCHAR(50) NOT NULL, 
    [Specialisation] VARCHAR(50) NOT NULL, 
    [OtherCriteria] VARCHAR(MAX) NULL, 
    [VisaStatusId] INT NOT NULL, 
    [StartDate] DATE NULL, 
    [IsFeatured] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_CompanyJob_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id])
)
