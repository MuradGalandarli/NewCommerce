using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NewCommerce.Application.Abstractions.Storage.Azure;
using NewCommerce.Infrastructure.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Infrastructure
{
    public class AzureStorage :Storages, IAzureStorage
    {
         readonly BlobServiceClient _blobServiceClient;
         BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration cofiguration)
        {
            _blobServiceClient = new(cofiguration["Storage:Azure"]); 
        }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }
        public List<string> GetFiles(string path)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
            return _blobContainerClient.GetBlobs().Select(x => x.Name).ToList();  
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            return _blobContainerClient.GetBlobs().Any(x => x.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in files)
            {
             string fileNewName = await  FileRenameAsync(pathOrContainerName, file.FileName, HasFile);


              BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
               await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((fileNewName, pathOrContainerName));

            }
            return datas;
        }
    }
}
