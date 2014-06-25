USE [master]

if(not exists(select 1 
            from master.dbo.syslogins 
           where name = 'AppUser'))
begin 
	CREATE LOGIN [AppUser]
		WITH PASSWORD = N'<fj,f,2014', DEFAULT_LANGUAGE = [us_english], CHECK_POLICY = OFF;
end
GO

ALTER DATABASE [CRM] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
RESTORE DATABASE [CRM] FROM  DISK = N'C:\Temp\CRM.bak' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 5
ALTER DATABASE [CRM] SET MULTI_USER
GO

use CRM
go
drop user [AppUser]
go

CREATE USER [AppUser] FOR LOGIN [AppUser];
go

EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'AppUser';
go
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'AppUser';
go
execute sp_addrolemember 'db_owner', 'AppUser';
go
