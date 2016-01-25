using System;
using System.IO;
using System.Linq;
using System.Text;
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

        [Test]
        public void Test_ZteFile1()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var csvFilesDirectory = Path.Combine(testDirectory, "CsvFiles");
            var path = Path.Combine(csvFilesDirectory, "NeighborCellZte1.csv");

            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            var infos = CsvContext.Read<NeighborCellZteCsv>(reader, CsvFileDescription.CommaDescription).ToList();
            Assert.AreEqual(infos.Count, 1);
        }

        [Test]
        public void Test_ZteFile()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var csvFilesDirectory = Path.Combine(testDirectory, "CsvFiles");
            var path = Path.Combine(csvFilesDirectory, "NeighborCellZte.csv");

            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            var infos = CsvContext.Read<NeighborCellZteCsv>(reader, CsvFileDescription.CommaDescription).ToList();
            Assert.AreEqual(infos.Count, 998);
        }

        [Test]
        public void Test_ZteFile_Merge()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var csvFilesDirectory = Path.Combine(testDirectory, "CsvFiles");
            var path = Path.Combine(csvFilesDirectory, "NeighborCellZte.csv");

            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            var infos = NeighborCellZteCsv.ReadNeighborCellZteCsvs(reader);
            Assert.AreEqual(infos.Count, 998);
        }
    }
}
