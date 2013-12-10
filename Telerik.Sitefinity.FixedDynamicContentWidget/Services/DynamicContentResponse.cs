using System;
using System.Linq;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class DynamicContentResponse
    {
        public string Title { get; set; }
        public DateTime LastModified { get; set; }
        public string Author { get; set; }
        public string CanonicalUrl { get; set; }
    }
}
