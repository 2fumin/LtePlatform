using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.DataService;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class GenerateViewListTest
    {
        [TestCase("2015-1-2", "region1", 12.3)]
        [TestCase("2015-3-2", "region3", 20.3)]
        public void Test_OneElement_Matched(string date, string region, double erlang2G)
        {
            var statList = new List<CdmaRegionStat>
            {
                new CdmaRegionStat
                {
                    ErlangIncludingSwitch = erlang2G,
                    Region = region,
                    StatDate = DateTime.Parse(date)
                }
            };
            var dates = new List<DateTime>
            {
                DateTime.Parse(date)
            };
            var regionList = new List<string>
            {
                region
            };
            var viewList = CdmaRegionStatService.GenerateViewList(statList, dates, regionList);
            Assert.IsNotNull(viewList);
            Assert.AreEqual(viewList.Count, 2);
            Assert.IsNotNull(viewList[0]);
            Assert.AreEqual(viewList[0].Count(), 1);
            Assert.AreEqual(viewList[1].Count(), 1);
            viewList[0].ElementAt(0).AssertErlang2G(erlang2G);
            viewList[1].ElementAt(0).AssertErlang2G(erlang2G);
        }

        [TestCase(new[] { "2015-1-2", "2015-2-4" }, "region1", new[] { 12.8, 22.3 }, 0)]
        [TestCase(new[] { "2015-1-2", "2015-2-4" }, "region1", new[] { 12.8, 22.3 }, 1)]
        [TestCase(new[] { "2015-8-2", "2015-7-4" }, "region1", new[] { 12.8, 22.3 }, 0)]
        [TestCase(new[] { "2015-8-2", "2015-7-4", "2015-3-2" }, "region1", new[] { 12.8, 22.3, 32.1 }, 1)]
        public void Test_SingleRegion_MultiDates_OnlyOneDateMatched(string[] dates,
            string region, double[] erlang2Gs, int matchIndex)
        {
            var statList = dates.Select((t, i) => new CdmaRegionStat
            {
                ErlangIncludingSwitch = erlang2Gs[i],
                Region = region,
                StatDate = DateTime.Parse(t)
            }).ToList();
            var singleDate = new List<DateTime>
            {
                DateTime.Parse(dates[matchIndex])
            };
            var regionList = new List<string>
            {
                region
            };
            var viewList = CdmaRegionStatService.GenerateViewList(statList, 
                singleDate, regionList);
            Assert.IsNotNull(viewList);
            Assert.AreEqual(viewList.Count, 2);
            Assert.IsNotNull(viewList[0]);
            Assert.AreEqual(viewList[0].Count(), 1);
            Assert.AreEqual(viewList[1].Count(), 1);
            viewList[0].ElementAt(0).AssertErlang2G(erlang2Gs[matchIndex]);
            viewList[1].ElementAt(0).AssertErlang2G(erlang2Gs[matchIndex]);
        }
    }
}
