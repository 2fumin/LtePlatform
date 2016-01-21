using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class BtsQueryService
    {
        private readonly ITownRepository _townRepository;
        private readonly IBtsRepository _btsRepository;

        public BtsQueryService(ITownRepository townRepository, IBtsRepository btsRepository)
        {
            _townRepository = townRepository;
            _btsRepository = btsRepository;
        }

        public IEnumerable<CdmaBtsView> GetByTownNames(string city, string district, string town)
        {
            var townItem =
                _townRepository.FirstOrDefault(
                    x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            return townItem == null
                ? null
                : Mapper.Map<List<CdmaBts>, IEnumerable<CdmaBtsView>>(
                    _btsRepository.GetAll().Where(x => x.TownId == townItem.Id).ToList());
        }

        public IEnumerable<CdmaBtsView> GetByGeneralName(string name)
        {
            var items =
                _btsRepository.GetAllList().Where(x => x.Name.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0).ToArray();
            if (items.Any())
                return Mapper.Map<IEnumerable<CdmaBts>, IEnumerable<CdmaBtsView>>(items);
            var btsId = name.Trim().ConvertToInt(0);
            if (btsId > 0)
            {
                items = _btsRepository.GetAll().Where(x => x.BtsId == btsId).ToArray();
                if (items.Any())
                    return Mapper.Map<IEnumerable<CdmaBts>, IEnumerable<CdmaBtsView>>(items);
            }
            items =
                _btsRepository.GetAllList()
                    .Where(
                        x =>
                            x.Address.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0)
                    .ToArray();
            if (items.Any())
                return Mapper.Map<IEnumerable<CdmaBts>, IEnumerable<CdmaBtsView>>(items);
            return null;
        }

        public CdmaBtsView GetByBtsId(int btsId)
        {
            var item = _btsRepository.GetByBtsId(btsId);
            return item == null ? null : Mapper.Map<CdmaBts, CdmaBtsView>(item);
        }
    }
}
