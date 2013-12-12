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

    }
}
