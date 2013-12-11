using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.FixedDynamicContentWidget
{
    [ControlDesigner(typeof(Designer))]
    [PropertyEditorTitle("Content items")]
    public class Widget : DynamicContentView
    {
        [TypeConverter(typeof(CollectionJsonTypeConverter<Guid>))]
        public IList<Guid> SelectedItems
        {
            get
            {
                if (this.selectedItems == null)
                    this.selectedItems = new List<Guid>();
                return this.selectedItems;
            }
            set
            {
                this.selectedItems = value;
            }
        }

        public override string DefaultMasterTemplateKey
        {
            get
            {
                if (string.IsNullOrEmpty(base.DefaultMasterTemplateKey))
                    base.DefaultMasterTemplateKey = "eeec4e9a-07b6-6924-b3bf-ff00006ffe9b";
                return base.DefaultMasterTemplateKey;
            }
            set
            {
                base.DefaultMasterTemplateKey = value;
            }
        }

        protected override void InitializeMasterView()
        {
            if (this.HasValidRelatedDataConfiguration && this.RelatedItemsIds != null)
            {
                this.MasterViewControl.SourceItemsIds = this.RelatedItemsIds;
            }

            

            var items = new List<DynamicContent>();
            foreach (var itemId in this.SelectedItems)
            {
                items.Add(this.DynamicManager.GetDataItem(this.DynamicContentType, itemId));
            }


            this.MasterViewControl.DataSource = items.AsQueryable();

            this.MasterViewControl.TemplateKey = string.IsNullOrEmpty(this.MasterViewDefinition.TemplateKey) ? this.DefaultMasterTemplateKey : this.MasterViewDefinition.TemplateKey;
            this.MasterViewControl.DynamicContentType = this.DynamicContentType;
            this.MasterViewControl.MasterViewDefinition = this.MasterViewDefinition;
            this.MasterViewControl.UrlEvaluationMode = this.UrlEvaluationMode;
            this.MasterViewControl.UrlKeyPrefix = this.UrlKeyPrefix;
            this.Controls.Add(this.MasterViewControl);
        }

        private IList<Guid> selectedItems;
    }
}
