﻿; WITH Lookup_CTE (Id, LookupTypeId, Name, IsActive, IsDefault) AS (

	/* Current Status */
		  SELECT 1, 1, 'N/A', 1, 1
	UNION SELECT 2, 1, 'Studying', 1, 0
	UNION SELECT 3, 1, 'Looking for internship', 1, 0
	UNION SELECT 4, 1, 'Internship', 1, 0
	UNION SELECT 5, 1, 'Looking for Job', 1, 0

	/* Visa Status */
	UNION SELECT 100, 2, 'N/A', 1, 1
	UNION SELECT 101, 2, 'EU Citizen', 1, 0
	UNION SELECT 102, 2, 'Study & Internship Visa', 1, 0
	UNION SELECT 103, 2, 'Orientation Year Visa', 1, 0
	UNION SELECT 104, 2, 'High Skilled Migrant Visa', 1, 0

	/* Proficiency */
	UNION SELECT 200, 3, 'Basic Understanding', 1, 1
	UNION SELECT 201, 3, 'Conversational', 1, 0
	UNION SELECT 202, 3, 'Fluent', 1, 0

	UNION SELECT 300, 4, 'Shortlisted', 1, 1
	UNION SELECT 301, 4, 'Invited for Interview', 1, 0
	UNION SELECT 302, 4, 'Conducted Interview', 1, 0
	UNION SELECT 303, 4, 'Offer Sent', 1, 0
	UNION SELECT 304, 4, 'Rejected', 1, 0
	UNION SELECT 305, 4, 'Recruited', 1, 0
) 
MERGE INTO dbo.Lookup
	USING Lookup_CTE as cte
	ON dbo.Lookup.Id = cte.Id
	WHEN MATCHED THEN 
		UPDATE SET dbo.Lookup.Name = cte.Name,
					LookupTypeId = cte.LookupTypeId,
					IsActive = cte.IsActive,
					IsDefault = cte.IsDefault
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (Id, LookupTypeId, Name, IsActive, IsDefault) VALUES (cte.Id, cte.LookupTypeId, cte.Name, cte.IsActive, cte.IsDefault)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;