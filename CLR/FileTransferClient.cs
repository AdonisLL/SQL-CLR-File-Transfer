using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.IO;
using System.Threading.Tasks;

public class FileTransferClient
{
    private string _storageAccountConnectionString;
    private readonly string _blobContainer;
    private readonly string _filePath;
    private CloudStorageAccount _cloudStorageaccount = null;

    public FileTransferClient(string storageAccountConnectionString,
        string blobContainer, string filePath)
    {
        _storageAccountConnectionString = storageAccountConnectionString;
        _blobContainer = blobContainer;
        _filePath = filePath;
    }

    public async Task<bool> fileaccntAsync()
    {
        try
        {
            var fileName = Path.GetFileName(_filePath);
            CloudStorageAccount account = GetStorageAccount();
            CloudFileClient client = account.CreateCloudFileClient();

            var pathData = new Uri(_filePath).Segments;

            //get File Share
            CloudFileShare cloudFileShare = client.GetShareReference(pathData[1].Replace("/", ""));

            string[] directories = pathData.Length > 3 ? new string[pathData.Length - 3] : new string[1] { pathData[1].Replace("/", "") };

            CloudFileDirectory fileDirectory = null;
            if (pathData.Length > 3)
            {
                Array.Copy(pathData, 2, directories, 0, directories.Length);

                //get the related directory
                CloudFileDirectory root = cloudFileShare.GetRootDirectoryReference();
                fileDirectory = root;
                foreach (string directory in directories)
                {
                    fileDirectory = fileDirectory.GetDirectoryReference(directory);
                }
            }
            else
            {
                fileDirectory = cloudFileShare.GetRootDirectoryReference();
            }

            //get the file reference
            CloudFile file = fileDirectory.GetFileReference(fileName);

            //Upload to blob storage
            await uploadBlobAsync(file);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task uploadBlobAsync(CloudFile file)
    {
        var fileName = Path.GetFileName(_filePath);

        CloudStorageAccount account = GetStorageAccount();
        CloudBlobClient blobClient = account.CreateCloudBlobClient();
        await blobClient.GetContainerReference(_blobContainer).CreateIfNotExistsAsync();
        var container = blobClient.GetContainerReference(_blobContainer);

        CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(fileName);

        using (Stream str = await cloudBlockBlob.OpenWriteAsync())
        {
            await file.DownloadToStreamAsync(str);
        };
    }

    private CloudStorageAccount GetStorageAccount()
    {
        if (_cloudStorageaccount == null)
        {
            return CloudStorageAccount.Parse(_storageAccountConnectionString);
        }
        return _cloudStorageaccount;
    }
}