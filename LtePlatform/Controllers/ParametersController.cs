using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers
{
    [Authorize]
    public class ParametersController : Controller
    {
        private readonly BasicImportService _basicImportService;
        private readonly AlarmsService _alarmsService;
        private readonly NearestPciCellService _neighborService;
        private readonly FlowService _flowService;

        public ParametersController(BasicImportService basicImportService, AlarmsService alarmsService,
            NearestPciCellService neighborService, FlowService flowService)
        {
            _basicImportService = basicImportService;
            _alarmsService = alarmsService;
            _neighborService = neighborService;
            _flowService = flowService;
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
        public ActionResult ZteAlarmPost(HttpPostedFileBase[] alarmZte)
        {
            if (alarmZte == null || alarmZte.Length <= 0 || string.IsNullOrEmpty(alarmZte[0]?.FileName))
                return View("AlarmImport");
            ViewBag.Message = "共上传中兴告警信息文件" + alarmZte.Length + "个！";
            foreach (var file in alarmZte)
            {
                _alarmsService.UploadZteAlarms(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
            }
            return View("AlarmImport");
        }

        [HttpPost]
        public ActionResult HwAlarmPost(HttpPostedFileBase[] alarmHw)
        {
            if (alarmHw == null || alarmHw.Length <= 0 || string.IsNullOrEmpty(alarmHw[0]?.FileName))
                return View("AlarmImport");
            ViewBag.Message = "共上传华为告警信息文件" + alarmHw.Length + "个！";
            foreach (var file in alarmHw)
            {
                _alarmsService.UploadHwAlarms(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
            }
            return View("AlarmImport");
        }
        
        public ActionResult BasicImport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LteImportPost()
        {
            var lteFile = Request.Files["lteExcel"];
            if (lteFile != null && lteFile.FileName != "")
            {
                var ltePath = lteFile.UploadParametersFile();
                _basicImportService.ImportLteParameters(ltePath);
            }
            return RedirectToAction("BasicImport");
        }

        [HttpPost]
        public ActionResult CdmaImportPost()
        {
            var cdmaFile = Request.Files["cdmaExcel"];
            if (cdmaFile != null && cdmaFile.FileName != "")
            {
                var cdmaPath = cdmaFile.UploadParametersFile();
                _basicImportService.ImportCdmaParameters(cdmaPath);
            }
            return RedirectToAction("BasicImport");
        }

        [Authorize]
        public ActionResult NeighborImport()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ZteNeighborPost(HttpPostedFileBase[] neighborZte)
        {
            if (neighborZte != null && neighborZte.Length > 0 && !string.IsNullOrEmpty(neighborZte[0]?.FileName))
            {
                ViewBag.Message = "共上传中兴邻区信息文件" + neighborZte.Length + "个！";
                foreach (var file in neighborZte)
                {
                    _neighborService.UploadZteNeighbors(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("NeighborImport");
        }

        [HttpPost]
        public ActionResult HwNeighborPost(HttpPostedFileBase[] neighborHw)
        {
            if (neighborHw != null && neighborHw.Length > 0 && !string.IsNullOrEmpty(neighborHw[0]?.FileName))
            {
                ViewBag.Message = "共上传华为邻区信息文件" + neighborHw.Length + "个！";
                foreach (var file in neighborHw)
                {
                    _neighborService.UploadHwNeighbors(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("NeighborImport");
        }

        [HttpPost]
        public ActionResult HwFlowPost(HttpPostedFileBase[] flowHw)
        {
            if (flowHw != null && flowHw.Length > 0 && !string.IsNullOrEmpty(flowHw[0]?.FileName))
            {
                ViewBag.Message = "共上传华为流量信息文件" + flowHw.Length + "个！";
                foreach (var file in flowHw)
                {
                    var path = file.UploadKpiFile();
                    _flowService.UploadFlowHuaweis(new StreamReader(path, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("NeighborImport");
        }

        [HttpPost]
        public ActionResult ZteFlowPost(HttpPostedFileBase[] flowZte)
        {
            if (flowZte != null && flowZte.Length > 0 && !string.IsNullOrEmpty(flowZte[0]?.FileName))
            {
                ViewBag.Message = "共上传中兴流量信息文件" + flowZte.Length + "个！";
                foreach (var file in flowZte)
                {
                    _flowService.UploadFlowZtes(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("NeighborImport");
        }
    }
}