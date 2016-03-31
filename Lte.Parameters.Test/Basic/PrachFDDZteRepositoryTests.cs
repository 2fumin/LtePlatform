using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Concrete.Basic;
using NUnit.Framework;

namespace Lte.Parameters.Test.Basic
{
    [TestFixture]
    public class PrachFDDZteRepositoryTests
    {
        private readonly IPrachFDDZteRepository _repository = new PrachFDDZteRepository();

        [Test]
        public void Test_GetRecentBySectorId()
        {
            var results = _repository.GetRecent(551203, 48);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.iDate, "20160325");
        }
    }
}
