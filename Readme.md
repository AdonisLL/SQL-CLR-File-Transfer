# Sample Code for SQL CLR 

This sample code copies files from an Azure file share to an Azure Blob container via SQL CLR

On a native SQL Server, you can reference assemblies locally on the file system but on an SQL Server Managed Instance you  can not reference assemblies locally so the CLR SQL install script contains assemblies to be imported via a binary string to allow the CLR functionality in SQL Managed instance.

The code sample has also been included for reference.

## Installation

1. Open the SQL installation script from the repository.

2. Update the Using Statement in the script to reference your database.

3. Execute the script.

## Usage

The installation script creates a  SQL function named MoveToBlob which executes the CLR code taking the following parameters.

1. @storageAccountConnectionString = the connection string for the Azure storage account containing the Azure FIle share and Blob container.
2. @blobContainerName = The name of the BLob container which the files will be copied to.
3. @fileUrl the URL of the file copied from Azure File Share.

Sample execution 
` SELECT dbo.MoveFileToBlob('ConnectionString','BlobContainerName','FileUrl') `




## References 

[Managing CLR Integration Assemblies](https://docs.microsoft.com/en-us/sql/relational-databases/clr-integration/assemblies/managing-clr-integration-assemblies?view=sql-server-ver16)

[SQL Server CLR Function on Azure SQL Server Managed Instance](https://thedatacrew.com/sql-server-clr-function-on-azure-sql-server-managed-instance/)

[Create External Data SSource](https://docs.microsoft.com/en-us/sql/t-sql/statements/create-external-data-source-transact-sql?view=azuresqldb-mi-current&preserve-view=true&tabs=dedicated)