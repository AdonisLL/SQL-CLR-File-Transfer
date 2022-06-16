# Sample Code for SQL CLR 

This sample code copies files from an Azure file share to an Azure Blob container via SQL CLR

On a native SQL Server, you can reference assemblies locally on the file system but on an SQL Server Managed Instance you  can not reference assemblies locally so the CLR SQL install script contains assemblies to be imported via a binary string to allow the CLR functionality in SQL Managed instance.

The code sample has also been included for reference.

## Installation

1. Open and run the SQL script named 'ADD_SYS_TRUSTED_ASSEMBLIES.sql' from the repository. This script adds the assemblies to the system table 'sys.trusted_assemblies', by listing the assemblies as trusted they can be loaded into SQL server when the default setting of  `clr strict security` is on and without having to set the entire database as `TRUSTWORTY` for security purposes.

2. Open the SQL script named 'CLR_INSTALL.sql' update the using statement in the script to reference the database you wish to use with the CLR function and run the script, this adds the assemblies needed to execute the CLR function.

## Usage

The installation script creates a  SQL function named MoveFileToBlob which executes the CLR code taking the following parameters.

1. @storageAccountConnectionString = the connection string for the Azure storage account containing the Azure File share and Blob container.
2. @blobContainerName = The name of the Blob container which the files will be copied to.
3. @fileUrl the URL of the file copied from Azure File Share.

Sample execution 
` SELECT dbo.MoveFileToBlob('ConnectionString','BlobContainerName','FileUrl') `

Currently for testing simplicity the select statement returns a string of 'TRUE' or the Exception Text if an error occurs.

`DECLARE @resultvalue nvarchar(max) = (SELECT dbo.MoveFileToBlob('ConnectionString','BlobContainerName','FileUrl')))'`

`IF @resultvalue = 'TRUE'`
	`PRINT 'Transfer Was Successful'`
`ELSE`
  ` PRINT 'Transfer Failed'`

## References 

[Managing CLR Integration Assemblies](https://docs.microsoft.com/en-us/sql/relational-databases/clr-integration/assemblies/managing-clr-integration-assemblies?view=sql-server-ver16)

[SQL Server CLR Function on Azure SQL Server Managed Instance](https://thedatacrew.com/sql-server-clr-function-on-azure-sql-server-managed-instance/)

[Create External Data Source](https://docs.microsoft.com/en-us/sql/t-sql/statements/create-external-data-source-transact-sql?view=azuresqldb-mi-current&preserve-view=true&tabs=dedicated)