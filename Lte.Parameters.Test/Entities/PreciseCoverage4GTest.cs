using System;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using NUnit.Framework;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class PreciseCoverage4GTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            StatMapperService.MapPreciseCoverage();
        }

        [TestCase("2015-2-2", 1, 2, 100, 0.33, 0.27, 0.19)]
        [TestCase("2015-5-2", 11, 2, 1000, 0.43, 0.27, 0.19)]
        [TestCase("2015-2-5", 1231, 2, 1001, 0.331, 0.227, 0.139)]
        [TestCase("2015-4-21", 1855, 2, 1002, 0.33, 0.217, 0.119)]
        public void Test_Constructor(string statTime, int cellId, byte sectorId, int totalMrs, double firstRate,
            double secondRate, double thirdRate)
        {
            var info = new PreciseCoverage4GCsv
            {
                StatTime = DateTime.Parse(statTime),
                CellId = cellId,
                SectorId = sectorId,
                TotalMrs = totalMrs,
                FirstNeighborRate = firstRate,
                SecondNeighborRate = secondRate,
                ThirdNeighborRate = thirdRate
            };
            var stat = PreciseCoverage4G.ConstructStat(info);
            Assert.AreEqual(stat.StatTime, DateTime.Parse(statTime));
            Assert.AreEqual(stat.CellId, cellId);
            Assert.AreEqual(stat.SectorId, sectorId);
            Assert.AreEqual(stat.TotalMrs, totalMrs);
            Assert.AreEqual(stat.FirstNeighbors, (int) (totalMrs*firstRate)/100);
            Assert.AreEqual(stat.SecondNeighbors, (int)(totalMrs * secondRate) / 100);
            Assert.AreEqual(stat.ThirdNeighbors, (int)(totalMrs * thirdRate) / 100);
        }
    }
}
