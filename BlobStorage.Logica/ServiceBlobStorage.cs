using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage.Logica;

public interface IServiceBlobStorage
{
    public Task<List<string>> GetBlobAsync();

    public Task UploadBlobAsync(string blobName, Stream stream);

    public Task DeleteBlobAsync(string blobName);
}

public class ServiceBlobStorage : IServiceBlobStorage
{
    private BlobServiceClient _client;

    private BlobContainerClient _containerClient;

    public ServiceBlobStorage(BlobServiceClient client)
    {
        _client = client;
        _containerClient = client.GetBlobContainerClient("pruebainicialblob");
    }

    public async Task DeleteBlobAsync(string blobName)
    {
        await _containerClient.DeleteBlobAsync(blobName);
    }

    public async Task<List<string>> GetBlobAsync()
    {
        List<string> blobs = new List<string>();
        await foreach (BlobItem blob in _containerClient.GetBlobsAsync())
        {
            blobs.Add(blob.Name);
        }
        return blobs;
    }

    public async Task UploadBlobAsync(string blobName, Stream stream)
    {
        BlobClient blobClient = _containerClient.GetBlobClient(blobName);
        await blobClient.UploadAsync(stream, overwrite:true);
    }
}
