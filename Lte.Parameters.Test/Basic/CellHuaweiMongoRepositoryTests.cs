using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Concrete.Basic;
using NUnit.Framework;
using System.Linq;

namespace Lte.Parameters.Test.Basic
{
    [TestFixture]
    public class CellHuaweiMongoRepositoryTests
    {
        private readonly ICellHuaweiMongoRepository _repository = new CellHuaweiMongoRepository();

        [Test]
        public void Test_GetByENodeb()
        {
            var results = _repository.GetAllList(500814);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 30);
            Assert.AreEqual(results[0].iDate, "20160122");
        }

        [Test]
        public void Test_GetRecentListByENodeb()
        {
            var results = _repository.GetRecentList(500814);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 3);
            Assert.AreEqual(results[0].iDate, "20160311");
        }

        [Test]
        public void Test_GetRecentBySectorId()
        {
            var results = _repository.GetRecent(499712, 0);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.iDate, "20160318");
        }

        [Test]
        public void Test_GetRecentBySectorIdWithSfn()
        {
            var results = _repository.GetRecent(499712, 1);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.iDate, "20160318");
            Assert.AreEqual(results.SectorEqmNum, null);
            Assert.AreEqual(results.MultiRruCellMode, null);
            Assert.AreEqual(results.CpriEthCompressionRatio, null);
        }

        [Test]
        public void Test_GetAllList_WithSfn()
        {
            var results = _repository.GetAllList(499712).Count(x => x.SectorEqmNum != null);
            Assert.AreEqual(results, 3);
        }
    }
}
