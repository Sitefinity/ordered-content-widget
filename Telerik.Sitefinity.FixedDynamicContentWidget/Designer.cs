using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

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

        #region Public and overridden methods

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container">The container.</param>
        protected override void InitializeControls(GenericContainer container)
        {
            // do nothing for now
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(Designer.designerScript, typeof(Designer).Assembly.FullName));
            return scripts;
        }

        #endregion

        #region Private fields and constants

        private const string layoutTemplatePath = "~/FixedDyamicContentWidget/Telerik.Sitefinity.FixedDynamicContentWidget.Templates.DesignerTemplate.ascx";
        internal const string designerScript = "Telerik.Sitefinity.FixedDynamicContentWidget.Scripts.Designer.js";

        #endregion

    }
}
