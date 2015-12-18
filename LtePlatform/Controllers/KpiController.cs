 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
 using System.Text;
 using System.Web;
using System.Web.Mvc;
using Lte.Domain.Common;
using Lte.Evaluations.DataService;
 using LtePlatform.Models;

namespace LtePlatform.Controllers
{
    public class KpiController : Controller
    {
        private readonly TownQueryService _townService;
        private readonly KpiImportService _importService;
        private readonly PreciseImportService _preciseImportService;

        public KpiController(TownQueryService townService, KpiImportService importService,
            PreciseImportService preciseImportService)
        {
            _townService = townService;
            _importService = importService;
            _preciseImportService = preciseImportService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Precise4G()
        {
            return View();
        }

        public ActionResult PreciseTrend()
        {
            return View();
        }

        public ActionResult PreciseTop()
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
            var message = new List<string>();
            var httpPostedFileBase = Request.Files["dailyKpi"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var fields = httpPostedFileBase.FileName.GetSplittedFields(new [] {'.', '\\'});
                var city = fields[fields.Length - 2];
                var legalCities = _townService.GetCities();
                if (legalCities.Count > 0 && legalCities.FirstOrDefault(x => x == city) == null)
                {
                    ViewBag.WarningMessage = "上传文件名对应的城市" + city + "找不到。使用'" + legalCities[0] + "'代替";
                    city = legalCities[0];
                }
                var regions = _townService.GetRegions(city);
                var path = httpPostedFileBase.UploadKpiFile();
                message = _importService.Import(path, regions);
            }
            ViewBag.Message = message;
            return View("Import");
        }

        public ActionResult PreciseImport()
        {
            return View();
        }

        [HttpPost]
        public ViewResult PrecisePost()
        {
            var message = new List<string>();
            var httpPostedFileBase = Request.Files["preciseFile"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var reader = new StreamReader(httpPostedFileBase.InputStream, Encoding.GetEncoding("GB2312"));
                _preciseImportService.UploadItems(reader);
                ViewBag.Message = "成功上传精确覆盖率文件" + httpPostedFileBase.FileName;
            }
            ViewBag.Message = message;
            return View("PreciseImport");
        }
    }
}