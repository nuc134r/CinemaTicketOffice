sqlcmd -S SERGIO-LAPTOP\MSSQL2008R2 -E -Q "EXEC sp_BackupDatabases @backupLocation='D:\SQLBackup\', @backupType='F'"

PAUSE