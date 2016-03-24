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
    public class EUtranRelationZteRepositoryTests
    {
        private readonly IEUtranRelationZteRepository _repository = new EUtranRelationZteRepository();

        [Test]
        public void Test_GetRecentBySectorId()
        {
            var results = _repository.GetRecentList(551203, 48);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 26);
            Assert.AreEqual(results[0].iDate, "20160318");
        }
    }
}
