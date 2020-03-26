using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.CampusCommunity.EventEngine.Api.Helpers
{
    class AzureFunctionAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string _accessToken = string.Empty;
        public AzureFunctionAuthenticationProvider(string accessToken)
        {
            _accessToken = accessToken;
        }


        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            try
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            }
            catch (Exception)
            {

                throw;
            }

            
        }

    }
}
