using System;
using System.Linq;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace Telerik.Sitefinity.OrderedContentWidget.Services
{
    /// <summary>
    /// Service Stack plug-in for the Ordered Content Widget module.
    /// </summary>
    public class OrderedContentWidgetPlugin : IPlugin
    {
        /// <summary>
        /// Registers the specified app host.
        /// </summary>
        /// <param name="appHost">The app host.</param>
        public void Register(IAppHost appHost)
        {
            appHost.RegisterService<DynamicTypesService>();
            appHost.RegisterService<DynamicContentsService>();
            appHost.RegisterService<PageService>();
            appHost.RegisterService<WidgetTemplateService>();

            appHost.Routes.Add<DynamicTypeRequest>("/orderedcontent/dynamictypes", "GET");
            appHost.Routes.Add<DynamicContentsRequest>("/orderedcontent/dynamiccontents/{TypeId}", "GET");
            appHost.Routes.Add<PageRequest>("/orderedcontent/page/{pageId}", "GET");
            appHost.Routes.Add<WidgetTemplateRequest>("/orderedcontent/widgettemplates", "GET");

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
        }
    }
}
