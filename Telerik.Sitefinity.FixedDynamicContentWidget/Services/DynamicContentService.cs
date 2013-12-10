using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class DynamicContentService : Service
    {
        public object Get(DynamicContentRequest request)
        {
            var dynamicContentList = new List<DynamicContentResponse>();
            var provider = ModuleBuilderManager.GetManager().Provider;
            var manager = DynamicModuleManager.GetManager();

            var dynamicType = provider.GetDynamicModuleType(Guid.Parse(request.TypeId));
            var type = TypeResolutionService.ResolveType(dynamicType.GetFullTypeName());

            var query = manager.GetDataItems(type).Where(di => di.Status == ContentLifecycleStatus.Live);
            foreach (DynamicContent item in query)
            {
                dynamicContentList.Add(new DynamicContentResponse()
                {
                     Id = item.Id,
                     Title = item.GetValue<Lstring>(dynamicType.MainShortTextFieldName).Value,
                     Author = item.Author,
                     LastModified = item.LastModified,
                     CanonicalUrl = item.SystemUrl
                });
            }

            return dynamicContentList.ToArray();
        }
    }
}
