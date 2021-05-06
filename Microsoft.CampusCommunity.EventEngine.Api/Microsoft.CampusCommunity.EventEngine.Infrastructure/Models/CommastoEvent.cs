using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    public class CommastoEvent
    {
        //
        // Summary:
        //     A title for the event.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Always)]
        public string Title { get; set; }
        //
        // Summary:
        //     Short summary of the event
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "shortDescription", Required = Required.Default)]
        public string ShortDescription { get; set; }
        //
        // Summary:
        //     A longer textual description of the event.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Always)]
        public string Description { get; set; }
        //
        // Summary:
        //     Highlight/Quote what attendees can expect from the event.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "highlightQuote", Required = Required.Default)]
        public string HighlightQuote { get; set; }
        //
        // Summary:
        //     Gets or sets is online meeting. True if this event has online meeting information,
        //     false otherwise. Default is false. Optional.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isOnlineMeeting", Required = Required.Default)]
        public bool? IsOnlineMeeting { get; set; }
        //
        // Summary:
        //     Gets or sets locations. The locations where the event is held or attended from.
        //     The location and locations properties always correspond with each other. If you
        //     update the location property, any prior locations in the locations collection
        //     would be removed and replaced by the new location value.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "locations", Required = Required.Default)]
        public IEnumerable<Location> Locations { get; set; }

        public bool? IsAllDay { get; set; }

        //
        // Summary:
        //     Gets or sets start. The date, time, and time zone that the event starts. By default,
        //     the start time is in UTC.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "start", Required = Required.Default)]
        public DateTimeTimeZone Start { get; set; }
        //
        // Summary:
        //     Gets or sets original end time zone. The end time zone that was set when the
        //     event was created. A value of tzone://Microsoft/Custom indicates that a legacy
        //     custom time zone was set in desktop Outlook.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "originalEndTimeZone", Required = Required.Default)]
        public string OriginalEndTimeZone { get; set; }

        //
        // Summary:
        //     Gets or sets original start time zone. The start time zone that was set when
        //     the event was created. A value of tzone://Microsoft/Custom indicates that a legacy
        //     custom time zone was set in desktop Outlook.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "originalStartTimeZone", Required = Required.Default)]
        public string OriginalStartTimeZone { get; set; }

        //
        // Summary:
        //     Gets or sets end. The date, time, and time zone that the event ends. By default,
        //     the end time is in UTC.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "end", Required = Required.Default)]
        public DateTimeTimeZone End { get; set; }

        //
        // Summary:
        //     Gets or sets audience. The possible values are: public, internal.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "audience", Required = Required.Default)]

        public Audience? Audience { get; set; }

        public String Campus { get; set; }


        //
        // Summary:
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "thumbnailTemplateId", Required = Required.Default)]
        public string ThumbnailTemplateId { get; set; }

        private String when()
        {
            DateTime start;
            DateTime end;

            if (DateTime.TryParse(Start.DateTime, out start) && DateTime.TryParse(End.DateTime, out end))
            {
                if (IsAllDay == true)
                {
                    return start.ToString("D") + " all day";
                }
                if (start.Date.Equals(end.Date))
                {
                    try
                    {
                        return intToMonthShort(start.Month) + " " + start.Day + ", " + start.Year + " " + start.TimeOfDay + " - " + end.TimeOfDay + " " + Start.TimeZone;

                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        return start.ToString("f") + " - " + end.ToString("f") + " " + Start.TimeZone;
                    }
                }
                else
                {
                    return start.ToString("f") + " - " + end.ToString("f") + " " + Start.TimeZone;
                }
            }
            else
            {
                return Start.DateTime + " " + End.DateTime;
            }

        }

        private String whereHTML()
        {
            if (IsOnlineMeeting == true)
            {
                return "online";
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (Location location in Locations)
                {
                    String locationString = location.DisplayName + " - " + location.Address.Street + ", " + location.Address.City;
                    if (stringBuilder.Length == 0)
                    {
                        stringBuilder.Append(locationString);
                    }
                    else
                    {
                        stringBuilder.Append(",<br>" + locationString);
                    }
                }
                if (stringBuilder.Length == 0)
                {
                    return Campus;
                }
                else
                {
                    return stringBuilder.ToString();
                }

            }

        }

        private string startDateString()
        {
            DateTime start;
            if (DateTime.TryParse(Start.DateTime, out start))
            {
                return start.ToString("dd.M.YYYY");
            }
            else
            {
                return Start.DateTime;
            }
        }

        private String intToMonthShort(int month)
        {
            switch (month)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sept";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private ItemBody getBody()
        {
            return new ItemBody()
            {
                ContentType = BodyType.Html,
                Content = this.toHtml()
            };
        }

        private String getBodyPreview()
        {
            return ShortDescription;
        }

        private Sensitivity getSenstivity()
        {
            switch (Audience)
            {
                case Models.Audience.Public:
                    return Sensitivity.Normal;
                case Models.Audience.Internal:
                    return Sensitivity.Private;
                default:
                    return Sensitivity.Private;
            }
        }
        public IEventSchemaExtension GetEventSchemaExtension()
        {
            return new EventSchemaExtension()
            {
                Description = Description,
                HighlightQuote = HighlightQuote
            };
        }

        private IEnumerable<Location> GetLocationsIncludingCampus()
        {
            if(Campus == null)
            {
                return this.Locations;
            } else
            {
         var campusLocation = new Location()
                    {
                        LocationType = LocationType.ConferenceRoom,
                        DisplayName = Campus
                    };
                    ICollection<Location> locationsWithCampus = new List<Location>();
                    locationsWithCampus.Add(campusLocation);
                    foreach (var location in Locations)
                    {
                        locationsWithCampus.Add(location);
                    }
                    return locationsWithCampus;
            }
           
        }

        private String getSubject()
        {
            return Title;
        }
        public Microsoft.Graph.Event ToGraphEvent()
        {
            return new Graph.Event()
            {
                Body = getBody(),
                BodyPreview = getBodyPreview(),
                End = this.End,
                IsAllDay = this.IsAllDay,
                Locations = GetLocationsIncludingCampus(),
                OriginalEndTimeZone = this.OriginalEndTimeZone,
                OriginalStartTimeZone = this.OriginalStartTimeZone,
                Sensitivity = getSenstivity(),
                Start = this.Start,
                Subject = getSubject(),
                Type = EventType.SingleInstance
            };
        }

        private string descriptionFirstSentence()
        {
            var i = Description.IndexOf(".");

            return i == -1 ? Description : Description.Substring(0, i);
        }

        private string descriptionWithoutFirstSentence()
        {
            var i = Description.IndexOf(".");

            return i == -1 ? "" : Description.Substring(i + 1);
        }

        private string toHtml()
        {
            return @"<!DOCTYPE html
    PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">

<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Community Gathering</title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    
</head>

<body style=""margin: 0; padding: 0;"">

    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
        <tr>
            <td>
                
                <table
                    style=""color:black; font-family:  'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;""
                    align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
                    <tr>
                        <td>
                            <table align=""center"">
                                <tbody>
                                    <tr>
                                        <td>
                                            <h3
                                                style=""color: #00A4EF; color: var(--color-mcc-blue, #00A4EF); letter-spacing: 10px; font-size: 15px; font-weight: 200; line-height: 30px;"">
                                                " + startDateString() + @" - " + Campus + @"
                                            </h3>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align=""center"">
                                <tbody>
                                    <tr>
                                        <td>
                                            <h1 style=""color: black; color: var(--color-secondary); margin: 0px; margin-top: 63px; margin-bottom: 63px; font-size: 70px; line-height: 75px;"">
                                                {EVENTTITLE}
                                            </h1>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td width=""230"">
                                        <table style=""font-size: 20px;"">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                            <tr>
                                                                <td>
                                                                    <h4
                                                                        style=""margin: 4px; margin-left: 0; font-weight: bold;"">
                                                                        When
                                                                    </h4>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <p style=""margin: 4px; margin-left: 0;"">
                                                                        " + when() + @"
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <h4
                                                                        style=""margin: 4px; margin-left: 0; font-weight: bold;"">
                                                                        Where
                                                                    </h4>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <p style=""margin: 4px; margin-left: 0;"">
                                                                        " + whereHTML() + @"
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>



                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td style=""vertical-align: top;"" width=""370"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""left"">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <h2 style=""margin-top: 0px; font-weight: 200;"">
                                                            " + ShortDescription + @"
                                                        </h2>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p>
                                                            " + descriptionFirstSentence() + @"
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h2>
                                                            " + HighlightQuote + @"
                                                        </h2>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p>
                                                            " + descriptionWithoutFirstSentence() + @"
                                                        </p>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>


                 
                </table>
            </td>
        </tr>

    </table>
</body>

</html>";
        }
    }
}
