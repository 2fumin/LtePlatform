using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Concrete.Switch;
using NUnit.Framework;

namespace Lte.Parameters.Test.Switch
{
    [TestFixture]
    public class InterRatHoCommRepositoryTests
    {
        private IInterRatHoCommRepository _repository = new InterRatHoCommRepository();

        [Test]
        public void Test_GetByENodebId()
        {
            var result = _repository.GetRecent(500814);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.iDate, "20160311");
        }
    }
}
