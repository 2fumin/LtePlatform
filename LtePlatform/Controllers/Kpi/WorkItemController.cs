using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities.Work;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("工单查询控制器")]
    public class WorkItemController : ApiController
    {
        private readonly WorkItemService _service;

        public WorkItemController(WorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有工单列表")]
        [ApiResponse("所有工单列表")]
        public IEnumerable<WorkItemView> Get()
        {
            return _service.QueryViews();
        }

        [HttpGet]
        [ApiDoc("查询指定条件下的工单列表数量")]
        [ApiParameterDoc("statCondition", "工单状态条件")]
        [ApiParameterDoc("typeCondition", "工单类型条件")]
        [ApiResponse("总的数量")]
        public int Get(string statCondition, string typeCondition)
        {
            return _service.QueryTotalItems(statCondition, typeCondition);
        }

        [HttpGet]
        [ApiDoc("查询指定条件下某一页的工单视图列表")]
        [ApiParameterDoc("statCondition", "工单状态条件")]
        [ApiParameterDoc("typeCondition", "工单类型条件")]
        [ApiParameterDoc("itemsPerPage", "每页显示的工单数")]
        [ApiParameterDoc("page", "指定页码")]
        [ApiResponse("某一页的工单视图列表")]
        public IEnumerable<WorkItemView> Get(string statCondition, string typeCondition, int itemsPerPage,
            int page)
        {
            return _service.QueryViews(statCondition, typeCondition, itemsPerPage, page);
        }

        [HttpPut]
        [ApiDoc("更新LTE扇区编号")]
        [ApiResponse("更新扇区编号数")]
        public int Put()
        {
            return _service.UpdateLteSectorIds();
        }
    }
}
