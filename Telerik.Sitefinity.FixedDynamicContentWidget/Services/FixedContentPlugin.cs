using System;
using System.Linq;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Text;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    /// <summary>
    /// Service Stack plug-in for the Fixed Dynamic Content module.
    /// </summary>
    public class FixedContentPlugin : IPlugin
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

            appHost.Routes.Add<DynamicTypeRequest>("/fixeddynamiccontent/dynamictypes", "GET");
            appHost.Routes.Add<DynamicContentsRequest>("/fixeddynamiccontent/dynamiccontents/{TypeId}", "GET");
            appHost.Routes.Add<PageRequest>("/fixeddynamiccontent/page/{pageId}", "GET");
            appHost.Routes.Add<WidgetTemplateRequest>("/fixeddynamiccontent/widgettemplates", "GET");

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
        }
    }
}
