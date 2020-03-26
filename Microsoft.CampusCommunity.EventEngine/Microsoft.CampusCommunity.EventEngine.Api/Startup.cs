using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Services;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class Startup : FunctionsStartup
    {
        private const string GraphAuthenticationSettingsSectionName = "Graph";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public override void Configure(IFunctionsHostBuilder builder)
        {

            configureGraph(builder);

        }
        
        private void configureGraph(IFunctionsHostBuilder builder)
        {
            IConfigurationSection graphConfigSection = Configuration.GetSection(GraphAuthenticationSettingsSectionName);
            GraphClientConfiguration graphConfig = graphConfigSection.Get<GraphClientConfiguration>();
            builder.Services.AddSingleton<GraphClientConfiguration>(graphConfig);


            builder.Services.AddScoped<IGraphService, GraphService>()
        }
    }
}
