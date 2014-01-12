using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.OrderedContentWidget
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
        /// Gets the templates control for add/edit single item templates.
        /// </summary>
        /// <value>The templates control for add/edit single item templates.</value>
        protected virtual CreateEditTemplateControl SingleItemTemplates
        {
            get
            {
                return this.Container.GetControl<CreateEditTemplateControl>("singleItemTemplates", true);
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

        /// <summary>
        /// Gets the instance of the page selector.
        /// </summary>
        protected virtual PageSelector PageSelector
        {
            get
            {
                return this.Container.GetControl<PageSelector>("pageSelector", true);
            }
        }

        /// <summary>
        /// Gets the instance of the filter selector
        /// </summary>
        //protected virtual FilterSelector FilterSelector
        //{
        //    get
        //    {
        //        return this.Container.GetControl<FilterSelector>("filterSelector", true);
        //    }
        //}

        protected virtual PlaceHolder FilterSelectorPlaceholder
        {
            get
            {
                return this.Container.GetControl<PlaceHolder>("filterSelectorPlaceholder", true);
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
            //this.ListTemplates.DesignedMasterViewType = typeof(DynamicContentViewMaster).FullName;
            this.ListTemplates.WindowManager = this.RadWindowManager;
            //this.SingleItemTemplates.DesignedMasterViewType = typeof(DynamicContentViewDetail).FullName;
            this.SingleItemTemplates.WindowManager = this.RadWindowManager;

            this.BuildFilterSelector();

            //this.FilterSelector.SetTaxonomyId(Designer.CategoriesQueryDataName, TaxonomyManager.CategoriesTaxonomyId);
            //this.FilterSelector.SetTaxonomyId(Designer.TagsQueryDataName, TaxonomyManager.TagsTaxonomyId);
        }

        private void BuildFilterSelector()
        {
            this.filterSelector = new FilterSelector()
            {
                AllowMultipleSelection = true,
                ItemsContainerTag = "ul",
                ItemTag = "li",
                ItemsContainerCssClass = "sfCheckListBox sfExpandedPropertyDetails",
                DisabledTextCssClass = "sfTooltip"
            };

            // taxonomies
            var taxonomyManager = TaxonomyManager.GetManager();

            var systemTaxonomies = new Guid[] { SiteInitializer.PageTemplatesTaxonomyId };

            var taxonomies = taxonomyManager.GetTaxonomies<Taxonomy>();

            foreach (var taxonomy in taxonomies)
            {
                if (systemTaxonomies.Contains(taxonomy.Id))
                    continue;

                var taxonomyItem = new FilterSelectorItem()
                {
                    Text = string.Format(Res.Get<OrderedContentResources>().BySomething, taxonomy.Title),
                    GroupLogicalOperator = "AND",
                    ItemLogicalOperator = "OR",
                    ConditionOperator = "Contains",
                    QueryDataName = taxonomy.Name,
                    QueryFieldName = taxonomy.TaxonName,
                    QueryFieldType = typeof(Guid).FullName
                };

                if (taxonomyItem.QueryDataName == "Category")
                {
                    taxonomyItem.QueryDataName = "Categories";
                }
                else if (taxonomyItem.QueryDataName == "Department")
                {
                    taxonomyItem.QueryDataName = "Departments";
                }

                if (taxonomy.GetType() == typeof(FlatTaxonomy))
                {
                    taxonomyItem.SelectorResultView = new GenericTemplate(new FlatTaxonSelectorResultView()
                    {
                        WebServiceUrl = "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc",
                        TaxonomyId = taxonomy.Id,
                        AllowMultipleSelection = true
                    });
                }
                else if (taxonomy.GetType() == typeof(HierarchicalTaxonomy))
                {
                    taxonomyItem.SelectorResultView = new GenericTemplate(new HierarchicalTaxonSelectorResultView()
                    {
                        WebServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc",
                        TaxonomyId = taxonomy.Id,
                        AllowMultipleSelection = true,
                        HierarchicalTreeRootBindModeEnabled = false
                    });
                }

                taxonomyItem.Attributes.Add("data-filter-id", taxonomy.Id.ToString());
                this.filterSelector.Items.Add(taxonomyItem);
                this.filterSelector.SetTaxonomyId(taxonomy.Name, taxonomy.Id);
            }


            // date range
            var dateRangeFilterItem = new FilterSelectorItem()
            {
                Text = Res.Get<OrderedContentResources>().ByDates,
                GroupLogicalOperator = "AND",
                ItemLogicalOperator = "AND",
                QueryDataName = "Dates",
                QueryFieldName = "PublicationDate",
                QueryFieldType = typeof(DateTime).FullName,
                CollectionTranslatorDelegate = "_translateQueryItems",
                CollectionBuilderDelegate = "_buildQueryItems"
            };
            dateRangeFilterItem.SelectorResultView = new GenericTemplate(new DateRangeSelectorResultView()
            {
                SelectorDateRangesTitle = Res.Get<Labels>().DisplayNewsPublishedIn
            });

            dateRangeFilterItem.Attributes.Add("data-filter-id", "date-selector");
            this.filterSelector.Items.Add(dateRangeFilterItem);

            this.FilterSelectorPlaceholder.Controls.Add(this.filterSelector);

        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            ScriptControlDescriptor descriptor = descriptors.Last() as ScriptControlDescriptor;

            descriptor.AddComponentProperty("listTemplateControl", this.ListTemplates.ClientID);
            descriptor.AddComponentProperty("singleItemTemplateControl", this.SingleItemTemplates.ClientID);
            descriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
            descriptor.AddComponentProperty("filterSelector", this.filterSelector.ClientID);

            return descriptors;
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(Designer.designerScript, typeof(Designer).Assembly.FullName));
            scripts.Add(new ScriptReference(Designer.sortableScript, typeof(Designer).Assembly.FullName));
            scripts.Add(new ScriptReference(Designer.filterScript, typeof(FilterSelector).Assembly.FullName));
            return scripts;
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                //we use div wrapper tag to make easier common styling
                return HtmlTextWriterTag.Div;
            }
        }

        #endregion

        #region Private fields and constants

        private const string layoutTemplatePath = "~/OrderedContentWidget/Telerik.Sitefinity.OrderedContentWidget.Templates.DesignerTemplate.ascx";
        internal const string designerScript = "Telerik.Sitefinity.OrderedContentWidget.Scripts.Designer.js";
        internal const string sortableScript = "Telerik.Sitefinity.OrderedContentWidget.Scripts.lib.sortable.js";
        internal const string filterScript = "Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js";

        private FilterSelector filterSelector;

        #endregion

    }
}
