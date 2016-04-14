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
    public class ExternalEUtranCellFDDZteRepositoryTests
    {
        private readonly IExternalEUtranCellFDDZteRepository _repository = new ExternalEUtranCellFDDZteRepository();
        
        [Test]
        public void Test_GetRecentBySectorId()
        {
            var results = _repository.GetRecentList(551203);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 81);
            Assert.AreEqual(results[0].iDate, "20160408");
        }

        [Test]
        public void Test_GetReverseBySectorId()
        {
            var results = _repository.GetReverseList(501676, 50);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 12);
            Assert.AreEqual(results[0].iDate, "20160408");
        }

        [Test]
        public void Test_GetReverseBySectorId2()
        {
            var results = _repository.GetReverseList(501965, 0);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 134);
            Assert.AreEqual(results[0].iDate, "20160408");
        }
    }
}
