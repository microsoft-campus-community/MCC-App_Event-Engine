using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Models
{
    public interface IEventSchemaExtension
    {
        String ReadableId { get; set; }
        String PlanningStatus { get; set; }
        String TitleImageUrl { get; set; }
        Boolean RequirePhotoAgreement { get; set; }
        String SupportRequestId { get; set; }
    }
}
