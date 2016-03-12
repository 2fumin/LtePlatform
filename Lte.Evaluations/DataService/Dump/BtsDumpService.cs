using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.ExcelCsv;

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

        public int DumpBtsExcels(IEnumerable<BtsExcel> infos)
        {
            var containers = (from info in infos
                             join town in _townRepository.GetAllList()
                                 on new { info.DistrictName, info.TownName } equals
                                 new { town.DistrictName, town.TownName }
                             select new BtsExcelWithTownIdContainer
                             {
                                 BtsExcel = info,
                                 TownId = town.Id
                             }).ToArray();

            if (!containers.Any()) return 0;
            var items =
                Mapper.Map<IEnumerable<BtsExcelWithTownIdContainer>, List<BtsWithTownIdContainer>>(containers);
            items.ForEach(x => { x.CdmaBts.TownId = x.TownId; });
            
            var count = 0;
            foreach (var bts in items.Select(x => x.CdmaBts).ToList())
            {
                if (_btsRepository.Insert(bts) != null)
                    count++;
            }
            _btsRepository.SaveChanges();
            return count;
        }

        public bool DumpSingleBtsExcel(BtsExcel info)
        {
            var bts = CdmaBts.ConstructBts(info, _townRepository);
            if (bts == null)
            {
                bts = CdmaBts.ConstructBts(info, _townRepository);
                var result = _btsRepository.Insert(bts);
                if (result == null) return false;
                var item = BasicImportService.BtsExcels.FirstOrDefault(x => x.BtsId == info.BtsId);
                if (item != null)
                {
                    BasicImportService.BtsExcels.Remove(item);
                }
                _btsRepository.SaveChanges();
                return true;
            }
            bts.IsInUse = true;
            _btsRepository.Update(bts);
            _btsRepository.SaveChanges();
            return true;
        }

        public void VanishBtss(ENodebIdsContainer container)
        {
            foreach (
                var bts in
                    container.ENodebIds.Select(btsId => _btsRepository.GetByBtsId(btsId))
                        .Where(bts => bts != null))
            {
                bts.IsInUse = false;
                _btsRepository.Update(bts);
            }
            _btsRepository.SaveChanges();
        }
    }
}
