using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;

namespace Lte.Evaluations.Test.TestService
{
    public class CollegeDistributionTestService
    {
        private readonly Mock<IInfrastructureRepository> _repository;
        private readonly Mock<IIndoorDistributioinRepository> _indoorRepository;

        public CollegeDistributionTestService(Mock<IInfrastructureRepository> repository,
            Mock<IIndoorDistributioinRepository> indoorRepository)
        {
            _repository = repository;
            _indoorRepository = indoorRepository;
        }

        public void MockOneLteDistribution(int id)
        {
            var infrastructures = new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-"+id,
                    HotspotType = HotspotType.College,
                    InfrastructureType = InfrastructureType.LteIndoor,
                    InfrastructureId = id
                }
            };
            _repository.MockInfrastructures(infrastructures);
        }

        public void MockOneCdmaDistribution(int id)
        {
            var infrastructures = new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-"+id,
                    HotspotType = HotspotType.College,
                    InfrastructureType = InfrastructureType.CdmaIndoor,
                    InfrastructureId = id
                }
            };
            _repository.MockInfrastructures(infrastructures);
        }
    }
}
