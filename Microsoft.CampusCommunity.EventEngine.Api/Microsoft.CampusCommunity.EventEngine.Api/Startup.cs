using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Services;
using System;
using System.Configuration;
using Azure.Storage.Blobs;

[assembly: FunctionsStartup(typeof(Microsoft.CampusCommunity.EventEngine.Api.Startup))]

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class Startup : FunctionsStartup
    {
        /*
         * All the configuration settings in host.json or local.settings.json to use for connecting to the graph are under this section name.
         */
        private const string GraphAuthenticationSettingsSectionName = "Graph";

        /*
         * All the configuration settings in host.json or local.settings.json to use for connecting to the Azure Storage are under this section name.
         */
        private const string AzureStorageConfigurationSectionName = "Storage";

        


        public override void Configure(IFunctionsHostBuilder builder)
        {

            configureGraph(builder);
            configureStorage(builder);

        }
        private void configureStorage(IFunctionsHostBuilder builder)
        {
            /*
            * Add the graph configuration as an Option so it can be injected with IOption.
            */
            builder.Services.AddOptions<AzureStorageConfiguration>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(AzureStorageConfigurationSectionName).Bind(settings);
                });

            builder.Services.AddSingleton<IHeroImageService, HeroImageService>();


        }


        private void configureGraph(IFunctionsHostBuilder builder)
        {
            /*
             * Add the graph configuration as an Option so it can be injected with IOption.
             */
            builder.Services.AddOptions<GraphClientConfiguration>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                   
                    //  configuration.Bind(settings);
                    configuration.GetSection(GraphAuthenticationSettingsSectionName).Bind(settings);
                });

            /*
             * Register all the services for dependency injection.
             */
            builder.Services.AddSingleton<IGraphService, GraphService>();
            builder.Services.AddSingleton<IGraphEventService, GraphEventService>();
            builder.Services.AddSingleton<IGraphEventServicePublic, GraphEventServicePublic>();
        }
    }
}
