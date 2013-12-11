using System;
using System.Linq;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class DynamicContentsRequest
    {
        public string TypeId { get; set; }
        public Guid[] SelectedContentIds { get; set; }
    }
}
