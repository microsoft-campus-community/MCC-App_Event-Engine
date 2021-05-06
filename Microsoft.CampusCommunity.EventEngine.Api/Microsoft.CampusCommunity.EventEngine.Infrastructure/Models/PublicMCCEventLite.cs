using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    public class PublicMCCEventLite
    {
        //
        // Summary:
        //     Gets or sets subject. The text of the event's subject line.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "subject", Required = Required.Default)]
        public string Subject { get; set; }
        //
        // Summary:
        //     Gets or sets id. Read-only.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }
        //
        // Summary:
        //     Gets or sets is cancelled. Set to true if the event has been canceled.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isCancelled", Required = Required.Default)]
        public bool? IsCancelled { get; set; }
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
        //     Gets or sets body preview. The preview of the message associated with the event.
        //     It is in text format.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "bodyPreview", Required = Required.Default)]
        public string BodyPreview { get; set; }

        //
        // Summary:
        //     Gets or sets original start time zone. The start time zone that was set when
        //     the event was created. A value of tzone://Microsoft/Custom indicates that a legacy
        //     custom time zone was set in desktop Outlook.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "originalStartTimeZone", Required = Required.Default)]
        public string OriginalStartTimeZone { get; set; }

        //
        // Summary:
        //     Gets or sets start. The date, time, and time zone that the event starts. By default,
        //     the start time is in UTC.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "start", Required = Required.Default)]
        public DateTimeTimeZone Start { get; set; }
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
        //     Gets or sets end. The date, time, and time zone that the event ends. By default,
        //     the end time is in UTC.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "end", Required = Required.Default)]
        public DateTimeTimeZone End { get; set; }

        virtual public void fromGraphEvent(Graph.Event wrappedEvent)
        {
            this.BodyPreview = wrappedEvent.BodyPreview;
            this.Id = wrappedEvent.Id;
            this.IsCancelled = wrappedEvent.IsCancelled;
            this.Location = wrappedEvent.Location;
            this.Locations = wrappedEvent.Locations;
            this.IsOnlineMeeting = wrappedEvent.IsOnlineMeeting;
            this.Subject = wrappedEvent.Subject;
            this.BodyPreview = wrappedEvent.BodyPreview;
            this.End = wrappedEvent.End;
            this.Id = wrappedEvent.Id;
            this.IsCancelled = wrappedEvent.IsCancelled;
            this.Location = wrappedEvent.Location;
            this.Locations = wrappedEvent.Locations;
            this.OriginalEndTimeZone = wrappedEvent.OriginalEndTimeZone;
            this.OriginalStart = wrappedEvent.OriginalStart;
            this.OriginalStartTimeZone = wrappedEvent.OriginalStartTimeZone;
            this.Start = wrappedEvent.Start;
        }
    }


}
