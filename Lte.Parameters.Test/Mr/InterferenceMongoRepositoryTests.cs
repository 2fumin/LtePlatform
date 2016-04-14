using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete.Mr;
using Lte.Parameters.Entities.Mr;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using Shouldly;

namespace Lte.Parameters.Test.Mr
{
    [TestFixture]
    public class InterferenceMongoRepositoryTests
    {
        private readonly IInterferenceMongoRepository _repository = new InterferenceMongoRepository();
        
        [Test]
        public void Test_GetWithENodebIdAndPci()
        {
            var results = _repository.GetList(501373, 341);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 984);
            Assert.AreEqual(results[0].InterfLevel ?? 0, 4.65, 1E-7);
            results[0].CurrentDate.ShouldBe(new DateTime(2016, 3, 27, 18, 30, 0));
            results[0].ENodebId.ShouldBe(501373);
            results[0].Pci.ShouldBe(341);
            results[0].NeighborPci.ShouldBe(8);
            results[0].NeighborFreq.ShouldBe(100);
            results[0].Mod3Count.ShouldBe(1);
            results[0].Mod6Count.ShouldBe(0);
            results[0].Over6db.ShouldBe(1);
            results[0].Over10db.ShouldBe(1);
        }

        [Test]
        public void Test_GetWithENodebIdAndPciAndTime()
        {
            var results = _repository.GetList(501373, 341, new DateTime(2016, 3, 27));
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 16);
            Assert.AreEqual(results[0].InterfLevel ?? 0, 3.34, 1E-7);
            results[0].CurrentDate.ShouldBe(new DateTime(2016, 3, 27, 9, 15, 0));
            results[0].ENodebId.ShouldBe(501373);
            results[0].Pci.ShouldBe(341);
            results[0].NeighborPci.ShouldBe(359);
            results[0].NeighborFreq.ShouldBe(100);
            results[0].Mod3Count.ShouldBe(1);
            results[0].Mod6Count.ShouldBe(1);
            results[0].Over6db.ShouldBe(1);
            results[0].Over10db.ShouldBe(1);
        }
        
        [TestCase(501298, 328, 329, "2016-03-27")]
        [TestCase(501454, 255, 438, "2016-03-27")]
        public void Test_GetList(int eNodebId, short pci, short neighborPci, string dateString)
        {
            var result = _repository.GetList(eNodebId, pci, neighborPci, DateTime.Parse(dateString));
            Assert.IsNotNull(result);
        }
    }
}
