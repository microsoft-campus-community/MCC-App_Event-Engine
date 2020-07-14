using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    /// <summary>
    /// The <c>MCCEvent</c> class extends <c cref="Microsoft.Graph.Event">Event</c> to add the Microsoft Campus Community specific event data.
    /// </summary>
    public partial class MCCEvent : Event
    {
        /// <summary>
        /// <value>If set to true the MCC event specific data will be serialized. Otherwise the specific data will not be visible when converting this object to JSON.</value>
        /// </summary>
        [JsonIgnore]
        public bool SerializeMccEventSpecificData {get; set;} = true;

        /// <summary>
        /// <value>If set to true the internal event schema extension object will be serialized. Otherwise the specific data will not be visible when converting this object to JSON.</value>
        /// </summary>
        [JsonIgnore]
        public bool SerializeEventSchemaExtension { get; set; } = false;


        /// <summary>
        /// Don't serialize the <c>extvmri0qlh_eventEngine<c> data property if the event is to be exposed to the event engine api.
        /// Because it is intended for the MS Graph api.
        /// </summary>
        /// <returns>Whether or not to serialize <c>EventSchemaExtensionData</c></returns>
        public bool ShouldSerializeEventSchemaExtensionData()
        {
           return SerializeEventSchemaExtension;
        }
        /// <summary>
        /// <value>Represents the MCC specific event data ready to be send to the MS Graph api.</value>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "extvmri0qlh_eventEngine", Required = Newtonsoft.Json.Required.Default)]
        public EventSchemaExtension EventSchemaExtensionData { get; set; }

        /// <summary>
        /// Don't serialize the <c>ReadableId</c> property if the event is to be exposed to the MS Graph api.
        /// </summary>
        /// <returns>Whether or not to serialize <c>ReadableId</c></returns>
        public bool ShouldSerializeReadableId()
        {
            return SerializeMccEventSpecificData;
        }
        /// <summary>
        /// Don't serialize the <c>PlanningStatus</c> property if the event is to be exposed to the MS Graph api.
        /// </summary>
        /// <returns>Whether or not to serialize <c>PlanningStatus</c></returns>
        public bool ShouldSerializePlanningStatus()
        {
            return SerializeMccEventSpecificData;
        }
        /// <summary>
        /// Don't serialize the <c>TitleImageUrl</c> property if the event is to be exposed to the MS Graph api.
        /// </summary>
        /// <returns>Whether or not to serialize <c>TitleImageUrl</c></returns>
        public bool ShouldSerializeTitleImageUrl()
        {
            return SerializeMccEventSpecificData;
        }
        /// <summary>
        /// Don't serialize the <c>RequirePhotoAgreement</c> property if the event is to be exposed to the MS Graph api.
        /// </summary>
        /// <returns>Whether or not to serialize <c>RequirePhotoAgreement</c></returns>
        public bool ShouldSerializeRequirePhotoAgreement()
        {
            return SerializeMccEventSpecificData;
        }
        /// <summary>
        /// Don't serialize the <c>SupportRequestId</c> property if the event is to be exposed to the MS Graph api.
        /// </summary>
        /// <returns>Whether or not to serialize <c>SupportRequestId</c></returns>
        public bool ShouldSerializeSupportRequestId()
        {
            return SerializeMccEventSpecificData;
        }

        /// <summary>
        /// A human readable id to reference the event.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "readableId", Required = Newtonsoft.Json.Required.Default)]
        String ReadableId {
            get => EventSchemaExtensionData?.ReadableId;
            set {
                if (EventSchemaExtensionData == null)
                {
                    EventSchemaExtensionData = new EventSchemaExtension();
                }
                EventSchemaExtensionData.ReadableId = value;
            }  
        }
        /// <summary>
        /// The stage of planning the event is in.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "planningStatus", Required = Newtonsoft.Json.Required.Default)]
        String PlanningStatus { 
            get => EventSchemaExtensionData?.PlanningStatus;
            set
            {
                if (EventSchemaExtensionData == null)
                {
                    EventSchemaExtensionData = new EventSchemaExtension();
                }
                EventSchemaExtensionData.PlanningStatus = value;
            }
        }
        /// <summary>
        /// A url to an image to display for this event.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "titleImageUrl", Required = Newtonsoft.Json.Required.Default)]
        String TitleImageUrl { 
            get => EventSchemaExtensionData?.TitleImageUrl;
            set
            {
                if (EventSchemaExtensionData == null)
                {
                    EventSchemaExtensionData = new EventSchemaExtension();
                }
                EventSchemaExtensionData.TitleImageUrl = value;
            }
        }
        /// <summary>
        /// Whether or not attendees need to agree to having photos taken.
        /// </summary>
        /// <value>If <c>true</c> a photo agreement is required. A photo agreement is not required if <c>false</c>.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "requirePhotoAgreement", Required = Newtonsoft.Json.Required.Default)]
        Boolean RequirePhotoAgreement {
            get {
                Boolean result = false;
                Boolean.TryParse(EventSchemaExtensionData?.RequirePhotoAgreement, out result);
                return result;
            }
            set
            {
                if (EventSchemaExtensionData == null)
                {
                    EventSchemaExtensionData = new EventSchemaExtension();
                }
                EventSchemaExtensionData.RequirePhotoAgreement = value.ToString();
            }
        }
        /// <summary>
        /// An id to handle support requests in the future.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "supportRequestId", Required = Newtonsoft.Json.Required.Default)]
        String SupportRequestId { 
            get => EventSchemaExtensionData?.SupportRequestId;
            set
            {
                if (EventSchemaExtensionData == null)
                {
                    EventSchemaExtensionData = new EventSchemaExtension();
                }
                EventSchemaExtensionData.SupportRequestId = value;
            }
        }

    

        public MCCEvent(): base()
        {
        }

        /// <summary>
        /// Creates a new <c>MCCEvent</c> derived from a MS Graph <c>Event</c>.
        /// </summary>
        /// <param name="wrappedEvent">The Graph event to base the new MCCEvent of.</param>
        public MCCEvent(Event wrappedEvent)
        {
            fromEvent(wrappedEvent);
        }

        /// <summary>
        /// Turns this MCCEvent into a MS Graph event without additional data.
        /// </summary>
        /// <returns>A new <c>Event</c> based on this MCCEvent.</returns>
        public Event toEvent()
        {
            return new Event()
            {
                AdditionalData = this.AdditionalData,
                Attachments = this.Attachments,
                Attendees = this.Attendees,
                Body = this.Body,
                BodyPreview = this.BodyPreview,
                Calendar = this.Calendar,
                Categories = this.Categories,
                ChangeKey = this.ChangeKey,
                CreatedDateTime = this.CreatedDateTime,
                End = this.End,
                Extensions = this.Extensions,
                HasAttachments = this.HasAttachments,
                ICalUId = this.ICalUId,
                Id = this.Id,
                Importance = this.Importance,
                Instances = this.Instances,
                IsAllDay = this.IsAllDay,
                IsCancelled = this.IsCancelled,
                IsOrganizer = this.IsOrganizer,
                IsReminderOn = this.IsReminderOn,
                LastModifiedDateTime = this.LastModifiedDateTime,
                Location = this.Location,
                Locations = this.Locations,
                MultiValueExtendedProperties = this.MultiValueExtendedProperties,
                ODataType = this.ODataType,
                OnlineMeetingUrl = this.OnlineMeetingUrl,
                Organizer = this.Organizer,
                OriginalEndTimeZone = this.OriginalEndTimeZone,
                OriginalStart = this.OriginalStart,
                OriginalStartTimeZone = this.OriginalStartTimeZone,
                Recurrence = this.Recurrence,
                ReminderMinutesBeforeStart = this.ReminderMinutesBeforeStart,
                ResponseRequested = this.ResponseRequested,
                ResponseStatus = this.ResponseStatus,
                Sensitivity = this.Sensitivity,
                SeriesMasterId = this.SeriesMasterId,
                ShowAs = this.ShowAs,
                SingleValueExtendedProperties = this.SingleValueExtendedProperties,
                Start = this.Start,
                Subject = this.Subject,
                Type = this.Type,
                WebLink = this.WebLink
            };

        }

        /// <summary>
        /// Takes over the data of the provided Event to this MCCEvent.
        /// </summary>
        /// <param name="wrappedEvent">To read all data and copy to this MCCEvent.</param>
        public void fromEvent(Event wrappedEvent)
        {
            if(wrappedEvent.AdditionalData != null)
            {
                Object additionalDataEvent = null;
                if (wrappedEvent.AdditionalData.TryGetValue("extvmri0qlh_eventEngine", out additionalDataEvent))
                {
                    wrappedEvent.AdditionalData.Remove("extvmri0qlh_eventEngine");
                    if(additionalDataEvent is JObject)
                    {
                        this.EventSchemaExtensionData = ((JObject)additionalDataEvent).ToObject<EventSchemaExtension>();
                    }
                }
                else
                {
                    this.EventSchemaExtensionData = null;
                }
            }
            


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
