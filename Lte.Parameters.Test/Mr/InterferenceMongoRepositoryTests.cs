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
            Assert.AreEqual(results.Count, 3931);
            Assert.AreEqual(results[0].InterfLevel ?? 0, 161.48, 1E-7);
            results[0].CurrentDate.ShouldBe(new DateTime(2016, 4, 28, 8, 45, 0));
            results[0].ENodebId.ShouldBe(501373);
            results[0].Pci.ShouldBe(341);
            results[0].NeighborPci.ShouldBe(340);
            results[0].NeighborFreq.ShouldBe(100);
            Assert.AreEqual(results[0].Mod3Count, 0);
            results[0].Mod6Count.ShouldBe(0);
            results[0].Over6db.ShouldBe(23);
            results[0].Over10db.ShouldBe(30);
        }

        [Test]
        public void Test_GetWithENodebIdAndPciAndTime()
        {
            var results =
                _repository.GetList(501373, 341)
                    .Where(x => x.CurrentDate >= new DateTime(2016, 4, 28) && x.CurrentDate < new DateTime(2016, 4, 29)).ToList();
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 313);
            Assert.AreEqual(results[0].InterfLevel ?? 0, 161.48, 1E-7);
            results[0].CurrentDate.ShouldBe(new DateTime(2016, 4, 28, 8, 45, 0));
            results[0].ENodebId.ShouldBe(501373);
            results[0].Pci.ShouldBe(341);
            results[0].NeighborPci.ShouldBe(340);
            results[0].NeighborFreq.ShouldBe(100);
            Assert.AreEqual(results[0].Mod3Count, 0);
            results[0].Mod6Count.ShouldBe(0);
            results[0].Over6db.ShouldBe(23);
            results[0].Over10db.ShouldBe(30);
        }
        
        [TestCase(501298, 328, 329, "2016-04-28")]
        [TestCase(501454, 255, 438, "2016-04-28")]
        public void Test_GetList(int eNodebId, short pci, short neighborPci, string dateString)
        {
            var list = _repository.GetList(eNodebId, pci);
            var result =
                list
                    .Where(
                        x =>
                            x.NeighborPci == neighborPci && x.CurrentDate >= DateTime.Parse(dateString) &&
                            x.CurrentDate < DateTime.Parse(dateString).AddDays(1));
            Assert.IsNotNull(result);
        }
    }
}
