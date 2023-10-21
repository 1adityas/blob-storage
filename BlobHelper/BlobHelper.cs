using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BlobHelper1
{
    public class BlobHelper : IBlobCrud
    {
        private string _connectionString { get; set; }
        public BlobServiceClient blobServiceClient { get; set; }
        public BlobHelper(string connectionString)
        {
            this._connectionString = connectionString;
            this.blobServiceClient = new BlobServiceClient(connectionString);

        }

        public async void NewContainer(string containerName)
        {

            //Create a unique name for the container
            if (string.IsNullOrEmpty(containerName))
                containerName = "quickstartblobs" + Guid.NewGuid().ToString();
            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            Console.WriteLine("new container with name", containerName);
        }
        public async void UploadBlob(string containerName, string filePath)
        {
            // Create a local file in the ./data/ directory for uploading and downloading
            //string localPath = "/data";
            //Directory.CreateDirectory(localPath);
            //string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
            string fileName = "quickstart" + Guid.NewGuid().ToString();
            //string localFilePath = Path.Combine(localPath, fileName);
            // Write text to the file
            //await File.WriteAllTextAsync(localFilePath, "Hello, World!");

            // Get a reference to a blob
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);// why we cant use async when using method


            BlobClient blobClient = containerClient.GetBlobClient(fileName + ".docx");
            await blobClient.UploadAsync(filePath, true);
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
