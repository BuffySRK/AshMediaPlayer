using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Media.Player.Storage
{
    public interface IStorageService
    {
        Task<string> AddToStorage(IFormFile file);
    }

    public class AzureBlob : IStorageService
    {
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _blobClient;
        private CloudBlobContainer _blobContainer;

        private const string containerName = "rebmemcontainer";

        public AzureBlob()
        {
            _storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=rebmemmedia;AccountKey=YrOHah8B+sQobrMAby6yF6HvMvRsAq3o2j2vLPlVCG1WUDTK3hThMBW/cf/IeD5j5yE0aL5kv1zduVhhXoHdaw==;EndpointSuffix=core.windows.net");
            _blobClient = _storageAccount.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference(containerName);
        }

        public async Task<string> AddToStorage(IFormFile file)
        {
            var blockBlob = _blobContainer.GetBlockBlobReference(file.FileName);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return blockBlob.Uri.ToString();
        }
    }
}
