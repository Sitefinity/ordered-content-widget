using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.FixedDynamicContentWidget
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SelectedItem
    {
        public string Id { get; set; }
    }
}
