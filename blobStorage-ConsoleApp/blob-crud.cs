using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace blobStorage_ConsoleApp
{
    internal class blob_crud : IBlobCrud
    {

        private string connectionString { get; set; }
        private BlobServiceClient blobServiceClient { get; set; }
        public blob_crud(string connectionString)
        {
            this.connectionString = connectionString;
            this.blobServiceClient = new BlobServiceClient(connectionString);

        }

        public async void NewContainer()
        {

            //Create a unique name for the container
            string containerName = "quickstartblobs" + Guid.NewGuid().ToString();
            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            Console.WriteLine(containerClient);
        }
        public async void UploadBlob(string containerName)
        {
            // Create a local file in the ./data/ directory for uploading and downloading
            string localPath = "/data";
            Directory.CreateDirectory(localPath);
            string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);
            // Write text to the file
            await File.WriteAllTextAsync(localFilePath, "Hello, World!");

            // Get a reference to a blob
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);// why we cant use async when using method

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(localFilePath, true);
        }

        public async Task<AsyncPageable<BlobItem>> ListBlob(string containerName)
        {
            Console.WriteLine("Listing blobs...");

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);// why we cant use async when using method

            // List all blobs in the container
            var blobItems = containerClient.GetBlobsAsync();
            BlobItem lastBlobItem;
            await foreach (BlobItem blobItem in blobItems)
            {
                Console.WriteLine("\t" + blobItem.Name);
                lastBlobItem = blobItem;

            }
            return blobItems;
        }
        public Pageable<BlobContainerItem> ListContainers()
        {
            Console.WriteLine("Listing containers...");

            var BlobItems = blobServiceClient.GetBlobContainers();
            foreach (var blobItem in BlobItems)
            {
                Console.WriteLine(blobItem.Name);
            }
            Console.WriteLine(BlobItems.Count());

            return BlobItems;
        }

        public async void DeleteBlob(string containerName, string deleteBlobName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);// why we cant use async when using method

            await containerClient.GetBlobClient(deleteBlobName).DeleteAsync();

        }

        public void DeleteContainer(string containerName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);// why we cant use async when using method
            containerClient.Delete();

        }
    }
}
