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
    public class InterferenceMatrixCsvTest
    {
        [Test]
        public void Test_CsvTest()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var csvFilesDirectory = Path.Combine(testDirectory, "CsvFiles");
            var path = Path.Combine(csvFilesDirectory, "Interference.txt");

            var reader = new StreamReader(path);
            var infos = CsvContext.Read<InterferenceMatrixCsv>(reader, CsvFileDescription.CommaDescription).ToList();
            Assert.AreEqual(infos.Count, 23);
            Assert.AreEqual(infos[0].InterferenceLevel, 14.32);
            Assert.AreEqual(infos[2].OverInterferences10Db, 0);
        }
    }
}
