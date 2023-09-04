﻿using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blobStorage_ConsoleApp
{
    internal class blob_crud
    {

        private string connectionString { get; set; }
        public blob_crud(string connectionString)
        {
            this.connectionString=connectionString;

        }




    }

    var blobServiceClient = new BlobServiceClient(connectionString);

    //Create a unique name for the container
    string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

    // Create the container and return a container client object
    BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
    //Console.WriteLine(containerClient);

    // Create a local file in the ./data/ directory for uploading and downloading
    string localPath = "data";
    Directory.CreateDirectory(localPath);
string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
    string localFilePath = Path.Combine(localPath, fileName);

    // Write text to the file
    await File.WriteAllTextAsync(localFilePath, "Hello, World!");

    // Get a reference to a blob
    BlobClient blobClient = containerClient.GetBlobClient(fileName);

    Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

// Upload data from the local file
await blobClient.UploadAsync(localFilePath, true);
}
