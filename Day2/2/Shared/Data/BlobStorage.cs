using Azure.Storage.Blobs;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Data;

public class BlobStorage : IStorage
{
    private readonly BlobContainerClient blobContainerClient;

    public BlobStorage(string storageConnectionString)
    {
        BlobContainerClient container = new(storageConnectionString, Constants.BlobStorageContainerName);
        container.CreateIfNotExists();
        this.blobContainerClient = container;
    }

    public async Task<string> Save(MemoryStream stream, string pngFileName)
    {
        BlobClient? pictureCloudBlockBlob = this.blobContainerClient.GetBlobClient($"{pngFileName}.png");
        bool fileExists = await pictureCloudBlockBlob.ExistsAsync();
        while (fileExists)
        {
            await pictureCloudBlockBlob.DeleteAsync();
            fileExists = await pictureCloudBlockBlob.ExistsAsync();
        }

        await pictureCloudBlockBlob.UploadAsync(stream);
        string picUri = pictureCloudBlockBlob.Uri.AbsoluteUri;
        return picUri;
    }

    public async Task<string> Save(byte[] bytes, string pngFileName)
    {
        using MemoryStream stream = new(bytes, 0, bytes.Length);
        return await this.Save(stream, pngFileName);
    }

    public async Task<string> Save(string base64PNG, string pngFileName)
    {
        byte[] bytes = Convert.FromBase64String(base64PNG);
        return await this.Save(bytes, pngFileName);
    }

    public async Task<MemoryStream?> Retrieve(string pngFileName)
    {
        BlobClient? pictureCloudBlockBlob = this.blobContainerClient.GetBlobClient($"{pngFileName}.png");
        bool fileExists = await pictureCloudBlockBlob.ExistsAsync();
        if(!fileExists)
        {
            return null;
        }

        using MemoryStream stream = new();
        await pictureCloudBlockBlob.DownloadToAsync(stream);
        return stream;
    }
}
