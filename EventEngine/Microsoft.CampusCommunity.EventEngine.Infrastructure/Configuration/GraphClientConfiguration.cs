﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration
{
    public class GraphClientConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string Authority => $"https://login.microsoftonline.com/{TenantId}/v2.0";
        public string Domain { get; set; }
        public string Scope => $"https://{Domain}/Api/.default"; //https://campus-community.org/Api
    }
}
