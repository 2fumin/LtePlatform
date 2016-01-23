using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

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
            var townItem = _townRepository.QueryTown(city, district, town);
            return townItem == null
                ? null
                : _eNodebRepository.GetAll().Where(x => x.TownId == townItem.Id).ToList().MapTo<IEnumerable<ENodebView>>();
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
                if (items.Any()) return items.MapTo<IEnumerable<ENodebView>>();
            }
            items =
                _eNodebRepository.GetAllList()
                    .Where(
                        x =>
                            x.Address.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0 ||
                            x.PlanNum.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0)
                    .ToArray();
            return items.Any() ? items.MapTo<IEnumerable<ENodebView>>() : null;
        }

        public ENodebView GetByENodebId(int eNodebId)
        {
            var item = _eNodebRepository.GetByENodebId(eNodebId);
            return item?.MapTo<ENodebView>();
        }
    }
}
