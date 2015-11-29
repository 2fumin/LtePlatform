using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToExcel;
using Lte.Domain.Test.LinqToExcel;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Parameters.Test.Kpi
{
    [TestFixture]
    public class CdmaCityStatExcelTest : SQLLogStatements_Helper
    {
        ExcelQueryFactory _repo;
        string _excelFileName;
        string _worksheetName;

        [TestFixtureSetUp]
        public void fs()
        {
            InstantiateLogger();
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            _excelFileName = Path.Combine(excelFilesDirectory, "佛山.xls");
            _worksheetName = "佛山";
        }

        [SetUp]
        public void s()
        {
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
        }

        [Test]
        public void Test_Read_Sheet()
        {
            var info = (from c in _repo.Worksheet<CdmaRegionStatExcel>(_worksheetName)
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 30);
        }

        [TestCase("2015-10-4", 24)]
        [TestCase("2015-10-15", 13)]
        public void Test_Read_ByDate(string beginDate, int lines)
        {
            var begin = DateTime.Parse(beginDate);
            var info = (from c in _repo.Worksheet<CdmaRegionStatExcel>(_worksheetName)
                        where c.StatDate > begin
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, lines);
        }

        [TestCase("2015-10-4", "2015-11-4", 24)]
        [TestCase("2015-10-4", "2015-10-10", 7)]
        [TestCase("2015-10-15", "2015-11-4", 13)]
        [TestCase("2015-10-15", "2015-10-20", 6)]
        public void Test_Read_ByDateSpan(string beginDate, string endDate, int lines)
        {
            var begin = DateTime.Parse(beginDate);
            var end = DateTime.Parse(endDate);
            var info = (from c in _repo.Worksheet<CdmaRegionStatExcel>(_worksheetName)
                        where c.StatDate > begin && c.StatDate <= end.AddDays(1)
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, lines);
        }
    }
}
