using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService
{
    public class TownQueryService
    {
        private readonly ITownRepository _repository;
        private readonly IRegionRepository _regionRepository;

        public TownQueryService(ITownRepository repository, IRegionRepository regionRepository)
        {
            _repository = repository;
            _regionRepository = regionRepository;
        }

        public List<string> GetCities()
        {
            return _repository.GetAll().Select(x => x.CityName).Distinct().ToList();
        }

        public IEnumerable<string> GetRegions(string city)
        {
            return _regionRepository.GetAllList().Where(x => x.City == city)
                .Select(x => x.Region).Distinct().OrderBy(x => x);
        }  
    }
}
