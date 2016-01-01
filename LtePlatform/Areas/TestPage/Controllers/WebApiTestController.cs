using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LtePlatform.Areas.TestPage.Controllers
{
    public class WebApiTestController : Controller
    {
        // GET: TestPage/WebApiTest
        public ActionResult SimpleType()
        {
            return View();
        }

        public ActionResult BasicPost()
        {
            return View();
        }

        public ActionResult Html5Test()
        {
            return View();
        }

        public ActionResult Html5PostTest()
        {
            return View();
        }
    }
}