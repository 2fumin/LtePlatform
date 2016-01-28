using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace LtePlatform.Controllers.Kpi
{
    public class PreciseTestController : ApiController
    {
        private readonly PreciseImportService _service;
        private readonly ITownRepository _townRepository;

        public PreciseTestController(PreciseImportService service, ITownRepository townRepository)
        {
            _service = service;
            _townRepository = townRepository;
        }

        public int Get()
        {
            return _service.GetTownStats(PreciseImportService.PreciseCoverage4Gs.ToList()).Count();
        }

        public IEnumerable<TownPreciseCoverage4GStat> Get(DateTime statTime)
        {
            return _service.GetMergeStats(_service.GetTownStats(PreciseImportService.PreciseCoverage4Gs.ToList()),
                statTime);
        }

        public List<TownPreciseView> Get(DateTime statTime, int stamp)
        {
            return _service.GetMergeStats(_service.GetTownStats(PreciseImportService.PreciseCoverage4Gs.ToList()),
                statTime).Select(x => TownPreciseView.ConstructView(x, _townRepository)).ToList();
        }
    }
}
