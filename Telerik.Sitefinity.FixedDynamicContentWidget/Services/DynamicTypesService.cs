using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.ServiceInterface;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
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
                     Title = dmType.DisplayName,
                     Id = dmType.Id
                });
            }

            return dynamicTypes.ToArray();
        }
    }
}
