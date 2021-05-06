using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
   public class PublicMCCEvent : PublicMCCEventLite
    {
        public String TitleImageUrl { get; set; }
        //
        // Summary:
        //     Gets or sets recurrence. The recurrence pattern for the event.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "recurrence", Required = Required.Default)]
        public PatternedRecurrence Recurrence { get; set; }
        //
        // Summary:
        //     Gets or sets series master id. The ID for the recurring series master item, if
        //     this event is part of a recurring series.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "seriesMasterId", Required = Required.Default)]
        public string SeriesMasterId { get; set; }
       
        //
        // Summary:
        //     Gets or sets type. The event type. The possible values are: singleInstance, occurrence,
        //     exception, seriesMaster. Read-only.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public EventType? Type { get; set; }
        //
        // Summary:
        //     Gets or sets web link. The URL to open the event in Outlook on the web.Outlook
        //     on the web opens the event in the browser if you are signed in to your mailbox.
        //     Otherwise, Outlook on the web prompts you to sign in.This URL can be accessed
        //     from within an iFrame.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "webLink", Required = Required.Default)]
        public string WebLink { get; set; }
        //
        // Summary:
        //     Gets or sets attachments. The collection of fileAttachment and itemAttachment
        //     attachments for the event. Navigation property. Read-only. Nullable.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "attachments", Required = Required.Default)]
        public IEventAttachmentsCollectionPage Attachments { get; set; }
        //
        // Summary:
        //     Gets or sets instances. The instances of the event. Navigation property. Read-only.
        //     Nullable.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "instances", Required = Required.Default)]
        public IEventInstancesCollectionPage Instances { get; set; }
        
        //
        // Summary:
        //     Gets or sets organizer. The organizer of the event.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "organizer", Required = Required.Default)]
        public Recipient Organizer { get; set; }
        //
        // Summary:
        //     Gets or sets online meeting url. A URL for an online meeting. The property is
        //     set only when an organizer specifies an event as an online meeting such as a
        //     Skype meeting. Read-only.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "onlineMeetingUrl", Required = Required.Default)]
        public string OnlineMeetingUrl { get; set; }
        //
        // Summary:
        //     Gets or sets body. The body of the message associated with the event. It can
        //     be in HTML or text format.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "body", Required = Required.Default)]
        public ItemBody Body { get; set; }
        
       
        //
        // Summary:
        //     Gets or sets has attachments. Set to true if the event has attachments.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "hasAttachments", Required = Required.Default)]
        public bool? HasAttachments { get; set; }
        //
        // Summary:
        //     Gets or sets i cal uid. A unique identifier for an event across calendars. This
        //     ID is different for each occurrence in a recurring series. Read-only.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "iCalUId", Required = Required.Default)]
        public string ICalUId { get; set; }
        //
        // Summary:
        //     Gets or sets is all day. Set to true if the event lasts all day.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isAllDay", Required = Required.Default)]
        public bool? IsAllDay { get; set; }
       
        
        //
        // Summary:
        //     Gets or sets online meeting. Details for an attendee to join the meeting online.
        //     Read-only.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "onlineMeeting", Required = Required.Default)]
        public OnlineMeetingInfo OnlineMeeting { get; set; }
       
        
        override public void fromGraphEvent(Graph.Event wrappedEvent)
        {
            base.fromGraphEvent(wrappedEvent);
            if (wrappedEvent.AdditionalData != null)
            {
                Object additionalDataEvent = null;
                if (wrappedEvent.AdditionalData.TryGetValue(IGraphEventService.EVENTSCHEMAEXTENSIONID, out additionalDataEvent))
                {
                    wrappedEvent.AdditionalData.Remove(IGraphEventService.EVENTSCHEMAEXTENSIONID);
                   /* if (additionalDataEvent is JObject)
                    {
                        TitleImageUrl = ((JObject)additionalDataEvent).ToObject<EventSchemaExtension>().TitleImageUrl;
                    }*/
                }
            }

           
            this.Attachments = wrappedEvent.Attachments;
            this.Body = wrappedEvent.Body;
            this.HasAttachments = wrappedEvent.HasAttachments;
            this.ICalUId = wrappedEvent.ICalUId;
            this.Instances = wrappedEvent.Instances;
            this.IsAllDay = wrappedEvent.IsAllDay;
            this.OnlineMeetingUrl = wrappedEvent.OnlineMeetingUrl;
            this.Organizer = wrappedEvent.Organizer;
            this.Recurrence = wrappedEvent.Recurrence;
            this.SeriesMasterId = wrappedEvent.SeriesMasterId;
            this.Type = wrappedEvent.Type;
            this.WebLink = wrappedEvent.WebLink;

        }

    }
}
