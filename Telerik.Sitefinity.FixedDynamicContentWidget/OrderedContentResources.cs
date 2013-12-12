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
        /// phrase: List Settings
        /// </summary>
        [ResourceEntry("ListSettings",
            Value = "List Settings",
            Description = "phrase: List Settings",
            LastModified = "2013/12/12")]
        public string ListSettings
        {
            get
            {
                return this["ListSettings"];
            }
        }

        /// <summary>
        /// phrase: Single Item Settings
        /// </summary>
        [ResourceEntry("SingleItemSettings",
            Value = "Single Item Settings",
            Description = "phrase: Single Item Settings",
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

    }
}
