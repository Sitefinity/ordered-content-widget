using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.FixedDynamicContentWidget.Configuration;
using Telerik.Sitefinity.FixedDynamicContentWidget.Services;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.FixedDynamicContentWidget
{
    /// <summary>
    /// Represents the module that installs Fixed Dynamic Content Widget
    /// and all the necessary resources.
    /// </summary>
    public class Module : ModuleBase
    {
        #region Properties

        /// <summary>
        /// Gets the landing page id.
        /// </summary>
        /// <value>The landing page id.</value>
        public override Guid LandingPageId
        {
            get 
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Gets the managers.
        /// </summary>
        /// <value>The managers.</value>
        public override Type[] Managers
        {
            get 
            {
                return new Type[0];
            }
        }

        #endregion

        #region Public and overridden methods

        /// <summary>
        /// Initializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);

            App.WorkWith()
               .Module()
               .Initialize()
               .Configuration<FixedDynamicContentConfig>()
               .ServiceStackPlugin(new FixedContentPlugin());
        }

        /// <summary>
        /// Installs the specified initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public override void Install(SiteInitializer initializer)
        {
            // do nothing for now
        }

        /// <summary>
        /// Gets the module config.
        /// </summary>
        /// <returns></returns>
        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<FixedDynamicContentConfig>();
        }

        #endregion

    }
}
