DECLARE @sql NVARCHAR(max) = '';

SELECT @sql += 'truncate table ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
FROM   INFORMATION_SCHEMA.TABLES
WHERE 1 = 1
	and TABLE_TYPE = 'BASE TABLE'
	and TABLE_SCHEMA not like '%HangFire%'
	and TABLE_NAME not like '%migration%'
	and TABLE_NAME not like '%AspNet%'

Exec Sp_executesql @sql

