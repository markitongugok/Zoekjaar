; SET IDENTITY_INSERT dbo.[Job] ON
; WITH Job_CTE (Id, CompanyId, JobNumber, Title, HiringManager, HrManager, JobTypeId, JobFunction, OrgLevel, JobDescription, Criteria, Degree, Specialisation, OtherCriteria, VisaStatusId, StartDate, IsFeatured, DatePosted) AS (
			SELECT 1, 1, '1', 'Jr. Business Analyst', 'John Doe', 'Jane Doe', 600, 'Technical', 'Entry', 'This job is a featured job.', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1, getdate()
	UNION	SELECT 2, 1, '1', 'Sr. C# Developer', 'John Doe', 'Jane Doe', 601, 'Technical', 'Supervisory', 'This job is a featured job.', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1, getdate()
	UNION	SELECT 3, 1, '1', 'ASP.Net Developer', 'John Doe', 'Jane Doe', 602, 'Technical', 'Mid Level', 'This job is a featured job.', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1, getdate()
	UNION	SELECT 4, 1, '1', 'Development Manager', 'John Doe', 'Jane Doe', 603, 'Technical', 'Managerial', 'This job is a featured job.', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1, getdate()
	UNION	SELECT 5, 2, '1', 'Sr. C# Developer', 'John Doe', 'Jane Doe', 604, 'Technical', 'Supervisory', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0, getdate()
	UNION	SELECT 6, 2, '1', 'ASP.Net Developer', 'John Doe', 'Jane Doe', 600, 'Technical', 'Mid Level', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0, getdate()
	UNION	SELECT 7, 2, '1', 'Development Manager', 'John Doe', 'Jane Doe', 601, 'Technical', 'Managerial', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0, getdate()
	UNION	SELECT 8, 2, '1', 'Sr. C# Developer', 'John Doe', 'Jane Doe', 602, 'Technical', 'Supervisory', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1, getdate()
	UNION	SELECT 9, 2, '1', 'ASP.Net Developer', 'John Doe', 'Jane Doe', 603, 'Technical', 'Mid Level', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 1, getdate()
	UNION	SELECT 10, 2, '1', 'Development Manager', 'John Doe', 'Jane Doe', 604, 'Technical', 'Managerial', 'This job is not a featured job', 'Graduate', 'College', 'Software', 'Good english communication skills.', 100, getdate(), 0, getdate()
) 
MERGE INTO dbo.[Job]
	USING Job_CTE as cte
	ON dbo.[Job].Id = cte.Id
	WHEN MATCHED THEN 
		UPDATE SET  CompanyId = cte.CompanyId,
					JobNumber = cte.JobNumber,
					Title = cte.Title,
					HiringManager = cte.HiringManager,
					HrManager = cte.HrManager,
					JobTypeId = cte.JobTypeId,
					JobFunction = cte.JobFunction,
					OrgLevel = cte.OrgLevel,
					JobDescription = cte.JobDescription,
					Criteria = cte.Criteria,
					Degree = cte.Degree,
					Specialisation = cte.Specialisation,
					OtherCriteria = cte.OtherCriteria,
					VisaStatusId = cte.VisaStatusId,
					StartDate = cte.StartDate,
					IsFeatured = cte.IsFeatured,
					DatePosted = cte.DatePosted
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (Id, CompanyId, JobNumber, Title, HiringManager, HrManager, JobTypeId, JobFunction, OrgLevel, JobDescription, Criteria, Degree, Specialisation, OtherCriteria, VisaStatusId, StartDate, IsFeatured, DatePosted)
		VALUES (cte.Id, cte.CompanyId, cte.JobNumber, cte.Title, cte.HiringManager, cte.HrManager, cte.JobTypeId, cte.JobFunction, cte.OrgLevel, cte.JobDescription, cte.Criteria, cte.Degree, cte.Specialisation, cte.OtherCriteria, cte.VisaStatusId, cte.StartDate, cte.IsFeatured, cte.DatePosted)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	DECLARE @companyJobSeed INT
		SELECT @companyJobSeed = MAX(Id) FROM dbo.[Job]
		DBCC CHECKIDENT ([Job], reseed, @companyJobSeed)

; SET IDENTITY_INSERT dbo.[Job] OFF