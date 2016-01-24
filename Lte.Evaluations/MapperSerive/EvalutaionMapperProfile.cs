using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;

namespace Lte.Evaluations.MapperSerive
{
    public class EvalutaionMapperProfile : Profile
    {
        protected override void Configure()
        {
            CoreMapperService.MapCdmaCell();
            CoreMapperService.MapCell();
            CoreMapperService.MapENodeb();
            CoreMapperService.MapBts();
            CoreMapperService.MapDtItems();

            AlarmMapperService.MapAlarms();
            StatMapperService.MapCdmaRegionStat();
            StatMapperService.MapPreciseCoverage();
            StatMapperService.MapTopConnection3G();
            StatMapperService.MapTopDrop2G();
            StatMapperService.MapWorkItem();
            StatMapperService.MapInterferenceMatrix();

            ParametersDumpMapperService.MapFromENodebContainerService();
            ParametersDumpMapperService.MapFromBtsContainerService();
            ParametersDumpMapperService.MapENodebBtsIdService();
            
            InfrastructureMapperService.MapCdmaCell();
            InfrastructureMapperService.MapCell();

            KpiMapperService.MapCdmaRegionStat();
            KpiMapperService.MapCellPrecise();
            KpiMapperService.MapDistrictPrecise();
            KpiMapperService.MapTownPrecise();
            KpiMapperService.MapAlarmStat();
            KpiMapperService.MapTopDrop2G();
            KpiMapperService.MapTopDrop2GTrend();
            KpiMapperService.MapPreciseStat();
            KpiMapperService.MapWorkItem();

            BaiduMapperService.MapCdmaCellView();
            BaiduMapperService.MapCellView();
            BaiduMapperService.MapDtViews();

            AutoMapperHelper.CreateMap(typeof (ENodebView));
            AutoMapperHelper.CreateMap(typeof (CdmaBtsView));
            AutoMapperHelper.CreateMap(typeof (College3GTestView));
            AutoMapperHelper.CreateMap(typeof (College4GTestView));
            AutoMapperHelper.CreateMap(typeof (CollegeKpiView));
            AutoMapperHelper.CreateMap(typeof(IndoorDistributionExcel));
        }
    }
}
