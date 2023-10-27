using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobHelper1
{
    internal interface IBlobCrud
    {
        BlobServiceClient blobServiceClient { get; set; }

        //void blob_crud(string connectionString);  
        void NewContainer(string containerName);
        void UploadBlob(string containerName, string filePath);
        Pageable<BlobItem> ListBlob(string containerName);
        Pageable<BlobContainerItem> ListContainers();
        void DeleteBlob(string containerName, string deleteBlobName);
        void DeleteContainer(string containerName);
    }
}
