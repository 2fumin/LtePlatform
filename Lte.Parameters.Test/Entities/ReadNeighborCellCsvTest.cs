using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Parameters.Entities.ExcelCsv;
using NUnit.Framework;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class ReadNeighborCellCsvTest
    {
        [Test]
        public void Test_HuaweiFile()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var csvFilesDirectory = Path.Combine(testDirectory, "CsvFiles");
            var path = Path.Combine(csvFilesDirectory, "NeighborCellHw.csv");

            var reader = new StreamReader(path);
            var infos = CsvContext.Read<NeighborCellHwCsv>(reader, CsvFileDescription.CommaDescription).ToList();
            Assert.AreEqual(infos.Count, 999);
        }

        [Test]
        public void Test_HuaweiFile_Merge()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var csvFilesDirectory = Path.Combine(testDirectory, "CsvFiles");
            var path = Path.Combine(csvFilesDirectory, "NeighborCellHw.csv");

            var reader = new StreamReader(path);
            var infos = NeighborCellHwCsv.ReadNeighborCellHwCsvs(reader);
            Assert.AreEqual(infos.Count, 7);
        }
    }
}
