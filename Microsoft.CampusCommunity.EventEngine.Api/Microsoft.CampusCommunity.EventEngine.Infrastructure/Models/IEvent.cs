using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
   public interface IEvent
    {
        String Subject { get; set; }
        String BodyPreview { get; set; }
        String Body { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }

        EventSensitivity Sensitivity { get; set; }

        String Campus { get; set; }

        String ToHtml();
    }
}
