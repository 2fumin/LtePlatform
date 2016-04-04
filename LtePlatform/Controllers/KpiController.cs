 using System.Collections.Generic;
 using System.IO;
 using System.Linq;
 using System.Text;
 using System.Web;
 using System.Web.Mvc;
 using Lte.Domain.Common;
 using Lte.Evaluations.DataService;
 using Lte.Evaluations.DataService.Kpi;
 using Lte.Evaluations.DataService.Mr;
 using LtePlatform.Models;

namespace LtePlatform.Controllers
{
    public class KpiController : Controller
    {
        private readonly TownQueryService _townService;
        private readonly KpiImportService _importService;
        private readonly PreciseImportService _preciseImportService;
        private readonly WorkItemService _workItemService;
        private readonly InterferenceMatrixService _interferenceMatrix;

        public KpiController(TownQueryService townService, KpiImportService importService,
            PreciseImportService preciseImportService, WorkItemService workItemService,
            InterferenceMatrixService interferenceMatrix)
        {
            _townService = townService;
            _importService = importService;
            _preciseImportService = preciseImportService;
            _workItemService = workItemService;
            _interferenceMatrix = interferenceMatrix;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult WorkItem()
        {
            return View();
        }
        
        [Authorize]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

        [Authorize]
        public ActionResult PreciseImport()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

        [Authorize]
        public ActionResult WorkItemImport()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult WorkItemPost()
        {
            var httpPostedFileBase = Request.Files["workItem"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _workItemService.ImportExcelFiles(path);
            }
            return View("WorkItemImport");
        }

        [Authorize]
        public ActionResult InterferenceImport()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult InterferencePost(HttpPostedFileBase[] files)
        {
            if (files == null || files.Length <= 0 || string.IsNullOrEmpty(files[0]?.FileName))
                return View("InterferenceImport");
            ViewBag.Message = "共上传干扰矩阵信息文件" + files.Length + "个！";
            foreach (var file in files)
            {
                _interferenceMatrix.UploadInterferenceStats(
                    new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")), file.FileName);
            }
            return View("InterferenceImport");
        }
    }
}