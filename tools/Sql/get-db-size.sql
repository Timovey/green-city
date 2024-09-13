USE ---Название базы-
GO
SELECT DB_NAME() AS [Database Name], 
    name AS [Logical File Name],
	 sys.database_files.name,
    type_desc,
    size/128.0 AS [Current Size (MB)],  
    size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS INT)/128.0 AS [Free Space (MB)]
FROM sys.database_files
WHERE type IN (0,1);