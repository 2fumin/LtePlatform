using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Concrete.Basic;
using NUnit.Framework;

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
            Assert.AreEqual(results.Count, 27);
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
    }
}
