using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using Azure.Identity;
using blobStorage_ConsoleApp;
using Nito.AsyncEx;

string connectionString = constants.connectionString;
var blobCrudObject = new blob_crud(connectionString);

//to create a new container

//AsyncContext.Run(blobCrudObject.NewContainer);

//blobCrudObject.UploadBlob("newcontainer-1");
blobCrudObject.ListContainers();
blobCrudObject.DeleteContainer("quickstartblobs14f8f1ef-7303-4b78-9f22-138d6ee63612");

//AsyncContext.Run(blobCrudObject.UploadBlob); <-- when uploadBlob had no parameters




