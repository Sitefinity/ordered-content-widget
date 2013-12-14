using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.FixedDynamicContentWidget
{
    [ObjectInfo("OrderedContentResources", ResourceClassId = "OrderedContentResources")]
    public sealed class OrderedContentResources : Resource
    {
        #region Constructions
        /// <summary>
        /// Initializes new instance of <see cref="OrderedContentResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public OrderedContentResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="OrderedContentResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public OrderedContentResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description
        
        /// <summary>
        /// Help
        /// </summary>
        [ResourceEntry("OrderedContentTitle",
            Value = "Ordered content",
            Description = "The title of this class.",
            LastModified = "2013/12/12")]
        public string OrderedContentTitle
        {
            get
            {
                return this["OrderedContentTitle"];
            }
        }

        /// <summary>
        /// Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.
        /// </summary>
        [ResourceEntry("OrderedContentDescription",
            Value = "Contains localizable resources for the ordered content widget.",
            Description = "The description of this class.",
            LastModified = "2013/12/12")]
        public string OrderedContentDescription
        {
            get
            {
                return this["OrderedContentDescription"];
            }
        }
        
        #endregion

        /// <summary>
        /// word: Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2013/12/12")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// phrase: List settings
        /// </summary>
        [ResourceEntry("ListSettings",
            Value = "List settings",
            Description = "phrase: List settings",
            LastModified = "2013/12/12")]
        public string ListSettings
        {
            get
            {
                return this["ListSettings"];
            }
        }

        /// <summary>
        /// phrase: Single item settings
        /// </summary>
        [ResourceEntry("SingleItemSettings",
            Value = "Single item settings",
            Description = "phrase: Single item settings",
            LastModified = "2013/12/12")]
        public string SingleItemSettings
        {
            get
            {
                return this["SingleItemSettings"];
            }
        }

        /// <summary>
        /// phrase: Content type
        /// </summary>
        [ResourceEntry("ContentType",
            Value = "Content type",
            Description = "phrase: Content type",
            LastModified = "2013/12/12")]
        public string ContentType
        {
            get
            {
                return this["ContentType"];
            }
        }

        /// <summary>
        /// phrase: -- choose content type --
        /// </summary>
        [ResourceEntry("ChooseContentType",
            Value = "-- choose content type --",
            Description = "phrase: -- choose content type --",
            LastModified = "2013/12/12")]
        public string ChooseContentType
        {
            get
            {
                return this["ChooseContentType"];
            }
        }

        /// <summary>
        /// word: All
        /// </summary>
        [ResourceEntry("All",
            Value = "All",
            Description = "word: All",
            LastModified = "2013/12/12")]
        public string All
        {
            get
            {
                return this["All"];
            }
        }

        /// <summary>
        /// word: Selected
        /// </summary>
        [ResourceEntry("Selected",
            Value = "Selected",
            Description = "word: Selected",
            LastModified = "2013/12/12")]
        public string Selected
        {
            get
            {
                return this["Selected"];
            }
        }

        /// <summary>
        /// word: Title
        /// </summary>
        [ResourceEntry("Title",
            Value = "Title",
            Description = "word: Title",
            LastModified = "2013/12/12")]
        public string Title
        {
            get
            {
                return this["Title"];
            }
        }

        /// <summary>
        /// phrase: Modified on
        /// </summary>
        [ResourceEntry("ModifiedOn",
            Value = "Modified on",
            Description = "phrase: Modified on",
            LastModified = "2013/12/12")]
        public string ModifiedOn
        {
            get
            {
                return this["ModifiedOn"];
            }
        }

        /// <summary>
        /// word: By
        /// </summary>
        [ResourceEntry("By",
            Value = "By",
            Description = "word: By",
            LastModified = "2013/12/12")]
        public string By
        {
            get
            {
                return this["By"];
            }
        }

        /// <summary>
        /// word: View
        /// </summary>
        [ResourceEntry("View",
            Value = "View",
            Description = "word: View",
            LastModified = "2013/12/12")]
        public string View
        {
            get
            {
                return this["View"];
            }
        }

        /// <summary>
        /// Instruction on the widget; line 1
        /// </summary>
        [ResourceEntry("WidgetHelp1",
            Value = "Select items you want to be displayed and order them manually",
            Description = "Instruction on the widget; line 1",
            LastModified = "2013/12/12")]
        public string WidgetHelp1
        {
            get
            {
                return this["WidgetHelp1"];
            }
        }

        /// <summary>
        /// Instruction on the widget; line 2
        /// </summary>
        [ResourceEntry("WidgetHelp2",
            Value = "You can change the sorting of items to automatic in the List settings tab",
            Description = "Instruction on the widget; line 2",
            LastModified = "2013/12/12")]
        public string WidgetHelp2
        {
            get
            {
                return this["WidgetHelp2"];
            }
        }

        /// <summary>
        /// phrase: Allow paging
        /// </summary>
        [ResourceEntry("AllowPaging",
            Value = "Allow Paging",
            Description = "phrase: Allow paging",
            LastModified = "2013/12/12")]
        public string AllowPaging
        {
            get
            {
                return this["AllowPaging"];
            }
        }

        /// <summary>
        /// phrase: Divide the list on pages up to
        /// </summary>
        [ResourceEntry("DivideTheListOnPagesUpTo",
            Value = "Divide the list on pages up to",
            Description = "phrase: Divide the list on pages up to",
            LastModified = "2013/12/12")]
        public string DivideTheListOnPagesUpTo
        {
            get
            {
                return this["DivideTheListOnPagesUpTo"];
            }
        }

        /// <summary>
        /// phrase: items per page
        /// </summary>
        [ResourceEntry("ItemsPerPage",
            Value = "items per page",
            Description = "phrase: items per page",
            LastModified = "2013/12/12")]
        public string ItemsPerPage
        {
            get
            {
                return this["ItemsPerPage"];
            }
        }

        /// <summary>
        /// phrase: Use Limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use Limit",
            Description = "phrase: Use Limit",
            LastModified = "2013/12/12")]
        public string UseLimit
        {
            get
            {
                return this["UseLimit"];
            }
        }

        /// <summary>
        /// phrase: Show only limited number of items
        /// </summary>
        [ResourceEntry("ShowOnlyLimitedNumberOfItems",
            Value = "Show only limited number of items",
            Description = "phrase: Show only limited number of items",
            LastModified = "2013/12/12/")]
        public string ShowOnlyLimitedNumberOfItems
        {
            get
            {
                return this["ShowOnlyLimitedNumberOfItems"];
            }
        }

        /// <summary>
        /// phrase: items in total
        /// </summary>
        [ResourceEntry("ItemsInTotal",
            Value = "items in total",
            Description = "phrase: items in total",
            LastModified = "2013/12/12")]
        public string ItemsInTotal
        {
            get
            {
                return this["ItemsInTotal"];
            }
        }

        /// <summary>
        /// phrase: No limit and paging
        /// </summary>
        [ResourceEntry("NoLimitAndPaging",
            Value = "No limit and paging",
            Description = "phrase: No limit and paging",
            LastModified = "2013/12/12")]
        public string NoLimitAndPaging
        {
            get
            {
                return this["NoLimitAndPaging"];
            }
        }

        /// <summary>
        /// phrase: Show all published items at once
        /// </summary>
        [ResourceEntry("ShowAllPublishedItemsAtOnce",
            Value = "Show all published items at once",
            Description = "phrase: Show all published items at once",
            LastModified = "2013/12/12")]
        public string ShowAllPublishedItemsAtOnce
        {
            get
            {
                return this["ShowAllPublishedItemsAtOnce"];
            }
        }

        /// <summary>
        /// phrase: Sort items
        /// </summary>
        [ResourceEntry("SortItems",
            Value = "Sort items",
            Description = "phrase: Sort items",
            LastModified = "2013/12/13")]
        public string SortItems
        {
            get
            {
                return this["SortItems"];
            }
        }

        /// <summary>
        /// phrase: As manually ordered
        /// </summary>
        [ResourceEntry("AsManuallyOrdered",
            Value = "As manually ordered",
            Description = "phrase: As manually ordered",
            LastModified = "2013/12/13")]
        public string AsManuallyOrdered
        {
            get
            {
                return this["AsManuallyOrdered"];
            }
        }

        /// <summary>
        /// phrase: Newest first
        /// </summary>
        [ResourceEntry("NewestFirst",
            Value = "Newest first",
            Description = "phrase: Newest first",
            LastModified = "2013/12/13")]
        public string NewestFirst
        {
            get
            {
                return this["NewestFirst"];
            }
        }

        /// <summary>
        /// phrase: Oldest first
        /// </summary>
        [ResourceEntry("OldestFirst",
            Value = "Oldest first",
            Description = "phrase: Oldest first",
            LastModified = "2013/12/13")]
        public string OldestFirst
        {
            get
            {
                return this["OldestFirst"];
            }
        }

        /// <summary>
        /// phrase: By Title (A-Z)
        /// </summary>
        [ResourceEntry("ByTitleAz",
            Value = "By Title (A-Z)",
            Description = "phrase: By Title (A-Z)",
            LastModified = "2013/12/13")]
        public string ByTitleAz
        {
            get
            {
                return this["ByTitleAz"];
            }
        }

        /// <summary>
        /// phrase: By Title (Z-A)
        /// </summary>
        [ResourceEntry("ByTitleZa",
            Value = "By Title (Z-A)",
            Description = "phrase: By Title (Z-A)",
            LastModified = "2013/12/13")]
        public string ByTitleZa
        {
            get
            {
                return this["ByTitleZa"];
            }
        }

        /// <summary>
        /// phrase: List template
        /// </summary>
        [ResourceEntry("ListTemplate",
            Value = "List template",
            Description = "phrase: List template",
            LastModified = "2013/12/13")]
        public string ListTemplate
        {
            get
            {
                return this["ListTemplate"];
            }
        }

        /// <summary>
        /// phrase: Single Item template
        /// </summary>
        [ResourceEntry("SingleItemTemplate",
            Value = "Single item template",
            Description = "phrase: Single Item template",
            LastModified = "2013/12/13")]
        public string SingleItemTemplate
        {
            get
            {
                return this["SingleItemTemplate"];
            }
        }

        /// <summary>
        /// phrase: Open Single item in...
        /// </summary>
        [ResourceEntry("OpenSingleItemIn",
            Value = "Open Single item in...",
            Description = "phrase: Open Single item in...",
            LastModified = "2013/12/13")]
        public string OpenSingleItemIn
        {
            get
            {
                return this["OpenSingleItemIn"];
            }
        }

        /// <summary>
        /// phrase: Auto-generated page
        /// </summary>
        [ResourceEntry("AutoGeneratedPage",
            Value = "Auto-generated page",
            Description = "phrase: Auto-generated page",
            LastModified = "2013/12/13")]
        public string AutoGeneratedPage
        {
            get
            {
                return this["AutoGeneratedPage"];
            }
        }

        /// <summary>
        /// phrase: with the same layout as the list page
        /// </summary>
        [ResourceEntry("WithTheSameLayoutAsTheListPage",
            Value = "with the same layout as the list page",
            Description = "phrase: with the same layout as the list page",
            LastModified = "2013/12/13")]
        public string WithTheSameLayoutAsTheListPage
        {
            get
            {
                return this["WithTheSameLayoutAsTheListPage"];
            }
        }

        /// <summary>
        /// phrase: Selected existing page
        /// </summary>
        [ResourceEntry("SelectedExistingPage",
            Value = "Selected existing page",
            Description = "phrase: Selected existing page",
            LastModified = "2013/12/13")]
        public string SelectedExistingPage
        {
            get
            {
                return this["SelectedExistingPage"];
            }
        }

        /// <summary>
        /// phrase: Select page
        /// </summary>
        [ResourceEntry("SelectPage",
            Value = "Select Page",
            Description = "phrase: Select page",
            LastModified = "2013/12/13")]
        public string SelectPage
        {
            get
            {
                return this["SelectPage"];
            }
        }

        /// <summary>
        /// phrase: Filter items
        /// </summary>
        [ResourceEntry("FilterItems",
            Value = "Filter items",
            Description = "phrase: Filter items",
            LastModified = "2013/12/14")]
        public string FilterItems
        {
            get
            {
                return this["FilterItems"];
            }
        }

        /// <summary>
        /// phrase: by category, tag...
        /// </summary>
        [ResourceEntry("ByCategoryTag",
            Value = "by category, tag...",
            Description = "phrase: by category, tag...",
            LastModified = "2013/12/14")]
        public string ByCategoryTag
        {
            get
            {
                return this["ByCategoryTag"];
            }
        }

        /// <summary>
        /// phrase: by Dates...
        /// </summary>
        [ResourceEntry("ByDates",
            Value = "by Dates...",
            Description = "phrase: by Dates...",
            LastModified = "2013/12/14")]
        public string ByDates
        {
            get
            {
                return this["ByDates"];
            }
        }

        /// <summary>
        /// word: Select
        /// </summary>
        [ResourceEntry("Select",
            Value = "Select",
            Description = "word: Select",
            LastModified = "2013/12/14")]
        public string Select
        {
            get
            {
                return this["Select"];
            }
        }

        /// <summary>
        /// word: Done
        /// </summary>
        [ResourceEntry("Done",
            Value = "Done",
            Description = "word: Done",
            LastModified = "2013/12/14")]
        public string Done
        {
            get
            {
                return this["Done"];
            }
        }

        /// <summary>
        /// word: or
        /// </summary>
        [ResourceEntry("Or",
            Value = "or",
            Description = "word: or",
            LastModified = "2013/12/14")]
        public string Or
        {
            get
            {
                return this["Or"];
            }
        }

        /// <summary>
        /// word: Cancel
        /// </summary>
        [ResourceEntry("Cancel",
            Value = "Cancel",
            Description = "word: Cancel",
            LastModified = "2013/12/14")]
        public string Cancel
        {
            get
            {
                return this["Cancel"];
            }
        }

    }
}
