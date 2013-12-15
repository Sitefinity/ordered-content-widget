using System;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;

namespace Telerik.Sitefinity.OrderedContentWidget.Configuration
{
    /// <summary>
    /// Represents the configuration class for the Ordered Content Widget module
    /// </summary>
    public class OrderedContentWidgetConfig : ModuleConfigBase
    {
        /// <summary>
        /// Initializes the default providers.
        /// </summary>
        /// <param name="providers">The providers.</param>
        protected override void InitializeDefaultProviders(ConfigElementDictionary<string, DataProviderSettings> providers)
        {
            // do nothing
        }
    }
}
