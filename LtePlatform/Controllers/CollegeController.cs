using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LtePlatform.Controllers
{
    public class CollegeController : Controller
    {
        // GET: College
        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        
        public ActionResult Coverage()
        {
            return View();
        }
        
        public ActionResult PreciseKpi()
        {
            return View();
        }
    }
}