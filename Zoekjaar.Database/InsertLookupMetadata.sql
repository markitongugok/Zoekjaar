; WITH LookupMetadata_CTE (Id, LookupId, Metadata) AS (
		  SELECT 1, 600, '[Badge("label-success")]'
	UNION SELECT 2, 601, '[Badge("label-warning")]'
	UNION SELECT 3, 602, '[Badge("label-info")]'
	UNION SELECT 4, 603, '[Badge("label-inverse")]'
	UNION SELECT 5, 604, '[Badge("label-default")]'
) 
MERGE INTO dbo.LookupMetadata
	USING LookupMetadata_CTE as cte
	ON dbo.LookupMetadata.Id = cte.Id
	WHEN MATCHED THEN 
		UPDATE SET  LookupId = cte.LookupId,
					Metadata = cte.Metadata
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (Id, LookupId, Metadata) VALUES (cte.Id, cte.LookupId, cte.Metadata)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;