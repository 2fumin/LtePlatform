using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LtePlatform.Controllers
{
    public class KpiController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Precise4G()
        {
            return View();
        }

        public ActionResult TopDrop2G()
        {
            return View();
        }

        public ActionResult TopDrop2GDaily()
        {
            return View();
        }

        public ActionResult TopConnection3G()
        {
            return View();
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ViewResult KpiImport()
        {
            return View("Import");
        }
    }
}