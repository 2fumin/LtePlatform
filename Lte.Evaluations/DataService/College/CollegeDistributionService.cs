using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeDistributionService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly IIndoorDistributioinRepository _indoorRepository;

        public CollegeDistributionService(IInfrastructureRepository repository,
            IIndoorDistributioinRepository indoorRepository)
        {
            _repository = repository;
            _indoorRepository = indoorRepository;
        }

        public IEnumerable<IndoorDistribution> QueryLteDistributions(string collegeName)
        {
            var ids = _repository.GetLteDistributionIds(collegeName);
            var distributions = ids.Select(_indoorRepository.Get).Where(distribution => distribution != null).ToList();
            return distributions;
        }

        public IEnumerable<IndoorDistribution> QueryCdmaDistributions(string collegeName)
        {
            var ids = _repository.GetCdmaDistributionIds(collegeName);
            var distributions = ids.Select(_indoorRepository.Get).Where(distribution => distribution != null).ToList();
            return distributions;
        }

        public IEnumerable<IndoorDistribution> QueryLteDistributions(IEnumerable<string> collegeNames)
        {
            var ids =
                collegeNames.Select(x => _repository.GetLteDistributionIds(x))
                    .Aggregate((x, y) => x.Concat(y))
                    .Distinct();
            var distributions = ids.Select(_indoorRepository.Get).Where(distribution => distribution != null).ToList();
            return distributions;
        }

        public IEnumerable<IndoorDistribution> QueryCdmaDistributions(IEnumerable<string> collegeNames)
        {
            var ids =
                collegeNames.Select(x => _repository.GetCdmaDistributionIds(x))
                    .Aggregate((x, y) => x.Concat(y))
                    .Distinct();
            var distributions = ids.Select(_indoorRepository.Get).Where(distribution => distribution != null).ToList();
            return distributions;
        }
    }
}
