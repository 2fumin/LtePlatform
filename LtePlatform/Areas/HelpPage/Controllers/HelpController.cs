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
            return Json(description.Select(api => new
            {
                FriendlyId = api.GetFriendlyId(),
                MethodName = api.HttpMethod.Method,
                RelativePath = api.RelativePath,
                Documentation = api.Documentation
            }), 
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Api(string apiId)
        {
            return Json(Configuration.GetHelpPageApiModel(apiId), JsonRequestBehavior.AllowGet);
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