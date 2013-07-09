; WITH LookupType_CTE (Id, Name) AS (
		  SELECT 1, 'Current Status'
	UNION SELECT 2, 'Visa Status'
	UNION SELECT 3, 'Proficiency'
	UNION SELECT 4, 'Recruitment Stage'
	UNION SELECT 5, 'State'
	UNION SELECT 6, 'Date Posted'
	UNION SELECT 7, 'Job Type'
) 
MERGE INTO dbo.LookupType
	USING LookupType_CTE as cte
	ON dbo.LookupType.Id = cte.Id
	WHEN MATCHED THEN 
		UPDATE SET  Name = cte.Name					
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (Id, Name) VALUES (cte.Id, cte.Name)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;