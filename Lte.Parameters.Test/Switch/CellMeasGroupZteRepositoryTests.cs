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
    public class CellMeasGroupZteRepositoryTests
    {
        private readonly ICellMeasGroupZteRepository _repository = new CellMeasGroupZteRepository();

        private readonly ICellMeasGroupZteRepository _outerRepository =
            new CellMeasGroupZteRepository(new OuterMongoProvider("fangww"));

        [Test]
        public void Test_GetByENodeb()
        {
            var result = _repository.GetRecent(551203);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.iDate, "20160304");
        }

        [Test]
        public void Test_GetOuter()
        {
            var result=_outerRepository.GetRecent(551203);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.iDate, "20160318");
            Assert.AreEqual(result.intraFHOMeasCfg, "50,51");
            Assert.AreEqual(int.Parse(result.intraFHOMeasCfg.Split(',')[0]), 50);
        }
    }

    [TestFixture]
    public class EUtranCellMeasurementZteRepositoryTests
    {
        private readonly IEUtranCellMeasurementZteRepository _repository = new EUtranCellMeasurementZteRepository();

        [Test]
        public void Test_GetBySectorId()
        {
            var result = _repository.GetRecent(502776, 48);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.iDate, "20160325");
        }
    }
}
