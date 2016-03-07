using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Entities.Work;
using Lte.Parameters.MockOperations;
using NUnit.Framework;
using Shouldly;

namespace Lte.Parameters.Test.Converter
{
    [TestFixture]
    public class WorkItemConverterTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            StatMapperService.MapWorkItem();
        }

        [Test]
        public void TestBasicParameters()
        {
            var source = new WorkItemExcel
            {
                ENodebId = 111,
                SectorId = 22,
                Deadline = DateTime.Today,
                RejectTimes = 10001,
                StaffName = "bac"
            };
            var dest = Mapper.Map<WorkItem>(source);
            dest.ENodebId.ShouldBe(111);
            dest.SectorId.ShouldBe((byte)22);
            dest.Deadline.ShouldBe(DateTime.Today);
            dest.RejectTimes.ShouldBe((short)10001);
            dest.StaffName.ShouldBe("bac");
        }

        [Test]
        public void TestTitle_ThreeParts()
        {
            var source = new WorkItemExcel
            {
                Title = "佛山_2G性能故障_小区级呼叫建立成功率异常_160307002"
            };
            var dest = Mapper.Map<WorkItem>(source);
            dest.Type.ShouldBe(WorkItemType.Kpi2G);
            dest.Subtype.ShouldBe(WorkItemSubtype.CallSetup);
        }

        [Test]
        public void TestTitle_TwoParts()
        {
            var source = new WorkItemExcel
            {
                Title = "佛山_RRC连接成功率恶化_150812001"
            };
            var dest = Mapper.Map<WorkItem>(source);
            dest.Type.ShouldBe(WorkItemType.RrcConnection);
            dest.Subtype.ShouldBe(WorkItemSubtype.Others);
        }

        [Test]
        public void TestTitle_Title1()
        {
            var source = new WorkItemExcel
            {
                Title = "佛山_日常网优作业计划_每周质量分析_20160307041"
            };
            var dest = Mapper.Map<WorkItem>(source);
            dest.Type.ShouldBe(WorkItemType.DailyTask);
            dest.Subtype.ShouldBe(WorkItemSubtype.WeeklyAnalysis);
        }

        [Test]
        public void TestTitle_Title2()
        {
            var source = new WorkItemExcel
            {
                Title = "日报--请地市提交网络性能指标异常分析处理报告"
            };
            var dest = Mapper.Map<WorkItem>(source);
            dest.Type.ShouldBe(WorkItemType.DailyReport);
            dest.Subtype.ShouldBe(WorkItemSubtype.Others);
        }

        [Test]
        public void TestTitle_Title3()
        {
            var source = new WorkItemExcel
            {
                Title = "翼路通—2016年第9周2G道路恶化小区清单"
            };
            var dest = Mapper.Map<WorkItem>(source);
            dest.Type.ShouldBe(WorkItemType.Yilutong);
            dest.Subtype.ShouldBe(WorkItemSubtype.Others);
        }

        [Test]
        public void TestTitle_Title4()
        {
            var source = new WorkItemExcel
            {
                Title = "省-集团测试保障-关键站点清单收集"
            };
            var dest = Mapper.Map<WorkItem>(source);
            dest.Type.ShouldBe(WorkItemType.Others);
            dest.Subtype.ShouldBe(WorkItemSubtype.Others);
        }
    }
}
