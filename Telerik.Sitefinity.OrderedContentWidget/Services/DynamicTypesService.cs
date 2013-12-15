using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.ServiceInterface;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.OrderedContentWidget.Services
{
    public class DynamicTypesService : Service
    {
        public object Get(DynamicTypeRequest request)
        {
            var dynamicTypes = new List<DynamicTypeResponse>();
            var provider = ModuleBuilderManager.GetManager().Provider;

            foreach (DynamicModuleType dmType in provider.GetDynamicModuleTypes())
            {
                dynamicTypes.Add(new DynamicTypeResponse()
                {
                    Id = dmType.Id,
                    Title = dmType.DisplayName,
                    ClrType = dmType.GetFullTypeName(),
                    SupportedTaxonomyIds = this.GetSupportedTaxonomyIds(dmType, provider)
                });
            }

            return dynamicTypes.ToArray();
        }

        private Guid[] GetSupportedTaxonomyIds(DynamicModuleType type, ModuleBuilderDataProvider provider)
        {
            var supportedTaxonomyIds = new List<Guid>();
            var fields = provider.GetDynamicModuleFields().Where(f => f.ParentTypeId == type.Id);
            foreach (var field in fields)
            {
                if (field.FieldType == FieldType.Classification)
                    supportedTaxonomyIds.Add(field.ClassificationId);
            }

            return supportedTaxonomyIds.ToArray();
        }

    }
}
