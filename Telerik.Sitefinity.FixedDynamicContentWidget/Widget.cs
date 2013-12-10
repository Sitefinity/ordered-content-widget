using System;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.FixedDynamicContentWidget
{
    [ControlDesigner(typeof(Designer))]
    [PropertyEditorTitle("Content items")]
    public class Widget : DynamicContentView
    {
    }
}
