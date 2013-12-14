using System;
using System.Linq;
using ServiceStack.ServiceInterface;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class PageService : Service
    {
        public object Get(PageRequest request)
        {
            PageManager manager = PageManager.GetManager();
            var page = manager.GetPageNode(request.PageId);
            return new PageResponse()
            {
                Title = page.Title
            };
        }
    }
}
