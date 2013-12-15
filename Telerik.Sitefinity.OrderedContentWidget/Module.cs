﻿using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.OrderedContentWidget.Configuration;
using Telerik.Sitefinity.OrderedContentWidget.Services;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.OrderedContentWidget
{
    /// <summary>
    /// Represents the module that installs Ordered Content Widget
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

        public override string Name
        {
            get
            {
                return Module.ModuleName;
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
            App.WorkWith()
               .Module(Module.ModuleName)
               .Initialize()
               .Configuration<OrderedContentWidgetConfig>()
               .Localization<OrderedContentResources>()
               .ServiceStackPlugin(new OrderedContentWidgetPlugin());

            base.Initialize(settings);
        }

        /// <summary>
        /// Installs the specified initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public override void Install(SiteInitializer initializer)
        {
            initializer
                .Installer
                .PageToolbox()
                    .ContentSection()
                        .LoadOrAddWidget<Widget>("OrderedContentWidget")
                        .SetTitle("Ordered Content")
                        .Done();

            this.InstallCustomVirtualPaths(initializer);
        }

        /// <summary>
        /// Gets the module config.
        /// </summary>
        /// <returns></returns>
        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<OrderedContentWidgetConfig>();
        }

        #endregion

        #region Non-public methods

        private void InstallCustomVirtualPaths(SiteInitializer initializer)
        {
            var virtualPathConfig = initializer.Context.GetConfig<VirtualPathSettingsConfig>();
            ConfigManager.Executed += new EventHandler<ExecutedEventArgs>(ConfigManager_Executed);
            var dashboardModuleVirtualPathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
            {
                VirtualPath = Module.OrderedContentWidgetVirtualPath + "*",
                ResolverName = "EmbeddedResourceResolver",
                ResourceLocation = "Telerik.Sitefinity.OrderedContentWidget"
            };
            if (!virtualPathConfig.VirtualPaths.ContainsKey(Module.OrderedContentWidgetVirtualPath + "*"))
                virtualPathConfig.VirtualPaths.Add(dashboardModuleVirtualPathConfig);
        }

        private void ConfigManager_Executed(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs args)
        {
            if (args.CommandName == "SaveSection")
            {
                var section = args.CommandArguments as VirtualPathSettingsConfig;
                if (section != null)
                {
                    // Reset the VirtualPathManager whenever we save the VirtualPathConfig section.
                    // This is needed so that our prefixes for the widget templates in the module assembly are taken into account.
                    VirtualPathManager.Reset();
                }
            }
        }

        #endregion

        #region Private fields and constants

        public const string ModuleName = "OrderedContentWidget";
        public const string OrderedContentWidgetVirtualPath = "~/OrderedContentWidget/";

        #endregion

    }
}
