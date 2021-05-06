using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    /// <summary>
    /// A MS Graph schema extension to store custom MCC event data.
    /// <see href="https://docs.microsoft.com/en-us/graph/extensibility-schema-groups">Microsoft Graph Schema Extension</see>
    /// </summary>
    public interface IEventSchemaExtension
    {
        // You must serialize your property names to camelCase if your SchemaExtension describes as such.
        /// <summary>
        /// A friendly id to refrence the event by humans.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Newtonsoft.Json.Required.Default)]
        public String Description { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "highlightQuote", Required = Newtonsoft.Json.Required.Default)]
        public String HighlightQuote { get; set; }
    }
}
