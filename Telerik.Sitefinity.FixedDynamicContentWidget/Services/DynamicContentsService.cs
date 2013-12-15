using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.ServiceInterface;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using System.Web.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class DynamicContentsService : Service
    {
        public object Get(DynamicContentsRequest request)
        {
            var dynamicContentList = new List<DynamicContentResponse>();
            var provider = ModuleBuilderManager.GetManager().Provider;
            var manager = DynamicModuleManager.GetManager();

            var dynamicType = provider.GetDynamicModuleType(Guid.Parse(request.TypeId));
            var type = TypeResolutionService.ResolveType(dynamicType.GetFullTypeName());
            int virtualCount = 0;

            if (request.SelectedContentIds != null)
            {
                virtualCount = request.SelectedContentIds.Length;
                foreach (var id in request.SelectedContentIds)
                {
                    var item = manager.GetDataItem(type, id);
                    dynamicContentList.Add(this.ToResponse(item, dynamicType));
                }
            }
            else
            {
                string filterExpression = null;
                if (request.QueryData != null)
                {
                    var converter = new JsonTypeConverter<QueryData>();
                    var queryData = (QueryData)converter.ConvertFromString(request.QueryData);
                    filterExpression = DefinitionsHelper.GetFilterExpression(string.Empty, queryData);
                }

                var query = manager.GetDataItems(type).Where(di => di.Status == ContentLifecycleStatus.Live);

                if (filterExpression != null)
                    query = query.Where(filterExpression);

                virtualCount = query.Count();

                if (request.Skip.HasValue)
                    query = query.Skip(request.Skip.Value);

                if (request.Take.HasValue)
                    query = query.Take(request.Take.Value);
                else
                    query = query.Take(20);

                foreach (DynamicContent item in query)
                {
                    dynamicContentList.Add(this.ToResponse(item, dynamicType));
                }
                
            }

            return new DynamicContentsResponse()
            {
                 Items = dynamicContentList,
                 VirtualCount = virtualCount
            };
        }

        private DynamicContentResponse ToResponse(DynamicContent dynamicContent, DynamicModuleType dynamicType)
        {
            return new DynamicContentResponse()
            {
                Id = dynamicContent.Id,
                Title = dynamicContent.GetValue<Lstring>(dynamicType.MainShortTextFieldName).Value,
                Author = this.GetAuthorName(dynamicContent.LastModifiedBy),
                LastModified = dynamicContent.LastModified,
                CanonicalUrl = dynamicContent.SystemUrl
            };
        }

        private string GetAuthorName(Guid authorId)
        {
            if (this.userManager == null)
                this.userManager = UserManager.GetManager();

            var user = this.userManager.GetUser(authorId);
            return string.Concat(user.FirstName, " ", user.LastName);
        }

        private UserManager userManager;

    }
}
