; SET IDENTITY_INSERT dbo.[CompanyJob] ON
; WITH CompanyJob_CTE (Id, CompanyId, JobNumber, Title, HiringManager, HrManager, JobType, JobFunction, OrgLevel, JobDescription, Criteria, Degree, Specialisation, OtherCriteria, VisaStatusId, StartDate, IsFeatured) AS (
			SELECT 1, 1, '1', 'Jr. Business Analyst', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Entry', 'This job is a featured job.', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1
	UNION	SELECT 2, 1, '1', 'Sr. C# Developer', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Supervisory', 'This job is a featured job.', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1
	UNION	SELECT 3, 1, '1', 'ASP.Net Developer', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Mid Level', 'This job is a featured job.', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1
	UNION	SELECT 4, 1, '1', 'Development Manager', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Managerial', 'This job is a featured job.', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1
	UNION	SELECT 5, 2, '1', 'Sr. C# Developer', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Supervisory', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0
	UNION	SELECT 6, 2, '1', 'ASP.Net Developer', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Mid Level', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0
	UNION	SELECT 7, 2, '1', 'Development Manager', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Managerial', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0
	UNION	SELECT 8, 2, '1', 'Sr. C# Developer', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Supervisory', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0
	UNION	SELECT 9, 2, '1', 'ASP.Net Developer', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Mid Level', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0
	UNION	SELECT 10, 2, '1', 'Development Manager', 'John Doe', 'Jane Doe', 'Permanent', 'Technical', 'Managerial', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0
) 
MERGE INTO dbo.[CompanyJob]
	USING CompanyJob_CTE as cte
	ON dbo.[CompanyJob].Id = cte.Id
	WHEN MATCHED THEN 
		UPDATE SET  CompanyId = cte.CompanyId,
					JobNumber = cte.JobNumber,
					Title = cte.Title,
					HiringManager = cte.HiringManager,
					HrManager = cte.HrManager,
					JobFunction = cte.JobFunction,
					OrgLevel = cte.OrgLevel,
					JobDescription = cte.JobDescription,
					Criteria = cte.Criteria,
					Degree = cte.Degree,
					Specialisation = cte.Specialisation,
					OtherCriteria = cte.OtherCriteria,
					VisaStatusId = cte.VisaStatusId,
					StartDate = cte.StartDate,
					IsFeatured = cte.IsFeatured
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (Id, CompanyId, JobNumber, Title, HiringManager, HrManager, JobType, JobFunction, OrgLevel, JobDescription, Criteria, Degree, Specialisation, OtherCriteria, VisaStatusId, StartDate, IsFeatured)
		VALUES (cte.Id, cte.CompanyId, cte.JobNumber, cte.Title, cte.HiringManager, cte.HrManager, cte.JobType, cte.JobFunction, cte.OrgLevel, cte.JobDescription, cte.Criteria, cte.Degree, cte.Specialisation, cte.OtherCriteria, cte.VisaStatusId, cte.StartDate, cte.IsFeatured)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	DECLARE @companyJobSeed INT
		SELECT @companyJobSeed = MAX(Id) FROM dbo.[CompanyJob]
		DBCC CHECKIDENT ("dbo.[CompanyJob]", reseed, @companyJobSeed)

; SET IDENTITY_INSERT dbo.[CompanyJob] OFF