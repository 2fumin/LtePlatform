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

namespace LtePlatform.Areas.TestPage.Controllers
{
    public class TestPostOriginENodebsController : ApiController
    {
        [HttpPost]
        public IEnumerable<ENodebExcel> Post(NewENodebListContainer container)
        {
            return container.Infos;
        }
    }

    public class TestPostBackENodebsController : ApiController
    {
        private readonly ITownRepository _townRepository;

        public TestPostBackENodebsController(ITownRepository townRepository)
        {
            _townRepository = townRepository;
        }

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
