using System;
using System.Linq;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class DynamicContentsRequest
    {
        public string TypeId { get; set; }
        public string QueryData { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public Guid[] SelectedContentIds { get; set; }
        public string MainFieldStartsWith { get; set; }
    }
}
