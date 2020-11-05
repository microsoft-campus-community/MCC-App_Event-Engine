using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    public class Event : IEvent
    {
        public string Subject { get ; set ; }
        public string BodyPreview { get ; set ; }
        public string Body { get ; set ; }
        public DateTime Start { get ; set ; }
        public DateTime End { get ; set ; }
        public EventSensitivity Sensitivity { get ; set ; }
        public string Campus { get ; set ; }

        public string ToHtml()
        {

        }
    }
}
