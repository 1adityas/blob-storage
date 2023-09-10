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

AsyncContext.Run(blobCrudObject.UploadBlob);



