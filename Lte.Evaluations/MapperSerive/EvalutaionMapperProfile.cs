using AutoMapper;
using Lte.Parameters.MockOperations;

namespace Lte.Evaluations.MapperSerive
{
    public class EvalutaionMapperProfile : Profile
    {
        protected override void Configure()
        {
            CoreMapperService.MapCdmaCell();
            CoreMapperService.MapCell();
            CoreMapperService.MapIndoorDistribution();
            AlarmMapperService.MapAlarms();
            StatMapperService.MapCdmaRegionStat();
            StatMapperService.MapPreciseCoverage();
            StatMapperService.MapTopConnection3G();
            StatMapperService.MapTopDrop2G();

            ParametersDumpMapperService.MapFromENodebContainerService();
            ParametersDumpMapperService.MapFromBtsContainerService();
            ParametersDumpMapperService.MapENodebBtsIdService();
            
            InfrastructureMapperService.MapENodeb();
            InfrastructureMapperService.MapBts();
            InfrastructureMapperService.MapCdmaCell();
            InfrastructureMapperService.MapCell();
            CollegeMapperService.MapCollege3GTest();
            CollegeMapperService.MapCollege4GTest();
            CollegeMapperService.MapCollegeKpi();
            KpiMapperService.MapCdmaRegionStat();
            KpiMapperService.MapCellPrecise();
            KpiMapperService.MapDistrictPrecise();
            KpiMapperService.MapTownPrecise();
            KpiMapperService.MapAlarmStat();
            KpiMapperService.MapTopDrop2G();
            KpiMapperService.MapTopDrop2GTrend();

            BaiduMapperService.MapCdmaCellView();
            BaiduMapperService.MapCellView();
        }
    }
}
