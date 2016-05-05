using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Evaluations.ViewModels.College;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.ExcelCsv;
using Lte.Parameters.Entities.Mr;
using Lte.Parameters.MockOperations;

namespace Lte.Evaluations.MapperSerive
{
    public class EvalutaionMapperProfile : Profile
    {
        protected override void Configure()
        {
            CoreMapperService.MapCell();
            CoreMapperService.MapDtItems();

            AlarmMapperService.MapAlarms();
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
            InfrastructureMapperService.MapHoParametersService();

            KpiMapperService.MapCdmaRegionStat();
            KpiMapperService.MapCellPrecise();
            KpiMapperService.MapTownPrecise();
            KpiMapperService.MapAlarmStat();
            KpiMapperService.MapTopKpi();
            KpiMapperService.MapTopKpiTrend();
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
            AutoMapperHelper.CreateMap(typeof(BtsExcel));
            AutoMapperHelper.CreateMap(typeof(CellSectorIdPair));
            AutoMapperHelper.CreateMap(typeof(PciCellPair));
            AutoMapperHelper.CreateMap(typeof(CdmaRegionStat));
            AutoMapperHelper.CreateMap(typeof(InterferenceMatrixStat));
            AutoMapperHelper.CreateMap(typeof (DistrictPreciseView));
            AutoMapperHelper.CreateMap(typeof(WorkItemChartView));
        }
    }
}
