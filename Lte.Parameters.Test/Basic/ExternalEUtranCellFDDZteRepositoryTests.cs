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
            Assert.AreEqual(results[0].iDate, "20160318");
        }
    }
}
