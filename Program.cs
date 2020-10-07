using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Threading;
using System.IO;


namespace Blob_Storage
{
    class Program
    {
        static string ConnectionString = "---"; // enter connection string here 
        static string ContainerName = "codecontainer";
        static string FileName = "blob file.txt";
        static string FilePath = "C:\\Users\\tanis\\Desktop\\blob file.txt";
        static string Downloadpath = "C:\\Users\\tanis\\Desktop\\blob file.txt";

        static async Task Main(string[] args)
        {
            Container().Wait();
            CreateBlob().Wait();
            GetBlobs().Wait();
            Console.WriteLine("Completed");
            Console.ReadKey();
        }

        static async Task Container()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
            BlobContainerClient blobContainerClient =  await blobServiceClient.CreateBlobContainerAsync(ContainerName);

        }
        static async Task CreateBlob()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            BlobClient blobClient = blobContainerClient.GetBlobClient(FileName);
            using FileStream uploadFileSteam = File.OpenRead(FilePath);

            await blobClient.UploadAsync(uploadFileSteam, true);
            uploadFileSteam.Close();

        }


        static async Task GetBlobs()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            await foreach(BlobItem blobItem in blobContainerClient.GetBlobsAsync())
            {
                Console.WriteLine(blobItem.Name + "\n");
            }
        }
    }
}
