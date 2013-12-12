using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class DynamicContentsResponse
    {
        public List<DynamicContentResponse> Items { get; set; }
        public int VirtualCount { get; set; }
    }
}
