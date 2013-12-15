using System;
using System.Linq;

namespace Telerik.Sitefinity.OrderedContentWidget.Services
{
    public class DynamicTypeResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ClrType { get; set; }
        public Guid[] SupportedTaxonomyIds { get; set; }
    }
}
