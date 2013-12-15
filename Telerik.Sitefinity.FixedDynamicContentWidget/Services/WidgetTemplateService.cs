using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.FixedDynamicContentWidget.Services
{
    public class WidgetTemplateService : Service
    {
        public object Get(WidgetTemplateRequest req)
        {
            var pageManager = PageManager.GetManager();
            IQueryable<ControlPresentation> allTemplates = pageManager.GetPresentationItems<ControlPresentation>();
            
            var layoutTemplates = allTemplates.Where(tmpl => tmpl.DataType == Presentation.AspNetTemplate);
            int? totalCount = 0;

            var viewType = req.IsMaster ? typeof(DynamicContentViewMaster).FullName : typeof(DynamicContentViewDetail).FullName;

            var provider = ModuleBuilderManager.GetManager().Provider;
            var dynamicTypeName = provider.GetDynamicModuleType(req.DynamicTypeId).GetFullTypeName();

            string filterExpression = String.Format(@"ControlType == ""{0}"" AND Condition == ""{1}""", viewType, dynamicTypeName);
            
            layoutTemplates = DataProviderBase.SetExpressions(layoutTemplates, filterExpression, "Name", 0, 0, ref totalCount);

            var widgetTemplateReponses = new List<WidgetTemplateResponse>();
            foreach (var template in layoutTemplates)
            {
                widgetTemplateReponses.Add(new WidgetTemplateResponse()
                {
                    TemplateName = template.Name,
                    TemplateId = template.Id
                });
            }

            return widgetTemplateReponses.ToArray();
        }
    }
}
