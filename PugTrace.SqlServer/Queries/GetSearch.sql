SELECT [Traces].* 
FROM [PugTrace].[Trace] AS [Traces] 
WHERE ([UtcDateTime] BETWEEN @From AND @To) AND ([Message] LIKE CONCAT('%', @Value ,'%')) AND (@FilterType IS NULL OR [EventType] = @FilterType)