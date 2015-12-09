using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Concrete;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.ViewModels;
using Lte.Domain.Regular;

namespace Lte.Evaluations.DataService
{
    public class ENodebQueryService
    {
        private readonly ITownRepository _townRepository;
        private readonly IENodebRepository _eNodebRepository;

        public ENodebQueryService(ITownRepository townRepository, IENodebRepository eNodebRepository)
        {
            _townRepository = townRepository;
            _eNodebRepository = eNodebRepository;
        }

        public IEnumerable<ENodebView> GetByTownNames(string city, string district, string town)
        {
            var townItem =
                _townRepository.FirstOrDefault(
                    x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            return townItem == null
                ? null
                : Mapper.Map<List<ENodeb>, IEnumerable<ENodebView>>(
                    _eNodebRepository.GetAll().Where(x => x.TownId == townItem.Id).ToList());
        }

        public IEnumerable<ENodebView> GetByGeneralName(string name)
        {
            var items =
                _eNodebRepository.GetAllList().Where(x => x.Name.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0).ToArray();
            if (items.Any())
                return Mapper.Map<IEnumerable<ENodeb>, IEnumerable<ENodebView>>(items);
            var eNodebId = name.Trim().ConvertToInt(0);
            if (eNodebId > 0)
            {
                items = _eNodebRepository.GetAll().Where(x => x.ENodebId == eNodebId).ToArray();
                if (items.Any())
                    return Mapper.Map<IEnumerable<ENodeb>, IEnumerable<ENodebView>>(items);
            }
            items =
                _eNodebRepository.GetAllList()
                    .Where(
                        x =>
                            x.Address.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0 ||
                            x.PlanNum.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0)
                    .ToArray();
            if (items.Any())
                return Mapper.Map<IEnumerable<ENodeb>, IEnumerable<ENodebView>>(items);
            return null;
        }
    }
}
