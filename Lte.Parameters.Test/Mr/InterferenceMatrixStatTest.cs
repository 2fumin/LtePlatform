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
                InterfLevel = 26.34,
                CurrentDate = new DateTime(2015,12,30,14,45,0),
                ENodebId = 500026,
                Pci = 88,
                NeighborPci =301,
                NeighborFreq = 1825,
                Over10db = 2
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
