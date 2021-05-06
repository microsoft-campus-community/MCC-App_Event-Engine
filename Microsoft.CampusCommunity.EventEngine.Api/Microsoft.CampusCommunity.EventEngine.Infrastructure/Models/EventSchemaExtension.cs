using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    public class EventSchemaExtension : IEventSchemaExtension
    {
        // You must serialize your property names to camelCase if your SchemaExtension describes as such.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Newtonsoft.Json.Required.Default)]
        public String Description { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "highlightQuote", Required = Newtonsoft.Json.Required.Default)]
        public String HighlightQuote { get; set; }


    }
}
