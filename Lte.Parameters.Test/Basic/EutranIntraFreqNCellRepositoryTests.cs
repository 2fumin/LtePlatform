using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Neighbor;
using Lte.Parameters.Concrete.Neighbor;
using NUnit.Framework;

namespace Lte.Parameters.Test.Basic
{
    [TestFixture]
    public class EutranIntraFreqNCellRepositoryTests
    {
        private readonly IEutranIntraFreqNCellRepository _repository = new EutranIntraFreqNCellRepository();

        [Test]
        public void Test_GetRecentBySectorId()
        {
            var results = _repository.GetRecentList(500814, 0);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 33);
            Assert.AreEqual(results[0].iDate, "20160318");
        }
    }
}
