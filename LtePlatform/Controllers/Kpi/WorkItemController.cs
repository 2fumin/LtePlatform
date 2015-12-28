using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities.Work;

namespace LtePlatform.Controllers.Kpi
{
    public class WorkItemController : ApiController
    {
        private readonly WorkItemService _service;

        public WorkItemController(WorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<WorkItem> Get()
        {
            return _service.QueryAllList();
        }

        [HttpGet]
        public int Get(string statCondition, string typeCondition, int itemsPerPage)
        {
            return _service.QueryTotalPages(statCondition, typeCondition, itemsPerPage);
        }

        [HttpGet]
        public IEnumerable<WorkItemView> Get(string statCondition, string typeCondition, int itemsPerPage,
            int page)
        {
            return _service.QueryViews(statCondition, typeCondition, itemsPerPage, page);
        }
    }
}
