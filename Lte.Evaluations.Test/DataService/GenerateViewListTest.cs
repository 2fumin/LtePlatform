using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Lte.Parameters.Entities;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.DataService;
using Lte.Evaluations.Test.DataService;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class GenerateViewListTest
    {
        [TestCase("2015-1-2", "region1", 12.3)]
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
    }
}
