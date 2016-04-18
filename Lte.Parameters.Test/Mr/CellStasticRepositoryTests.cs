using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete.Mr;
using NUnit.Framework;
using Shouldly;

namespace Lte.Parameters.Test.Mr
{
    [TestFixture]
    public class CellStasticRepositoryTests
    {
        private readonly ICellStasticRepository _repository = new CellStasticRepository();

        [Test]
        public void Test_GetWithENodebIdAndPci()
        {
            var results = _repository.GetList(501373, 341);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 726);
            results[0].CurrentDate.ShouldBe(new DateTime(2016, 3, 27, 7, 0, 0));
            results[0].ENodebId.ShouldBe(501373);
            results[0].Pci.ShouldBe(341);
            Assert.AreEqual(results[0].Mod3Count, 0);
            Assert.AreEqual(results[0].Mod6Count, 0);
            Assert.AreEqual(results[0].MrCount, 13);
        }

        [Test]
        public void Test_GetWithENodebIdAndPciAndTime()
        {
            var results = _repository.GetList(501373, 341, new DateTime(2016, 3, 27));
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 9);
            results[0].CurrentDate.ShouldBe(new DateTime(2016, 3, 27, 7, 0, 0));
            results[0].ENodebId.ShouldBe(501373);
            results[0].Pci.ShouldBe(341);
            results[0].Mod3Count.ShouldBe(0);
            results[0].Mod6Count.ShouldBe(0);
            results[0].MrCount.ShouldBe(13);
        }

    }
}
