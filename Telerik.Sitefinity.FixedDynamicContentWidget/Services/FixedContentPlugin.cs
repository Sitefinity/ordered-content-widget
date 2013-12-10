using System;
using System.Linq;
using ServiceStack.WebHost.Endpoints;

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
            appHost.RegisterService<DynamicContentService>();

            appHost.Routes.Add<DynamicTypeRequest>("/fixeddynamiccontent/dynamictypes", "GET");
            appHost.Routes.Add<DynamicContentRequest>("/fixeddynamiccontent/dynamiccontents/{TypeId}", "GET");
        }
    }
}
