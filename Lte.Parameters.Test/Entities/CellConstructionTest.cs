using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Wireless;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using NUnit.Framework;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class CellConstructionTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            CoreMapperService.MapCell();
        }

        [TestCase(1233, 23, 12.345, 1, "1T1R", 1.0, "1T1R")]
        [TestCase(1253, 23, 223.45, 1825, "否", 3.2, "2T2R")]
        [TestCase(1353, 33, 27.34, 119, "DO", 4.2, "1T1R")]
        [TestCase(1353, 33, 27.34, 160, "DO", 4.2, "1T1R")]
        [TestCase(1353, 33, 2.734, 201, "否 ", 1.6, "2T4R")]
        [TestCase(1353, 33, 273.4, 75, "否 ", 3.2, "2T4R")]
        [TestCase(1353, 33, 27.34, 283, "2T4R", 6.4, "1T1R")]
        [TestCase(1353, 33, 2.734, 1013, "否", 1.28, "2T4R")]
        [TestCase(1353, 33, 27.34, 100, "4T4R", 0.9, "4T4R")]
        public void Test_NewCell(int eNodebId, byte sectorId, double longtitute, short frequency, string isIndoor,
            double azimuth, string antConfig)
        {
            var cellExcelInfo = new CellExcel
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                Longtitute = longtitute,
                Frequency = frequency,
                IsIndoor = isIndoor,
                AntennaGain = 12.8,
                Azimuth = azimuth,
                TransmitReceive = antConfig
            };
            var cell = Cell.ConstructItem(cellExcelInfo);
            Assert.IsNotNull(cell);
            Assert.AreEqual(cell.ENodebId, eNodebId);
            Assert.AreEqual(cell.SectorId, sectorId);
            Assert.AreEqual(cell.Longtitute, longtitute);
            Assert.AreEqual(cell.IsOutdoor, isIndoor.Trim() == "否");
            Assert.AreEqual(cell.AntennaGain, 12.8);
            Assert.AreEqual(cell.Azimuth, azimuth, "azimuth");
            Assert.AreEqual(cell.AntennaPorts, antConfig.GetAntennaPortsConfig());
        }
    }
}
