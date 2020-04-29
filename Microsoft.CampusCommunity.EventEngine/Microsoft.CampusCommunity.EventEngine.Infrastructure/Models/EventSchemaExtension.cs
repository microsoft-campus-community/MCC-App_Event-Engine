using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    public class EventSchemaExtension : IEventSchemaExtension
    {
        // You must serialize your property names to camelCase if your SchemaExtension describes as such.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "readableId", Required = Newtonsoft.Json.Required.Default)]
        public String ReadableId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "planningStatus", Required = Newtonsoft.Json.Required.Default)]
        public String PlanningStatus { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "titleImageUrl", Required = Newtonsoft.Json.Required.Default)]
        public String TitleImageUrl { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "requirePhotoAgreement", Required = Newtonsoft.Json.Required.Default)]
        public String RequirePhotoAgreement { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "supportRequestId", Required = Newtonsoft.Json.Required.Default)]
        public String SupportRequestId { get; set; }

    }
}
