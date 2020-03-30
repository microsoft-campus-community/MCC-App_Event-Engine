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
        private const string GraphAuthenticationSettingsSectionName = "Graph";

       /* public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }*/

        public override void Configure(IFunctionsHostBuilder builder)
        {

            configureGraph(builder);

        }
        
        private void configureGraph(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<GraphClientConfiguration>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(GraphAuthenticationSettingsSectionName).Bind(settings);
                });

          /*  IConfigurationSection graphConfigSection = (IConfigurationSection) ConfigurationManager.GetSection(GraphAuthenticationSettingsSectionName);
            GraphClientConfiguration graphConfig = graphConfigSection.Get<GraphClientConfiguration>();
            builder.Services.AddSingleton<GraphClientConfiguration>(graphConfig);*/

            builder.Services.AddSingleton<IGraphService, GraphService>();
            builder.Services.AddSingleton<IGraphEventService, GraphEventService>();
            //builder.Services.AddScoped<IGraphService, GraphService>();
        }
    }
}
