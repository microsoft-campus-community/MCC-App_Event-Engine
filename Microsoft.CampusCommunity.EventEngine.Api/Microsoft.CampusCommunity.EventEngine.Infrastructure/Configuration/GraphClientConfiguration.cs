using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration to create a connection to MS Graph.
    /// For development this is best added as a "Graph" section in local.settings.json.
    /// <example>
    /// For example (local.settings.json):
    /// <code>
    /// {
    ///     "IsEncrypted": false,
    ///     "Values": {
    ///         "Graph:TenantId": [your tenant id],
    ///         "Graph:ClientId": [client id of your registered AAD application],
    ///         "Graph:AdminPrincipalUsername": [your username],
    ///         "Graph:AdminPrincipalPassword": [your password]
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class GraphClientConfiguration
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Authority => $"https://login.microsoftonline.com/{TenantId}/v2.0";
        public string AdminPrincipalPassword { get; set; }

        public string AdminPrincipalUsername { get; set; }

    }
}
