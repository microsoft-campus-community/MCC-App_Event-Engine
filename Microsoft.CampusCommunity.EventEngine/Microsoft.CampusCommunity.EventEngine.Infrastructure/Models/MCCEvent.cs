using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Graph;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    public class MCCEvent : Event
    {

        public IEventSchemaExtension mccEventData { get; set; }

        private Event wrappedEvent;

        public MCCEvent(Event wrappedEvent)
        {
            Object additionalDataEvent = null;
            if (wrappedEvent.AdditionalData.TryGetValue("extvmri0qlh_eventEngine", out additionalDataEvent))
            {
                this.mccEventData = (IEventSchemaExtension) additionalDataEvent;
            } else
            {
                this.mccEventData = null;
            }

            this.wrappedEvent = wrappedEvent;
            this.AdditionalData = wrappedEvent.AdditionalData;
            this.Attachments = wrappedEvent.Attachments;
            this.Attendees = wrappedEvent.Attendees;
            this.Body = wrappedEvent.Body;
            this.BodyPreview = wrappedEvent.BodyPreview;
            this.Calendar = wrappedEvent.Calendar;
            this.Categories = wrappedEvent.Categories;
            this.ChangeKey = wrappedEvent.ChangeKey;
            this.CreatedDateTime = wrappedEvent.CreatedDateTime;
            this.End = wrappedEvent.End;
            this.Extensions = wrappedEvent.Extensions;
            this.HasAttachments = wrappedEvent.HasAttachments;
            this.ICalUId = wrappedEvent.ICalUId;
            this.Id = wrappedEvent.Id;
            this.Importance = wrappedEvent.Importance;
            this.Instances = wrappedEvent.Instances;
            this.IsAllDay = wrappedEvent.IsAllDay;
            this.IsCancelled = wrappedEvent.IsCancelled;
            this.IsOrganizer = wrappedEvent.IsOrganizer;
            this.IsReminderOn = wrappedEvent.IsReminderOn;
            this.LastModifiedDateTime = wrappedEvent.LastModifiedDateTime;
            this.Location = wrappedEvent.Location;
            this.Locations = wrappedEvent.Locations;
            this.MultiValueExtendedProperties = wrappedEvent.MultiValueExtendedProperties;
            this.ODataType = wrappedEvent.ODataType;
            this.OnlineMeetingUrl = wrappedEvent.OnlineMeetingUrl;
            this.Organizer = wrappedEvent.Organizer;
            this.OriginalEndTimeZone = wrappedEvent.OriginalEndTimeZone;
            this.OriginalStart = wrappedEvent.OriginalStart;
            this.OriginalStartTimeZone = wrappedEvent.OriginalStartTimeZone;
            this.Recurrence = wrappedEvent.Recurrence;
            this.ReminderMinutesBeforeStart = wrappedEvent.ReminderMinutesBeforeStart;
            this.ResponseRequested = wrappedEvent.ResponseRequested;
            this.ResponseStatus = wrappedEvent.ResponseStatus;
            this.Sensitivity = wrappedEvent.Sensitivity;
            this.SeriesMasterId = wrappedEvent.SeriesMasterId;
            this.ShowAs = wrappedEvent.ShowAs;
            this.SingleValueExtendedProperties = wrappedEvent.SingleValueExtendedProperties;
            this.Start = wrappedEvent.Start;
            this.Subject = wrappedEvent.Subject;
            this.Type = wrappedEvent.Type;
            this.WebLink = wrappedEvent.WebLink;
        }
    }
}
