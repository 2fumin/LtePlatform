using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;

namespace Lte.Parameters.Entities
{
    public class CollegeStat
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public int ExpectedSubscribers { get; private set; }

        public double Area { get; private set; }

        public int TotalLteENodebs { get; set; }

        public int TotalLteCells { get; set; }

        public int TotalCdmaBts { get; set; }

        public int TotalCdmaCells { get; set; }

        public int TotalLteIndoors { get; set; }

        public int TotalCdmaIndoors { get; set; }

        public CollegeStat(ICollegeRepository repository, CollegeInfo info,
            IInfrastructureRepository infrastructureRepository)
        {
            CollegeRegion region = repository.GetRegion(info.Id);
            Name = info.Name;
            ExpectedSubscribers = info.ExpectedSubscribers;
            Area = region.Area;
            Id = region.AreaId;
            UpdateStats(infrastructureRepository);
        }

        private void UpdateStats(IInfrastructureRepository repository)
        {
            TotalLteENodebs = repository.Count(x => x.HotspotName == Name
                                                    && x.HotspotType == HotspotType.College
                                                    && x.InfrastructureType == InfrastructureType.ENodeb);
            TotalLteCells = repository.Count(x => x.HotspotName == Name
                                                  && x.HotspotType == HotspotType.College
                                                  && x.InfrastructureType == InfrastructureType.Cell);
            TotalCdmaBts = repository.Count(x => x.HotspotName == Name
                                                 && x.HotspotType == HotspotType.College
                                                 && x.InfrastructureType == InfrastructureType.CdmaBts);
            TotalCdmaCells = repository.Count(x => x.HotspotName == Name
                                                   && x.HotspotType == HotspotType.College
                                                   && x.InfrastructureType == InfrastructureType.CdmaCell);
            TotalLteIndoors = repository.Count(x => x.HotspotName == Name
                                                    && x.HotspotType == HotspotType.College
                                                    && x.InfrastructureType == InfrastructureType.LteIndoor);
            TotalCdmaIndoors = repository.Count(x => x.HotspotName == Name
                                                     && x.HotspotType == HotspotType.College
                                                     && x.InfrastructureType == InfrastructureType.CdmaIndoor);
        }
    }
}
