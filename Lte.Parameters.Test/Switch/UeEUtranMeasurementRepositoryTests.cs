using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Concrete;
using Lte.Parameters.Concrete.Switch;
using NUnit.Framework;

namespace Lte.Parameters.Test.Switch
{
    [TestFixture]
    public class UeEUtranMeasurementRepositoryTests
    {
        private readonly IUeEUtranMeasurementRepository _repository = new UeEUtranMeasurementRepository();

        private readonly IUeEUtranMeasurementRepository _outerRepository =
            new UeEUtranMeasurementRepository(new OuterMongoProvider("fangww"));

        [Test]
        public void Test_GetByENodebAndConfig()
        {
            var result = _repository.GetRecent(551203, 10);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.iDate, "20160304");
        }

        [Test]
        public void Test_GetOuter()
        {
            var result=_outerRepository.GetRecent(551203, 10);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.iDate, "20160318");
        }

        [Test]
        public void Test_Outer2()
        {
            var result = _outerRepository.GetRecent(551203, 50);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.iDate, "20160318");
        }
    }
}
