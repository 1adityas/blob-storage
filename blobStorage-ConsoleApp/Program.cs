using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using Azure.Identity;

var connectionString = "DefaultEndpointsProtocol=https;AccountName=1blobstorage1;AccountKey=JrCOdb2s8BzzLryaR8i8Va6YF1YDPHToMfJtrPb1+R1gS7cECuNrunLCGy30NUUibb1j59rdNz4++ASt0h30sg==;EndpointSuffix=core.windows.net";

var blobServiceClient = new BlobServiceClient(connectionString);

//Create a unique name for the container
string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

// Create the container and return a container client object
BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

Console.WriteLine(containerClient);