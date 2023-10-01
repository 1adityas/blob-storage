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
        string connectionString = constants.connectionString;
        var blobCrudObject = new BlobHelper("");
    }
}

//to create a new container

//AsyncContext.Run(blobCrudObject.NewContainer);

//blobCrudObject.UploadBlob("newcontainer-1");
//blobCrudObject.ListContainers();
//blobCrudObject.DeleteContainer("quickstartblobs14f8f1ef-7303-4b78-9f22-138d6ee63612");

//AsyncContext.Run(blobCrudObject.UploadBlob); <-- when uploadBlob had no parameters




