; WITH Graduate_CTE (Id, UserId, FirstName, LastName, [Profile], CurrentStatusId, VisaStatusId, AvailableFromDate, IsActive) AS (
		  SELECT 1, 1, 'Admin', 'Graduate', 'Admin Graduate Profile', 1, 100, getdate(), 1
) 
MERGE INTO dbo.Graduate
	USING Graduate_CTE as cte
	ON dbo.Graduate.Id = cte.Id
	WHEN MATCHED THEN 
		UPDATE SET  UserId = cte.UserId,
					FirstName = cte.FirstName,
					LastName = cte.LastName,
					[Profile] = cte.[Profile],
					CurrentStatusId = cte.CurrentStatusId,
					VisaStatusId = cte.VisaStatusId,
					AvailableFromDate = cte.AvailableFromDate,
					IsActive = cte.IsActive
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (UserId, FirstName, LastName, [Profile], CurrentStatusId, VisaStatusId, AvailableFromDate, IsActive) 
		VALUES (cte.Userid, cte.FirstName, cte.LastName, cte.[Profile], cte.CurrentStatusId, cte.VisaStatusId, cte.AvailableFromDate, cte.IsActive)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	DECLARE @seed INT
		SELECT @seed = MAX(Id) FROM dbo.Graduate
		DBCC CHECKIDENT ("dbo.Graduate", reseed, @seed)