using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    public class BtsDumpService
    {
        private readonly IBtsRepository _btsRepository;
        private readonly ITownRepository _townRepository;

        public BtsDumpService(IBtsRepository btsRepository, ITownRepository townRepository)
        {
            _btsRepository = btsRepository;
            _townRepository = townRepository;
        }

        public void DumpBtsExcels(IEnumerable<BtsExcel> infos)
        {
            var containers = from info in infos
                             join town in _townRepository.GetAllList()
                                 on new { info.DistrictName, info.TownName } equals
                                 new { town.DistrictName, town.TownName }
                             select new BtsExcelWithTownIdContainer
                             {
                                 BtsExcel = info,
                                 TownId = town.Id
                             };

            if (!containers.Any()) return;
            var items =
                Mapper.Map<IEnumerable<BtsExcelWithTownIdContainer>, List<BtsWithTownIdContainer>>(containers);
            items.ForEach(x => { x.CdmaBts.TownId = x.TownId; });

            items.Select(x => x.CdmaBts).ToList().ForEach(x => _btsRepository.InsertAsync(x));
        }
    }
}
