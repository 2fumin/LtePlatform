using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LtePlatform.Areas.TestPage.Controllers
{
    public class QUnitTestController : Controller
    {
        // GET: TestPage/QUnitTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LegacyMarkup()
        {
            return View();
        }

        public ActionResult NoQUnitMarkup()
        {
            return View();
        }

        public ActionResult SingleTestId()
        {
            return View();
        }

        public ActionResult AutoStart()
        {
            return View();
        }

        public ActionResult Headless()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Logs()
        {
            return View();
        }

        public ActionResult Only()
        {
            return View();
        }
    }
}