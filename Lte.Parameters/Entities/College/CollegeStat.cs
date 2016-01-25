using Lte.Domain.Regular.Attributes;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.College;

namespace Lte.Parameters.Entities.College
{
    [TypeDoc("校园网统计信息，主要包括用户数和有关的基础信息")]
    public class CollegeStat
    {
        [MemberDoc("校园编号")]
        public int Id { get; set; }

        [MemberDoc("校园名称")]
        public string Name { get; private set; }

        [MemberDoc("用户到达数")]
        public int ExpectedSubscribers { get; private set; }

        [MemberDoc("区域面积")]
        public double Area { get; private set; }

        [MemberDoc("LTE基站总数")]
        public int TotalLteENodebs { get; set; }

        [MemberDoc("LTE小区总数")]
        public int TotalLteCells { get; set; }

        [MemberDoc("CDMA基站总数")]
        public int TotalCdmaBts { get; set; }

        [MemberDoc("CDMA小区总数")]
        public int TotalCdmaCells { get; set; }

        [MemberDoc("LTE室内分布总数")]
        public int TotalLteIndoors { get; set; }

        [MemberDoc("CDMA室内分布总数")]
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
