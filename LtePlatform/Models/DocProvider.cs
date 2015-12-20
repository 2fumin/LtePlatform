using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using LtePlatform.Areas.HelpPage.ModelDescriptions;

namespace LtePlatform.Models
{
    public class DocProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var doc = "";
            var attr = parameterDescriptor.ActionDescriptor
                        .GetCustomAttributes<ApiParameterDocAttribute>()
                        .FirstOrDefault(p => p.Parameter == parameterDescriptor.ParameterName);
            if (attr != null)
            {
                doc = attr.Documentation;
            }
            return doc;
        }

        public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var doc = "";
            var attr = actionDescriptor.GetCustomAttributes<ApiResponseAttribute>().FirstOrDefault();
            if (attr != null)
            {
                doc = attr.Documentation;
            }
            return doc;
        }

        public string GetDocumentation(HttpControllerDescriptor controllerDescriptor)
        {
            var doc = "";
            var attr = controllerDescriptor.GetCustomAttributes<ApiControlAttribute>().FirstOrDefault();
            if (attr != null)
            {
                doc = attr.Documentation;
            }
            return doc;
        }

        public string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var doc = "";
            var attr = actionDescriptor.GetCustomAttributes<ApiDocAttribute>().FirstOrDefault();
            if (attr != null)
            {
                doc = attr.Documentation;
            }
            return doc;
        }

        public string GetDocumentation(MemberInfo member)
        {
            return "To be supported....";
        }

        public string GetDocumentation(Type type)
        {
            return "To be supported...";
        }
    }
}
