using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Entities.Mr;
using Lte.Parameters.MockOperations;
using NUnit.Framework;

namespace Lte.Parameters.Test.Mr
{
    [TestFixture]
    public class InterferenceMatrixStatTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            StatMapperService.MapInterferenceMatrix();
        }

        [Test]
        public void TestMap()
        {
            var mongoStat = new InterferenceMatrixMongo
            {
                INTERF_ONLY_COFREQ = 26.34,
                current_date = "201512301445",
                ENODEBID_PCI_NPCI_NFREQ = "500026_88_301_1825",
                OVERCOVER_COFREQ_10DB = 2
            };
            var stat = Mapper.Map<InterferenceMatrixMongo, InterferenceMatrixStat>(mongoStat);
            Assert.IsNotNull(stat);
            Assert.AreEqual(stat.ENodebId, 500026);
            Assert.AreEqual(stat.DestPci, 301);
            Assert.AreEqual(stat.OverInterferences10Db, 2);
            Assert.AreEqual(stat.InterferenceLevel, 26.34);
        }
    }
}
