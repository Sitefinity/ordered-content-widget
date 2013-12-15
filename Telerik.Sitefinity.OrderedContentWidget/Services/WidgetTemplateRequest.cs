using System;
using System.Linq;

namespace Telerik.Sitefinity.OrderedContentWidget.Services
{
    public class WidgetTemplateRequest
    {
        public Guid DynamicTypeId { get; set; }
        public bool IsMaster { get; set; }
    }
}
