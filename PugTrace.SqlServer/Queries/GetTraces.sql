SELECT [Traces].*
FROM (
	SELECT ROW_NUMBER() OVER(ORDER BY [UtcDateTime] DESC) AS NUMBER, * 
	FROM [PugTrace].[Trace]
	WHERE (@type IS NULL OR [EventType] = @type)
) AS [Traces]
WHERE NUMBER BETWEEN (@skip + 1) AND (@top + @skip) ORDER BY [UtcDateTime] DESC