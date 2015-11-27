using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;

namespace LtePlatform.Controllers.Kpi
{
    public class TopDrop2GController : ApiController
    {
        private readonly TopDrop2GService _service;

        public TopDrop2GController(TopDrop2GService service)
        {
            _service = service;
        }

        public IEnumerable<TopDrop2GCellView> GetViews(DateTime statDate, string city)
        {
            return _service.GetViews(statDate, city);
        }
    }
}
