RESTORE DATABASE [ContainerizedRMData] FROM DISK = '/tmp/ContainerizedRMData.bak'
WITH FILE = 1,
MOVE 'ContainerizedRMData' TO '/var/opt/mssql/data/ContainerizedRMData_backup.mdf',
MOVE 'ContainerizedRMData_log' TO '/var/opt/mssql/data/ContainerizedRMData_backup.ldf',
NOUNLOAD, REPLACE, STATS = 5
GO

/*RESTORE FILELISTONLY FROM DISK = 'C:\Users\ouyim\Demos\RMDataBackup\ContainerizedRMData.bak' to find out names of mdf and ldf*/