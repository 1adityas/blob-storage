using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using Azure.Identity;
using blobStorage_ConsoleApp;
using Nito.AsyncEx;
using BlobHelper1;
internal class Program
{
    private static void Main(string[] args)
    {
        string filePath = "C:\\Users\\adity\\Downloads\\Telegram Desktop\\Aditya- Resume(0).docx";
        string containerName = "new-container";
        string connectionString = constants.connectionString;
        var blobCrudObject = new BlobHelper(connectionString);
        //to create a new container

        //blobCrudObject.NewContainer(containerName);

        //blobCrudObject.UploadBlob(containerName, filePath);
        blobCrudObject.ListContainers();
        blobCrudObject.interContainers("newcontainer-1", "dogFuny.png", "new-container");

        //blobCrudObject.ListBlob("newcontainer-1");


        //blobCrudObject.DeleteContainer("quickstartblobs14f8f1ef-7303-4b78-9f22-138d6ee63612");

        //AsyncContext.Run(blobCrudObject.UploadBlob); 
        //< --when uploadBlob had no parameters
    }
}





