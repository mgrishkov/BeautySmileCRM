USE master;
GO
BACKUP DATABASE CRM
TO DISK = 'c:\Temp\CRM.bak'
   WITH FORMAT,
      NAME = 'Full Backup of AdventureWorks2008R2';
GO