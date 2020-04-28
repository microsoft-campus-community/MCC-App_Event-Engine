using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Helpers
{
    public class AzureFunctionAuthenticationProviderToken : IAuthenticationProvider
    {
        private readonly string _accessToken = string.Empty;
        //Does not work for local development: https://github.com/Azure/azure-functions-microsoftgraph-extension/issues/76

        public AzureFunctionAuthenticationProviderToken(string graphToken)
        {
            _accessToken = graphToken;

        }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

        }
    }
}
