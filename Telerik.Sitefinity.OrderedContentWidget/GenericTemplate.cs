using System;
using System.Linq;
using System.Web.UI;

namespace Telerik.Sitefinity.OrderedContentWidget
{
    public class GenericTemplate : ITemplate
    {
        public GenericTemplate(Control control)
        {
            this.control = control;
        }

        public void InstantiateIn(Control container)
        {
            container.Controls.Add(this.control);
        }

        private Control control;
    }
}
