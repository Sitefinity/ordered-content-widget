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

            if (request.SelectedContentIds != null)
            {
                foreach (var id in request.SelectedContentIds)
                {
                    var item = manager.GetDataItem(type, id);
                    dynamicContentList.Add(this.ToResponse(item, dynamicType));
                }
            }
            else
            {
                var query = manager.GetDataItems(type).Where(di => di.Status == ContentLifecycleStatus.Live);
                foreach (DynamicContent item in query)
                {
                    dynamicContentList.Add(this.ToResponse(item, dynamicType));
                }
            }

            return dynamicContentList.ToArray();
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
