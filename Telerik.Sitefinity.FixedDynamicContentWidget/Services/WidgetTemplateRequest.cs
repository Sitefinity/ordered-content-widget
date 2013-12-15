using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class WidgetTemplateRequest
    {
        public Guid DynamicTypeId { get; set; }
        public bool IsMaster { get; set; }
    }
}
