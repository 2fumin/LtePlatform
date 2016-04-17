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
            return Json(Configuration.Services.GetApiExplorer().ApiDescriptions.Select(api =>
            {
                var descriptor = api.ActionDescriptor.ControllerDescriptor;
                return new
                {
                    ControllerName = descriptor.ControllerName,
                    ControllerType = descriptor.ControllerType.ToString(),
                    FriendlyId= api.GetFriendlyId(),
                    MethodName = api.HttpMethod.Method,
                    RelativePath = api.RelativePath,
                    Documentation = api.Documentation
                };
            }), 
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View();
        }

        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
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