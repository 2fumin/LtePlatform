using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using NUnit.Framework;
using Moq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.Test.MockItems;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class CdmaRegionStatServiceTest
    {
        private readonly Mock<IRegionRepository> _regionRepository
            = new Mock<IRegionRepository>();
        private readonly Mock<ICdmaRegionStatRepository> _statRepository
            = new Mock<ICdmaRegionStatRepository>();

        [SetUp]
        public void SetUp()
        {
            _regionRepository.MockQueryRegions(new List<OptimizeRegion>
            {
                new OptimizeRegion { City = "city", Region = "region1" },
                new OptimizeRegion { City = "city", Region = "region2" },
                new OptimizeRegion { City = "city", Region = "region3" }
            });
        }

        private void AssertStatView(CdmaRegionStatView view, string region, double erlang)
        {
            Assert.AreEqual(view.Region, region);
            Assert.AreEqual(view.ErlangIncludingSwitch, erlang);
        }

        private void AssertDropRate(CdmaRegionStatView view, string region, double dropRate)
        {
            Assert.AreEqual(view.Region, region);
            Assert.AreEqual(view.Drop2GRate, dropRate);
        }

        private CdmaRegionDateView QueryDateViewWithSingleStat(string initialDate,
            string region, string recordDate, double erlang)
        {
            _statRepository.MockCdmaRegionStats(new List<CdmaRegionStat>
            {
                new CdmaRegionStat
                {
                    Region = region,
                    StatDate = DateTime.Parse(recordDate),
                    ErlangIncludingSwitch = erlang
                }
            });
            var service = new CdmaRegionStatService(_regionRepository.Object, _statRepository.Object);
            return service.QueryLastDateStat(DateTime.Parse(initialDate), "city");
        }

        private CdmaRegionDateView QueryDateView_SingleRegion_MultiDates(string initialDate,
            string region, string[] recordDates, double[] erlangs)
        {
            var statList = new List<CdmaRegionStat>();
            for (int i = 0; i < recordDates.Length; i++)
            {
                statList.Add(new CdmaRegionStat
                {
                    Region = region,
                    StatDate = DateTime.Parse(recordDates[i]),
                    ErlangIncludingSwitch = erlangs[i]
                });
            }
            _statRepository.MockCdmaRegionStats(statList);
            var service = new CdmaRegionStatService(_regionRepository.Object, _statRepository.Object);
            return service.QueryLastDateStat(DateTime.Parse(initialDate), "city");
        }

        private CdmaRegionDateView QueryDateView_MultiRegions_SingleDate(string initialDate,
            string[] regions, string recordDate, double[] erlangs)
        {
            var statList = new List<CdmaRegionStat>();
            for (int i = 0; i < regions.Length; i++)
            {
                statList.Add(new CdmaRegionStat
                {
                    Region = regions[i],
                    StatDate = DateTime.Parse(recordDate),
                    ErlangIncludingSwitch = erlangs[i]
                });
            }
            _statRepository.MockCdmaRegionStats(statList);
            var service = new CdmaRegionStatService(_regionRepository.Object, _statRepository.Object);
            return service.QueryLastDateStat(DateTime.Parse(initialDate), "city");
        }

        private CdmaRegionDateView QueryDateView_MultiRegions_SingleDate_DropRateConsidered(
            string initialDate, string[] regions, string recordDate, 
            int[] drop2GNums, int[] drop2GDems)
        {
            var statList = new List<CdmaRegionStat>();
            for (int i = 0; i < regions.Length; i++)
            {
                statList.Add(new CdmaRegionStat
                {
                    Region = regions[i],
                    StatDate = DateTime.Parse(recordDate),
                    Drop2GNum = drop2GNums[i],
                    Drop2GDem = drop2GDems[i]
                });
            }
            _statRepository.MockCdmaRegionStats(statList);
            var service = new CdmaRegionStatService(_regionRepository.Object, _statRepository.Object);
            return service.QueryLastDateStat(DateTime.Parse(initialDate), "city");
        }

        [TestCase("2015-5-1", "region1", "2015-4-1", 10)]
        [TestCase("2015-6-2", "region2", "2015-4-20", 15)]
        [TestCase("2015-6-2", "region3", "2015-5-20", 15)]
        public void TestQueryLastDateStat_Normal(string initialDate,
            string region, string recordDate, double erlang)
        {
            var result = QueryDateViewWithSingleStat(initialDate, region, recordDate, erlang);
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Parse(result.StatDate), DateTime.Parse(recordDate));
            Assert.AreEqual(result.StatViews.Count(), 2);
            AssertStatView(result.StatViews.ElementAt(0), region, erlang);
            AssertStatView(result.StatViews.ElementAt(1), "city", erlang);
        }

        [TestCase("2015-5-1", "region4", "2015-4-1", 10)]
        [TestCase("2015-6-2", "region5", "2015-4-10", 15)]
        [TestCase("2015-6-2", "region6", "2015-5-25", 15)]
        public void TestQueryLastDateStat_RegionNotFound(string initialDate,
            string region, string recordDate, double erlang)
        {
            var result = QueryDateViewWithSingleStat(initialDate, region, recordDate, erlang);
            Assert.IsNull(result);
        }

        [TestCase("2015-5-1", "region1", "2015-1-1", 10)]
        [TestCase("2015-6-2", "region2", "2015-2-10", 15)]
        [TestCase("2015-6-2", "region3", "2015-1-25", 15)]
        [TestCase("2015-6-3", "region4", "2015-1-25", 15)]
        public void TestQueryLastDateStat_DateOutOfRange(string initialDate,
            string region, string recordDate, double erlang)
        {
            var result = QueryDateViewWithSingleStat(initialDate, region, recordDate, erlang);
            Assert.IsNull(result);
        }

        [TestCase(1, "2015-5-1", "region1",
            new[] { "2015-4-1", "2015-4-6" }, new[] { 10.7, 12 }, 1)]
        [TestCase(2, "2015-6-2", "region2", 
            new[] { "2015-4-20", "2015-4-11" }, new[] { 15, 17.8 }, 0)]
        [TestCase(3, "2015-6-2", "region3", 
            new[] { "2015-5-20", "2015-5-19", "2015-5-21" }, new[] { 15, 26.2, 14 }, 2)]
        [TestCase(4, "2015-6-2", "region2",
            new[] { "2015-4-20", "2015-4-26", "2015-4-17" }, new[] { 15.1, 17, 18 }, 1)]
        public void TestQueryLastDateStat_SingleRegion_MultiDates(int testNo,
            string initialDate, string region, string[] recordDates, double[] erlangs,
            int matchedIndex)
        {
            var result = QueryDateView_SingleRegion_MultiDates(initialDate, region, recordDates, erlangs);
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Parse(result.StatDate), DateTime.Parse(recordDates[matchedIndex]));
            Assert.AreEqual(result.StatViews.Count(), 2);
            AssertStatView(result.StatViews.ElementAt(0), region, erlangs[matchedIndex]);
            AssertStatView(result.StatViews.ElementAt(1), "city", erlangs[matchedIndex]);
        }

        [TestCase(1, "2015-5-1", new[] { "region1", "region2" },
            "2015-4-1", new[] { 10.1, 12.4 })]
        [TestCase(2, "2015-6-2", new[] { "region2", "region3", "region1" },
            "2015-4-20", new[] { 15, 11.4, 12.3 })]
        [TestCase(3, "2015-6-2", new[] { "region3", "region2" },
            "2015-5-20", new[] { 15.9, 16.4 })]
        public void TestQueryLastDateStat_MultiRegions_SingleDate_AllRegionsMatched(int testNo,
            string initialDate, string[] regions, string recordDate, double[] erlangs)
        {
            var result = QueryDateView_MultiRegions_SingleDate(initialDate, regions, recordDate, erlangs);
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Parse(result.StatDate), DateTime.Parse(recordDate));
            Assert.AreEqual(result.StatViews.Count(), regions.Length + 1);
            for (int i=0;i<regions.Length;i++)
                AssertStatView(result.StatViews.ElementAt(i), regions[i], erlangs[i]);
            AssertStatView(result.StatViews.ElementAt(regions.Length), "city", erlangs.Sum());
        }

        [TestCase(1, "2015-5-1", new[] { "region1", "region2" },
            "2015-4-1", new[] { 10, 12 }, new[] { 11, 13 })]
        [TestCase(2, "2015-6-2", new[] { "region2", "region3", "region1" },
            "2015-4-20", new[] { 15, 11, 12 }, new[] { 11, 13, 17 })]
        [TestCase(3, "2015-6-2", new[] { "region3", "region2" },
            "2015-5-20", new[] { 15, 16 }, new[] { 11, 13 })]
        public void TestQueryLastDateStat_MultiRegions_SingleDate_DropRateConsidered(int testNo,
            string initialDate, string[] regions, string recordDate,
            int[] drop2GNums, int[] drop2GDems)
        {
            var result = QueryDateView_MultiRegions_SingleDate_DropRateConsidered(initialDate, regions, recordDate,
                drop2GNums, drop2GDems);
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Parse(result.StatDate), DateTime.Parse(recordDate));
            Assert.AreEqual(result.StatViews.Count(), regions.Length + 1);
            for (int i = 0; i < regions.Length; i++)
                AssertDropRate(result.StatViews.ElementAt(i), regions[i], (double) drop2GNums[i]/drop2GDems[i]);
            AssertDropRate(result.StatViews.ElementAt(regions.Length), "city",
                (double) drop2GNums.Sum()/drop2GDems.Sum());
        }
    }
}
