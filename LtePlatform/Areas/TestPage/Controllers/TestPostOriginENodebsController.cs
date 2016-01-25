using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Lte.Evaluations.DataService.Dump;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using LtePlatform.Models;

namespace LtePlatform.Areas.TestPage.Controllers
{
    [ApiControl("测试回发原有基站信息控制器")]
    public class TestPostOriginENodebsController : ApiController
    {
        [HttpPost]
        [ApiDoc("回发原有基站信息")]
        [ApiParameterDoc("container", "新基站信息容器")]
        [ApiResponse("基站Excel信息列表")]
        public IEnumerable<ENodebExcel> Post(NewENodebListContainer container)
        {
            return container.Infos;
        }
    }

    [ApiControl("测试回发基站信息控制器")]
    public class TestPostBackENodebsController : ApiController
    {
        private readonly ITownRepository _townRepository;

        public TestPostBackENodebsController(ITownRepository townRepository)
        {
            _townRepository = townRepository;
        }

        [HttpPost]
        [ApiDoc("回发基站信息")]
        [ApiParameterDoc("container", "新基站信息容器")]
        [ApiResponse("基站信息列表")]
        public IEnumerable<ENodeb> Post(NewENodebListContainer container)
        {
            var containers = from info in container.Infos
                             join town in _townRepository.GetAllList()
                                 on new { info.CityName, info.DistrictName, info.TownName } equals
                                 new { town.CityName, town.DistrictName, town.TownName }
                             select new ENodebExcelWithTownIdContainer
                             {
                                 ENodebExcel = info,
                                 TownId = town.Id
                             };

            if (!containers.Any()) return null;
            var items =
                Mapper.Map<IEnumerable<ENodebExcelWithTownIdContainer>, List<ENodebWithTownIdContainer>>(containers);
            items.ForEach(x => { x.ENodeb.TownId = x.TownId; });

            return items.Select(x => x.ENodeb);
        }
    }
}
