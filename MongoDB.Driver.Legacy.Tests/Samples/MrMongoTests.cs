using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Legacy.TestHelpers;
using NUnit.Framework;

namespace MongoDB.Driver.Legacy.Tests.Samples
{
    [TestFixture]
    public class MrMongoTests
    {
        private readonly MrMongoTestHelper _helper = new MrMongoTestHelper();

        [Test]
        public async void Test_GetInterferenceInfos_First()
        {
            var results = await _helper.GetInterferenceInfos("522409_304_241_1825");
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 274);
            Assert.AreEqual(results[0]["INTERF_ONLY_COFREQ"].AsDouble, 11.88, 1E-7);
            Assert.AreEqual(results[0]["current_date"].AsString, "201512301530");
            Assert.IsTrue(results[0]["current_date"].IsString);
            Assert.AreEqual(results[0]["MOD3_COUNT"].AsInt32, 2);
            Assert.AreEqual(results[0]["OVERCOVER_COFREQ_10DB"].AsDouble, 2);
            Assert.AreEqual(results[0]["OVERCOVER_COFREQ_6DB"].AsInt32, 2);
            Assert.AreEqual(results[0]["MOD6_COUNT"].AsInt32, 0);
        }

        [Test]
        public async void Test_GetInterferenceInfosByTime_First()
        {
            var results = await _helper.GetInterferenceInfosByTime("201512301530");
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 184944);
            Assert.AreEqual(results[0]["INTERF_ONLY_COFREQ"].AsDouble, 11.88, 1E-7);
            Assert.AreEqual(results[0]["current_date"].AsString, "201512301530");
            Assert.IsTrue(results[0]["current_date"].IsString);
            Assert.AreEqual(results[0]["MOD3_COUNT"].AsInt32, 2);
            Assert.AreEqual(results[0]["OVERCOVER_COFREQ_10DB"].AsDouble, 2);
            Assert.AreEqual(results[0]["OVERCOVER_COFREQ_6DB"].AsInt32, 2);
            Assert.AreEqual(results[0]["MOD6_COUNT"].AsInt32, 0);
        }

        [Test]
        public async void Test_GetInterferenceInfosByENodebAndTime_First()
        {
            var results = await _helper.GetInterferenceInfosByENodebAndTime("522409_304_241_1825", "201512301530");
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results[0]["INTERF_ONLY_COFREQ"].AsDouble, 11.88, 1E-7);
            Assert.AreEqual(results[0]["current_date"].AsString, "201512301530");
            Assert.IsTrue(results[0]["current_date"].IsString);
            Assert.AreEqual(results[0]["MOD3_COUNT"].AsInt32, 2);
            Assert.AreEqual(results[0]["OVERCOVER_COFREQ_10DB"].AsDouble, 2);
            Assert.AreEqual(results[0]["OVERCOVER_COFREQ_6DB"].AsInt32, 2);
            Assert.AreEqual(results[0]["MOD6_COUNT"].AsInt32, 0);
        }
    }
}
