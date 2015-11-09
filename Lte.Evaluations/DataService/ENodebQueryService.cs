using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Concrete;
using Lte.Domain.Common.Geo;

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

        public List<ENodeb> GetAllWithIds(IEnumerable<int> ids)
        {
            return (from a in _eNodebRepository.GetAll()
                    join b in ids on a.ENodebId equals b
                    select a).OrderBy(x => x.ENodebId).ToList();
        }

        public List<ENodeb> GetAllWithNames(string city, string district, string town, string eNodebName,
            string address)
        {
            IEnumerable<Town> _townList = _townRepository.QueryTowns(city, district, town).ToList();
            return (!_townList.Any())
                ? null
                : Query(eNodebName, address, _townList).ToList();
        }

        private IEnumerable<ENodeb> Query(string eNodebName, string address, IEnumerable<Town> _townList)
        {
            return _eNodebRepository.GetAllList().Where(x =>
                _townList.FirstOrDefault(t => t.Id == x.TownId) != null
                && (string.IsNullOrEmpty(eNodebName) || x.Name.IndexOf(eNodebName.Trim(),
                    StringComparison.Ordinal) >= 0)
                && (string.IsNullOrEmpty(address) || x.Address.IndexOf(address.Trim(),
                    StringComparison.Ordinal) >= 0));
        }

        public List<ENodeb> GetAllWithNames(ITownRepository townRepository, ITown town, 
            string eNodebName, string address)
        {
            return GetAllWithNames(town.CityName, town.DistrictName, town.TownName,
                eNodebName, address);
        }
    }
}
