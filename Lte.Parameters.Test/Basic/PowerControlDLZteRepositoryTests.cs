using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Neighbor;
using Lte.Parameters.Concrete.Channel;
using NUnit.Framework;

namespace Lte.Parameters.Test.Basic
{
    [TestFixture]
    public class PowerControlDLZteRepositoryTests
    {
        private readonly IPowerControlDLZteRepository _repository = new PowerControlDLZteRepository();

        [Test]
        public void Test_GetRecentBySectorId()
        {
            var results = _repository.GetRecent(501562, 1);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.iDate, "20160325");
        }

        [Test]
        public void Test_GetRecentByENodebId()
        {
            var results = _repository.GetRecentList(501562);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 3);
            Assert.AreEqual(results[0].iDate, "20160325");
        }
    }
}
