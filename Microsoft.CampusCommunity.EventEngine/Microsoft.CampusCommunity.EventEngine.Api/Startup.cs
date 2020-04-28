using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Services;
using System;
using System.Configuration;

[assembly: FunctionsStartup(typeof(Microsoft.CampusCommunity.EventEngine.Api.Startup))]

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class Startup : FunctionsStartup
    {
        /*
         * All the configuration settings in host.json or local.settings.json to use for connecting to the graph are under this section name.
         */
        private const string GraphAuthenticationSettingsSectionName = "Graph";


        public override void Configure(IFunctionsHostBuilder builder)
        {

            configureGraph(builder);

        }
        
        private void configureGraph(IFunctionsHostBuilder builder)
        {
            /*
             * Add the graph configuration as an Option so it can be injected with IOption.
             */
            builder.Services.AddOptions<GraphClientConfiguration>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(GraphAuthenticationSettingsSectionName).Bind(settings);
                });

            /*
             * Register all the services for dependency injection.
             */
            builder.Services.AddSingleton<IGraphService, GraphService>();
            builder.Services.AddSingleton<IGraphEventService, GraphEventService>();
        }
    }
}
