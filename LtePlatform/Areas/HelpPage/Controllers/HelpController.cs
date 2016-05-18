using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using LtePlatform.Areas.HelpPage.ModelDescriptions;
using LtePlatform.Areas.HelpPage.Models;

namespace LtePlatform.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; }

        public JsonResult ApiDescriptions()
        {
            var provider = Configuration.Services.GetDocumentationProvider();
            return Json(Configuration.Services.GetApiExplorer().ApiDescriptions.Select(api =>
            {
                var descriptor = api.ActionDescriptor.ControllerDescriptor;
                return new
                {
                    ControllerName = descriptor.ControllerName,
                    ControllerType = descriptor.ControllerType.ToString(),
                    Documentation = provider.GetDocumentation(descriptor)
                };
            }).Distinct(),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApiMethod(string controllerName)
        {
            var description =
                Configuration.Services.GetApiExplorer()
                    .ApiDescriptions.Where(
                        api => api.ActionDescriptor.ControllerDescriptor.ControllerName == controllerName);
            var modelGenerator = Configuration.GetModelDescriptionGenerator();
            return Json(description.Select(api => new
            {
                FriendlyId = api.GetFriendlyId(),
                MethodName = api.HttpMethod.Method,
                RelativePath = api.RelativePath,
                Documentation = api.Documentation,
                ResponseName = api.GenerateResourceDescription(modelGenerator)?.Name
            }), 
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ApiActionDoc(string apiId)
        {
            var description = Configuration.Services.GetApiExplorer().ApiDescriptions
                .FirstOrDefault(x => x.GetFriendlyId() == apiId);
            if (description == null) return null;
            var modelGenerator = Configuration.GetModelDescriptionGenerator();
            var sampleGenerator = Configuration.GetHelpPageSampleGenerator();
            var parametersDescriptions = description.GenerateUriParameters(modelGenerator);
            var requestModelDescription = description.GenerateRequestModelDescription(modelGenerator, sampleGenerator);
            var responseModel = description.GenerateResourceDescription(modelGenerator);
            return Json(new
            {
                ParameterDescriptions = parametersDescriptions.Select(x => new
                {
                    x.Name,
                    x.Documentation,
                    TypeDocumentation = x.TypeDescription.Documentation,
                    TypeName = x.TypeDescription.Name,
                    AnnotationDoc = x.Annotations.Select(an => an.Documentation)
                }),
                FromBodyModel = requestModelDescription==null?null: new
                {
                    requestModelDescription.Name,
                    Type = requestModelDescription.ModelType.ToString(),
                    requestModelDescription.Documentation,
                    requestModelDescription.ParameterDocumentation
                },
                ResponseModel = responseModel==null?null: new
                {
                    responseModel.Name,
                    responseModel.Documentation,
                    responseModel.ParameterDocumentation
                }
            },
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApiDescriptions()
        {
            return Json(Configuration.Services.GetApiExplorer().ApiDescriptions.Select(x=> new 
            {
                x.Documentation,
                Id = x.GetFriendlyId()
            }), 
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApiDetails(string apiId)
        {
            return Json(Configuration.GetHelpPageApiModel(apiId)?.UriParameters.Select(des=>new
            {
                des.Name,
                Annotations = des.Annotations.Select(x=>x.Documentation),
                des.Documentation,
                TypeDocumentation = des.TypeDescription.Documentation,
                TypeName = des.TypeDescription.Name,
                Type = des.TypeDescription.ModelType.ToString()
            }), 
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}