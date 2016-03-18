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
    public class IntraFreqHoGroupRepositoryTests
    {
        private readonly IIntraFreqHoGroupRepository _repository = new IntraFreqHoGroupRepository();

        [Test]
        public void Test_GetByENodebAndLocalCellId()
        {
            var result = _repository.GetRecent(500814, 2);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.iDate, "20160311");
        }
    }
}
