using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.FixedDynamicContentWidget
{
    /// <summary>
    /// Represents the designer for the widget.
    /// </summary>
    public class Designer : ControlDesignerBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the layout template path.
        /// </summary>
        /// <value>The layout template path.</value>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    base.LayoutTemplatePath = Designer.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        #endregion

        #region Control References

        /// <summary>
        /// Gets the templates control for add/edit list templates.
        /// </summary>
        /// <value>The templates control for add/edit list templates.</value>
        protected virtual CreateEditTemplateControl ListTemplates
        {
            get
            {
                return this.Container.GetControl<CreateEditTemplateControl>("listTemplates", true);
            }
        }

        /// <summary>
        /// Gets the correct instance of RadWindowManager
        /// </summary>
        protected virtual RadWindowManager RadWindowManager
        {
            get
            {
                return this.Container.GetControl<RadWindowManager>("windowManager", true);
            }
        }

        #endregion

        #region Public and overridden methods

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container">The container.</param>
        protected override void InitializeControls(GenericContainer container)
        {
            this.ListTemplates.DesignedMasterViewType = typeof(DynamicContentViewMaster).FullName;
            this.ListTemplates.WindowManager = this.RadWindowManager;
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(Designer.designerScript, typeof(Designer).Assembly.FullName));
            scripts.Add(new ScriptReference(Designer.sortableScript, typeof(Designer).Assembly.FullName));
            return scripts;
        }

        #endregion

        #region Private fields and constants

        private const string layoutTemplatePath = "~/FixedDyamicContentWidget/Telerik.Sitefinity.FixedDynamicContentWidget.Templates.DesignerTemplate.ascx";
        internal const string designerScript = "Telerik.Sitefinity.FixedDynamicContentWidget.Scripts.Designer.js";
        internal const string sortableScript = "Telerik.Sitefinity.FixedDynamicContentWidget.Scripts.lib.sortable.js";

        #endregion

    }
}
