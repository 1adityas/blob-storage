using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Storage;
using System.IO;
using System.Runtime.CompilerServices;

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

            //string accountName = "1blobstorage1";
            //string accountKey = "";
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=" + accountName + ";AccountKey=" + accountKey;
            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);


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

        public Pageable<BlobItem> ListBlob(string containerName)
        {
            Console.WriteLine("Listing blobs...");

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);// why we cant use async when using method

            // List all blobs in the container
            var blobItems = containerClient.GetBlobs();
            BlobItem lastBlobItem;
            foreach (BlobItem blobItem in blobItems)
            {
                Console.WriteLine("\t" + blobItem.Name);
                lastBlobItem = blobItem;
            }
            return blobItems;
        }

        public void DownloadBlob(string containerName, string blobName)
        {
            //var blobName = "dogFuny.png";
            var blobClient = new BlobClient(this._connectionString, containerName, blobName);
            var newFilePath = "C:\\Users\\adity\\Downloads\\" + blobName;
            //var localFilePath = "C:\\Users\\adity\\Downloads";
            FileStream fileStream = File.OpenWrite(newFilePath);
            blobClient.DownloadTo(fileStream);
            fileStream.Close();
            //blobClient.DownloadTo("D:\\download_d");
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
        /// <summary>
        /// to transfer blob in between containers
        /// </summary>
        /// <param name="srcContainerName"></param>
        /// <param name="blobName"></param>
        /// <param name="destContainerName"></param>
        public void interContainers(string srcContainerName, string blobName, string destContainerName)
        {
            var newFilePath = "C:\\Users\\adity\\Downloads\\" + blobName;
            //this.DownloadBlob(blobName="");
            var blobClient = new BlobClient(this._connectionString, srcContainerName, blobName);
            var memStream = new MemoryStream();
            //this.UploadBlob(destContainerName, newFilePath);
            var blobContent = blobClient.DownloadContent().Value.Content;

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(destContainerName);// why we cant use async when using 
            this.DeleteBlob(srcContainerName, blobName);
            blobClient = containerClient.GetBlobClient(blobName);
            blobClient.Upload(blobContent, true);
        }

        public void inter

        public async void DeleteBlob(string containerName, string blobName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);// why we cant use async when using method
            await containerClient.GetBlobClient(blobName).DeleteAsync();

        }

        public void DeleteContainer(string containerName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);// why we cant use async when using method
            containerClient.Delete();

        }
    }
}
