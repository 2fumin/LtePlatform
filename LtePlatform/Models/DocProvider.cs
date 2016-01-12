using System;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using Lte.Domain.Regular.Attributes;
using LtePlatform.Areas.HelpPage.ModelDescriptions;

namespace LtePlatform.Models
{
    public class DocProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var attr = parameterDescriptor.ActionDescriptor
                        .GetCustomAttributes<ApiParameterDocAttribute>()
                        .FirstOrDefault(p => p.Parameter == parameterDescriptor.ParameterName);
            return attr != null ? attr.Documentation : "";
        }

        public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var attr = actionDescriptor.GetCustomAttributes<ApiResponseAttribute>().FirstOrDefault();
            return attr != null ? attr.Documentation : "";
        }

        public string GetDocumentation(HttpControllerDescriptor controllerDescriptor)
        {
            var attr = controllerDescriptor.GetCustomAttributes<ApiControlAttribute>().FirstOrDefault();
            return attr != null ? attr.Documentation : "";
        }

        public string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var attr = actionDescriptor.GetCustomAttributes<ApiDocAttribute>().FirstOrDefault();
            return attr != null ? attr.Documentation : "";
        }

        public string GetDocumentation(MemberInfo member)
        {
            var attr = member.GetCustomAttributes<MemberDocAttribute>().FirstOrDefault();
            return attr != null ? attr.Documentation : "";
        }

        public string GetDocumentation(Type type)
        {
            var attr = type.GetCustomAttributes<TypeDocAttribute>().FirstOrDefault();
            return attr != null ? attr.Documentation : "";
        }
    }
}
