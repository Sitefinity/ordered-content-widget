using System;
using System.Linq;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class DynamicContentsRequest
    {
        public string TypeId { get; set; }
        public string QueryData { get; set; }
        public Guid[] SelectedContentIds { get; set; }
    }
}
