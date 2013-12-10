using System;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Configuration
{
    /// <summary>
    /// Represents the configuration class for the FixedContent module
    /// </summary>
    public class FixedDynamicContentConfig : ModuleConfigBase
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
