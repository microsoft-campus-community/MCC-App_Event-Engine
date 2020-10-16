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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "readableId", Required = Newtonsoft.Json.Required.Default)]
        String ReadableId { get; set; }

        /// <summary>
        /// In which phase of planning is the project.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "planningStatus", Required = Newtonsoft.Json.Required.Default)]
        String PlanningStatus { get; set; }

        /// <summary>
        /// Image for display on website
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "titleImageUrl", Required = Newtonsoft.Json.Required.Default)]
        String TitleImageUrl { get; set; }

        /// <summary>
        /// To indicate if photos could be taken from the participant
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "requirePhotoAgreement", Required = Newtonsoft.Json.Required.Default)]
        String RequirePhotoAgreement { get; set; }

        /// <summary>
        /// Long-term field to handle event support requests automatically later on
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "supportRequestId", Required = Newtonsoft.Json.Required.Default)]
        String SupportRequestId { get; set; }
    }
}
