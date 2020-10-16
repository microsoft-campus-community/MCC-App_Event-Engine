using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
   public class PublicMCCEvent
    {
        public String TitleImageUrl { get; set; }
        //
        // Summary:
        //     Gets or sets id. Read-only.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }
        //
        // Summary:
        //     Gets or sets original start time zone. The start time zone that was set when
        //     the event was created. A value of tzone://Microsoft/Custom indicates that a legacy
        //     custom time zone was set in desktop Outlook.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "originalStartTimeZone", Required = Required.Default)]
        public string OriginalStartTimeZone { get; set; }        //
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
        //     Gets or sets start. The date, time, and time zone that the event starts. By default,
        //     the start time is in UTC.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "start", Required = Required.Default)]
        public DateTimeTimeZone Start { get; set; }
        //
        // Summary:
        //     Gets or sets subject. The text of the event's subject line.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "subject", Required = Required.Default)]
        public string Subject { get; set; }
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
        //     Gets or sets original start. The Timestamp type represents date and time information
        //     using ISO 8601 format and is always in UTC time. For example, midnight UTC on
        //     Jan 1, 2014 would look like this: '2014-01-01T00:00:00Z'
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "originalStart", Required = Required.Default)]
        public DateTimeOffset? OriginalStart { get; set; }
        //
        // Summary:
        //     Gets or sets original end time zone. The end time zone that was set when the
        //     event was created. A value of tzone://Microsoft/Custom indicates that a legacy
        //     custom time zone was set in desktop Outlook.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "originalEndTimeZone", Required = Required.Default)]
        public string OriginalEndTimeZone { get; set; }
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
        //     Gets or sets body preview. The preview of the message associated with the event.
        //     It is in text format.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "bodyPreview", Required = Required.Default)]
        public string BodyPreview { get; set; }
        //
        // Summary:
        //     Gets or sets end. The date, time, and time zone that the event ends. By default,
        //     the end time is in UTC.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "end", Required = Required.Default)]
        public DateTimeTimeZone End { get; set; }
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
        //     Gets or sets is online meeting. True if this event has online meeting information,
        //     false otherwise. Default is false. Optional.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isOnlineMeeting", Required = Required.Default)]
        public bool? IsOnlineMeeting { get; set; }
        //
        // Summary:
        //     Gets or sets location. The location of the event.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "location", Required = Required.Default)]
        public Location Location { get; set; }
        //
        // Summary:
        //     Gets or sets locations. The locations where the event is held or attended from.
        //     The location and locations properties always correspond with each other. If you
        //     update the location property, any prior locations in the locations collection
        //     would be removed and replaced by the new location value.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "locations", Required = Required.Default)]
        public IEnumerable<Location> Locations { get; set; }
        //
        // Summary:
        //     Gets or sets online meeting. Details for an attendee to join the meeting online.
        //     Read-only.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "onlineMeeting", Required = Required.Default)]
        public OnlineMeetingInfo OnlineMeeting { get; set; }
        //
        // Summary:
        //     Gets or sets is cancelled. Set to true if the event has been canceled.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isCancelled", Required = Required.Default)]
        public bool? IsCancelled { get; set; }

        public void fromMCCEvent(MCCEvent wrappedEvent)
        {
            if(wrappedEvent.EventSchemaExtensionData != null)
            {
                this.TitleImageUrl = wrappedEvent.EventSchemaExtensionData.TitleImageUrl;
            }
            this.Attachments = wrappedEvent.Attachments;
            this.Body = wrappedEvent.Body;
            this.BodyPreview = wrappedEvent.BodyPreview;
            this.End = wrappedEvent.End;
            this.HasAttachments = wrappedEvent.HasAttachments;
            this.ICalUId = wrappedEvent.ICalUId;
            this.Id = wrappedEvent.Id;
            this.Instances = wrappedEvent.Instances;
            this.IsAllDay = wrappedEvent.IsAllDay;
            this.IsCancelled = wrappedEvent.IsCancelled;
            this.Location = wrappedEvent.Location;
            this.Locations = wrappedEvent.Locations;
            this.OnlineMeetingUrl = wrappedEvent.OnlineMeetingUrl;
            this.Organizer = wrappedEvent.Organizer;
            this.OriginalEndTimeZone = wrappedEvent.OriginalEndTimeZone;
            this.OriginalStart = wrappedEvent.OriginalStart;
            this.OriginalStartTimeZone = wrappedEvent.OriginalStartTimeZone;
            this.Recurrence = wrappedEvent.Recurrence;
            this.SeriesMasterId = wrappedEvent.SeriesMasterId;
            this.Start = wrappedEvent.Start;
            this.Subject = wrappedEvent.Subject;
            this.Type = wrappedEvent.Type;
            this.WebLink = wrappedEvent.WebLink;

        }

    }
}
