using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;

namespace Lte.Evaluations.ViewModels
{
    [TypeDoc("工单信息视图")]
    public class WorkItemView
    {
        [MemberDoc("工单编号")]
        public string SerialNumber { get; set; }

        [MemberDoc("工单类型")]
        public string WorkItemType { get; set; }

        [MemberDoc("工单子类型")]
        public string WorkItemSubType { get; set; }

        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("城市")]
        public string City { get; set; }

        [MemberDoc("区域")]
        public string District { get; set; }

        [MemberDoc("镇区")]
        public string Town { get; set; }

        [MemberDoc("派单时间")]
        public DateTime BeginTime { get; set; }

        [MemberDoc("回单期限")]
        public DateTime Deadline { get; set; }

        [MemberDoc("重复次数")]
        public short RepeatTimes { get; set; }

        [MemberDoc("驳回次数")]
        public short RejectTimes { get; set; }

        [MemberDoc("责任人")]
        public string StaffName { get; set; }

        [MemberDoc("最近反馈时间")]
        public DateTime? FeedbackTime { get; set; }

        [MemberDoc("完成时间")]
        public DateTime? FinishTime { get; set; }

        [MemberDoc("定位原因")]
        public string WorkItemCause { get; set; }

        [MemberDoc("工单状态")]
        public string WorkItemState { get; set; }

        [MemberDoc("省中心平台反馈信息")]
        public string Comments { get; set; }

        [MemberDoc("本平台反馈信息")]
        public string FeedbackContents { get; set; }

        public void UpdateTown(IENodebRepository eNodebRepository, IBtsRepository btsRepository,
            ITownRepository townRepository)
        {
            if (ENodebId > 10000)
            {
                var eNodeb = eNodebRepository.GetByENodebId(ENodebId);
                if (eNodeb != null)
                {
                    var town = eNodeb.TownId == -1 ? null : townRepository.Get(eNodeb.TownId);
                    if (town != null)
                    {
                        City = town.CityName;
                        District = town.DistrictName;
                        Town = town.TownName;
                        return;
                    }
                }
            }
            var bts = btsRepository.GetByBtsId(ENodebId);
            if (bts != null)
            {
                var town = bts.TownId == -1 ? null : townRepository.Get(bts.TownId);
                if (town != null)
                {
                    City = town.CityName;
                    District = town.DistrictName;
                    Town = town.TownName;
                }
            }
        }
    }

    public class WorkItemFeedbackView
    {
        [MemberDoc("工单编号")]
        public string SerialNumber { get; set; }

        public string Message { get; set; }
    }

    [AutoMapFrom(typeof(WorkItemView))]
    public class WorkItemChartView
    {
        [MemberDoc("工单类型")]
        public string WorkItemType { get; set; }

        [MemberDoc("工单子类型")]
        public string WorkItemSubType { get; set; }

        [MemberDoc("区域")]
        public string District { get; set; }

        [MemberDoc("镇区")]
        public string Town { get; set; }

        [MemberDoc("工单状态")]
        public string WorkItemState { get; set; }
    }
}
