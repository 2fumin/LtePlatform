using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Concrete.Neighbor;
using NUnit.Framework;

namespace Lte.Parameters.Test.Basic
{
    [TestFixture]
    public class EutranInterNFreqRepositoryTests
    {
        private readonly IEutranInterNFreqRepository _repository = new EutranInterNFreqRepository();

        [Test]
        public void Test_GetRecentListByENodeb()
        {
            var results = _repository.GetRecentList(500814);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 6);
            Assert.AreEqual(results[0].iDate, "20160318");
        }

        [Test]
        public void Test_GetRecentBySectorId()
        {
            var results = _repository.GetRecentList(499712, 0);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 2);
            Assert.AreEqual(results[0].iDate, "20160318");
            Assert.AreEqual(results[0].DlEarfcn, 1825);
            Assert.AreEqual(results[1].DlEarfcn, 41140);
        }
    }
}
