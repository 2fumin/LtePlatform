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

        public TownQueryService(ITownRepository repository)
        {
            _repository = repository;
        }

        public List<string> GetCities()
        {
            return _repository.GetAll().Select(x => x.CityName).Distinct().ToList();
        }
    }
}
