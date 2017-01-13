using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;


namespace Nancy.AzureFileUpload
{
    public class AzureFileStorage : IStorage
    {
        private CloudBlobContainer GetContainer()
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("myfiles");

            return container;
        }

        private CloudBlobContainer Container => GetContainer();
        public async Task UploadAsync(Stream fileStream, string filename)
        {


            await Container.CreateIfNotExistsAsync();
            var block = Container.GetBlockBlobReference(filename);

            await block.UploadFromStreamAsync(fileStream);
        }

        public async Task<IEnumerable<string>> GetFilesAsync()
        {
            BlobContinuationToken continuationToken = null;
            BlobResultSegment resultSegment = null;
            var result = new List<string>();
            do
            {
                resultSegment =  await Container.ListBlobsSegmentedAsync("", true, BlobListingDetails.All, 10, continuationToken, null, null);
                foreach (var blobItem in resultSegment.Results)
                {
                   result.Add(blobItem.StorageUri.PrimaryUri.ToString());
                }
                continuationToken = resultSegment.ContinuationToken;
            }
            while (continuationToken != null);
            return result;

        }

    }
}