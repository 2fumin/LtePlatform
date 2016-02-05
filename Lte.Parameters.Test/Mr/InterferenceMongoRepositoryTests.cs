using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete.Mr;
using Lte.Parameters.Entities.Mr;
using NUnit.Framework;
using Shouldly;

namespace Lte.Parameters.Test.Mr
{
    [TestFixture]
    public class InterferenceMongoRepositoryTests
    {
        private readonly IInterferenceMongoRepository _repository = new InterferenceMongoRepository();

        [Test]
        public void Test_GetMongoInfos_First()
        {
            var results = _repository.GetByENodebInfo("522409_304_241_1825");
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 274);
            Assert.AreEqual((double)results[0].INTERF_ONLY_COFREQ, 11.88, 1E-7);
            results[0].current_date.ShouldBe("201512301530");
            results[0].MOD3_COUNT.ShouldBe(2);
            results[0].MOD6_COUNT.ShouldBe(0);
            results[0].OVERCOVER_COFREQ_6DB.ShouldBe(2);
            results[0].OVERCOVER_COFREQ_10DB.ShouldBe(2);
        }
    }
}
