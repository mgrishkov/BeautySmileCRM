USE [master]
GO

if(db_id('CRM') is not null)
begin 
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'CRM'
	DROP DATABASE [CRM]
end;
GO

if(exists(select 1 
            from master.dbo.syslogins 
           where name = @loginName and dbname = 'AppUser'))
begin 
	DROP LOGIN [AppUser]
end;
GO

