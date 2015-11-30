using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lte.Evaluations.DataService;
using LtePlatform.Models;

namespace LtePlatform.Controllers
{
    public class ParametersController : Controller
    {
        private readonly BasicImportService _basicImportService;

        public ParametersController(BasicImportService basicImportService)
        {
            _basicImportService = basicImportService;
        }

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

        public ActionResult BasicImport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BasicPost()
        {
            var messages = new List<string>();
            var lteFile = Request.Files["lteExcel"];
            if (lteFile != null && lteFile.FileName != "")
            {
                var ltePath = lteFile.UploadParametersFile();
                _basicImportService.ImportLteParameters(ltePath);
                messages.Add("完成LTE工参文件读取！");
            }
            else
            {
                messages.Add("LTE工参文件无选择！");
            }
            var cdmaFile = Request.Files["cdmaExcel"];
            if (cdmaFile != null && cdmaFile.FileName != "")
            {
                var cdmaPath = cdmaFile.UploadParametersFile();
                _basicImportService.ImportCdmaParameters(cdmaPath);
                messages.Add("完成CDMA工参文件读取！");
            }
            else
            {
                messages.Add("CDMA工参文件无选择！");
            }
            return View("BasicImport");
        }

        public ActionResult NeighborImport()
        {
            return View();
        }
    }
}