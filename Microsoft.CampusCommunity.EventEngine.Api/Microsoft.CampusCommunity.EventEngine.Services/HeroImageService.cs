using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    public class HeroImageService: IHeroImageService
    {
        private readonly static string CONTAINERNAME = "heroimagesblob";
        private readonly static string DEFAULTHEROTEMPLATE = "defaultherotemplate";
        private BlobServiceClient _blobServiceClient;
        private BlobContainerClient _blobContainerClient;
        private readonly AzureStorageConfiguration _azureStorageConfiguration;

        public HeroImageService(IOptions<AzureStorageConfiguration> azureStorageConfiguration)
        {
            _azureStorageConfiguration = azureStorageConfiguration.Value;
            BuildAzureStorageClients();
        }

        /// <summary>
        /// Create a new Azure Storage Client based on configuration file.
        /// </summary>
        private void BuildAzureStorageClients()
        {
            // Create a BlobServiceClient object which will be used to create a container client
            _blobServiceClient = new BlobServiceClient(_azureStorageConfiguration.AzureStorageConnectionString);

            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(CONTAINERNAME);

            _blobContainerClient.CreateIfNotExists();
        }

        public async System.Threading.Tasks.Task<string> GetHeroImageForEventAsync(CommastoEvent commastoEvent)
        {
            string heroImageTemplate = await GetHeroImageTemplateAsync(commastoEvent.ThumbnailTemplateId);


        }

            public async System.Threading.Tasks.Task<string> GetHeroImageTemplateAsync(string templateId)
        {
           BlobClient blobClient = _blobContainerClient.GetBlobClient(templateId);
            if (!await blobClient.ExistsAsync())
            {
               
                blobClient = _blobContainerClient.GetBlobClient(DEFAULTHEROTEMPLATE);
                if(!await blobClient.ExistsAsync())
                {
                    throw new ArgumentException("no hero image found");
                }

            }
            BlobDownloadInfo downloadInfo = await blobClient.DownloadAsync();

            //TODO does automatic encoding detection work? Alternative: downloadInfo.Details.ContentEncoding
            using (var reader = new StreamReader(downloadInfo.Content, true))
            {
                return await reader.ReadToEndAsync();
            }

        }
    }
}
