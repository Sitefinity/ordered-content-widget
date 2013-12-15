using System;
using System.Linq;

namespace Telerik.Sitefinity.OrderedContentWidget.Services
{
    public class DynamicContentResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime LastModified { get; set; }
        public string Author { get; set; }
        public string CanonicalUrl { get; set; }
    }
}
