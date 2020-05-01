using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    public partial class MCCEvent : Event
    {

        [JsonIgnore]
        public bool SerializeMccEventSpecificData {get; set;} = true;

        [JsonIgnore]
        public bool SerializeEventSchemaExtension { get; set; } = false;


        public bool ShouldSerializeEventSchemaExtensionData()
        {
            // don't serialize the Extvmri0qlh_eventEngine data property if the event is to be exposed to the Graph
           return SerializeEventSchemaExtension;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "extvmri0qlh_eventEngine", Required = Newtonsoft.Json.Required.Default)]
        public EventSchemaExtension EventSchemaExtensionData { get; set; }

        public bool ShouldSerializeReadableId()
        {

            // don't serialize the additional data property if the event is to be exposed to the API
            return SerializeMccEventSpecificData;
        }
        public bool ShouldSerializePlanningStatus()
        {
            // don't serialize the additional data property if the event is to be exposed to the API
            return SerializeMccEventSpecificData;
        }
        public bool ShouldSerializeTitleImageUrl()
        {
            // don't serialize the additional data property if the event is to be exposed to the API
            return SerializeMccEventSpecificData;
        }
        public bool ShouldSerializeRequirePhotoAgreement()
        {
            // don't serialize the additional data property if the event is to be exposed to the API
            return SerializeMccEventSpecificData;
        }
        public bool ShouldSerializeSupportRequestId()
        {
            // don't serialize the additional data property if the event is to be exposed to the API
            return SerializeMccEventSpecificData;
        }

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

        


      /*  public new IDictionary<string, object> AdditionalData { 
            get
            {
                if (ShouldSerializeAdditionalData())
                {
                    if (EventSchemaExtensionData != null)
                    {
                        if (base.AdditionalData == null)
                        {
                            base.AdditionalData = new Dictionary<string, object>();
                        }

                        base.AdditionalData["extvmri0qlh_eventEngine"] = EventSchemaExtensionData;

                    }
                    return base.AdditionalData;
                } else
                {
                    return null;
                }
                
            }
            set => base.AdditionalData = value;
        }*/

        public MCCEvent(): base()
        {
        }

        public MCCEvent(Event wrappedEvent)
        {
           

            fromEvent(wrappedEvent);
        }

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
