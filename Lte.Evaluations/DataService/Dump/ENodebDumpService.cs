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
    public class ENodebDumpService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;

        public ENodebDumpService(IENodebRepository eNodebRepository, ITownRepository townRepository)
        {
            _eNodebRepository = eNodebRepository;
            _townRepository = townRepository;
        }

        public int DumpNewEnodebExcels(IEnumerable<ENodebExcel> infos)
        {
            var containers = (from info in infos
                join town in _townRepository.GetAllList()
                    on new {info.CityName, info.DistrictName, info.TownName} equals
                    new {town.CityName, town.DistrictName, town.TownName}
                select new ENodebExcelWithTownIdContainer
                {
                    ENodebExcel = info,
                    TownId = town.Id
                }).ToArray();

            if (!containers.Any()) return 0;
            var items =
                Mapper.Map<IEnumerable<ENodebExcelWithTownIdContainer>, List<ENodebWithTownIdContainer>>(containers);
            items.ForEach(x => { x.ENodeb.TownId = x.TownId; });

            var count = 0;
            foreach (var eNodeb in items.Select(x => x.ENodeb).ToList())
            {
                var item = _eNodebRepository.GetByENodebId(eNodeb.ENodebId);
                if (item == null)
                {
                    var result = _eNodebRepository.Insert(eNodeb);
                    if (result != null) count++;
                }
                else
                {
                    item.IsInUse = true;
                    _eNodebRepository.Update(item);
                }
            }
            return count;
        }

        public bool DumpSingleENodebExcel(ENodebExcel info)
        {
            var eNodeb = _eNodebRepository.GetByENodebId(info.ENodebId);
            if (eNodeb == null)
            {
                eNodeb = ENodeb.ConstructENodeb(info, _townRepository);
                var result = _eNodebRepository.Insert(eNodeb);
                if (result != null)
                {
                    var item = BasicImportService.ENodebExcels.FirstOrDefault(x => x.ENodebId == info.ENodebId);
                    if (item != null)
                    {
                        BasicImportService.ENodebExcels.Remove(item);
                    }
                    return true;
                }
            }
            else
            {
                eNodeb.IsInUse = true;
                _eNodebRepository.Update(eNodeb);
                return true;
            }

            return false;
        }

        public void VanishENodebs(ENodebIdsContainer container)
        {
            foreach (var eNodebId in container.ENodebIds)
            {
                var eNodeb = _eNodebRepository.GetByENodebId(eNodebId);
                if (eNodeb != null)
                {
                    eNodeb.IsInUse = false;
                    _eNodebRepository.Update(eNodeb);
                }
            }
        }
    }
}
