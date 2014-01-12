using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.OrderedContentWidget
{
    [ControlDesigner(typeof(Designer))]
    [PropertyEditorTitle("Content items")]
    public class Widget : DynamicContentView
    {
        public enum SortModes
        {
            None,
            NewestFirst,
            OldestFirst,
            AlphabetAsc,
            AlphabetDesc,
            Manual
        }

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

        public SortModes SortMode
        {
            get;
            set;
        }

        protected override void InitializeMasterView()
        {
            if (this.HasValidRelatedDataConfiguration && this.RelatedItemsIds != null)
            {
                this.MasterViewControl.SourceItemsIds = this.RelatedItemsIds;
            }

            switch (this.SortMode)
            {
                case SortModes.NewestFirst:
                    this.MasterViewDefinition.SortExpression = "PublicationDate ASC";
                    break;
                case SortModes.OldestFirst:
                    this.MasterViewDefinition.SortExpression = "PublicationDate DESC";
                    break;
                case SortModes.AlphabetAsc:
                    this.MasterViewDefinition.SortExpression = "Title ASC"; // TODO: this is a bug, title is hard-coded
                    break;
                case SortModes.AlphabetDesc:
                    this.MasterViewDefinition.SortExpression = "Title DESC"; // TODO: this is a bug, title is hard-coded
                    break;
                case SortModes.Manual:
                    this.MasterViewDefinition.SortExpression = "";

                    var sortedItems = new List<DynamicContent>();
                    var selectedItems = this.DynamicManager.GetDataItems(this.DynamicContentType)
                                                           .Where(d => this.SelectedItems.Contains(d.Id));

                    foreach (var itemId in this.SelectedItems)
                    {
                        var selectedItem = selectedItems.Where(si => si.Id == itemId).SingleOrDefault();
                        if(selectedItem != null)
                        {
                            sortedItems.Add(selectedItem);
                        }
                    }
                    this.MasterViewControl.DataSource = sortedItems.AsQueryable();
                    break;
            }

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
