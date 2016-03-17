using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.MapperSerive.Kpi;
using Lte.Parameters.Entities.Kpi;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.Test.MapperService
{
    [TestFixture]
    public class TopCellQueriesServiceTest
    {
        [Test]
        public void Test_OneItem()
        {
            var stats = new List<TopDrop2GCell>
            {
                new TopDrop2GCell
                {
                    BtsId = 1,
                    SectorId = 2,
                    StatTime = DateTime.Parse("2015-1-1"),
                    Drops = 100,
                    TrafficAssignmentSuccess = 1200
                }
            };
            var trends = stats.QueryTrends();
            Assert.AreEqual(trends.Count(), 1);
            var trend = trends.ElementAt(0);
            trend.BtsId.ShouldBe(1);
            trend.SectorId.ShouldBe((byte)2);
            trend.TopDates.ShouldBe(1);
            trend.TotalDrops.ShouldBe(100);
            trend.TotalCallAttempst.ShouldBe(1200);
        }

        [TestCase(1, 1, new[] {1, 200}, new[] {2100, 291})]
        [TestCase(1, 2, new[] { 1098, 220 }, new[] { 2300, 2191 })]
        [TestCase(3, 4, new[] { 1, 200, 2134 }, new[] { 2100, 291, 74 })]
        public void Test_MultiItems_SameCell(int btsId, byte sectorId, int[] drops, int[] calls)
        {
            var stats = drops.Select((t, i) => new TopDrop2GCell
            {
                BtsId = btsId,
                SectorId = sectorId,
                StatTime = DateTime.Parse("2015-1-1"),
                Drops = t,
                TrafficAssignmentSuccess = calls[i]
            });
            var trends = stats.QueryTrends();
            Assert.AreEqual(trends.Count(), 1);
            var trend = trends.ElementAt(0);
            trend.BtsId.ShouldBe(btsId);
            trend.SectorId.ShouldBe(sectorId);
            trend.TopDates.ShouldBe(drops.Length);
            trend.TotalDrops.ShouldBe(drops.Sum());
            trend.TotalCallAttempst.ShouldBe(calls.Sum());
        }
        
        [TestCase(new[] {1, 1}, new byte[] {2, 3}, new[] { 1, 200 }, new[] { 2100, 291 })]
        [TestCase(new[] {1, 3}, new byte[] {2, 2}, new[] { 1098, 220 }, new[] { 2300, 2191 })]
        [TestCase(new[] {1,1,2}, new byte[] {2,3,3}, new[] { 1, 200, 2134 }, new[] { 2100, 291, 74 })]
        public void Test_MultiItems_DifferentCells(int[] btsIds, byte[] sectorIds, int[] drops, int[] calls)
        {
            var stats = drops.Select((t, i) => new TopDrop2GCell
            {
                BtsId = btsIds[i],
                SectorId = sectorIds[i],
                StatTime = DateTime.Parse("2015-1-1"),
                Drops = t,
                TrafficAssignmentSuccess = calls[i]
            });
            var trends = stats.QueryTrends();
            trends.Count().ShouldBe(btsIds.Length);
            for (var i = 0; i < btsIds.Length; i++)
            {
                var trend = trends.ElementAt(i);
                trend.BtsId.ShouldBe(btsIds[i]);
                trend.SectorId.ShouldBe(sectorIds[i]);
                trend.TopDates.ShouldBe(1);
                trend.TotalDrops.ShouldBe(drops[i]);
                trend.TotalCallAttempst.ShouldBe(calls[i]);
            }
        }
    }
}
