using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LtePlatform.Controllers
{
    public class ParametersController : Controller
    {
        // GET: Parameters
        public ActionResult List()
        {
            return View();
        }

        public ActionResult AlarmImport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlarmPost()
        {
            return View("AlarmImport");
        }
    }
}