using System;

public class CLRFileTransfer
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static string MoveFileToBlob(string storageAccountConnectionString,
        string blobContainerName, string fileUrl)
    {
        try
        {
            FileTransferClient client =
                new FileTransferClient(storageAccountConnectionString,
               blobContainerName, fileUrl);
            return client.fileaccntAsync().Result.ToString();
        }
        catch (Exception ex)
        {
            return ex.InnerException.Message;
        }
    }
}