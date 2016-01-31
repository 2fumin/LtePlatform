using System;
using System.IO;
using System.Linq;
using Lte.Domain.LinqToExcel;
using Lte.Domain.Test.LinqToExcel;
using Lte.Parameters.Entities;
using NUnit.Framework;
using Shouldly;

namespace Lte.Parameters.Test.Excel
{
    [TestFixture]
    public class RealCellExcelTest : SQLLogStatements_Helper
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
            _excelFileName = Path.Combine(excelFilesDirectory, "佛山无线中心LTE工参-20151105.xlsx");
            _worksheetName = "小区级";
        }

        [SetUp]
        public void s()
        {
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
        }

        [Test]
        public void Test_ReadCells_Sheet()
        {
            var info = (from c in _repo.Worksheet<CellExcel>(_worksheetName)
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 14999);
            var longtitutes = info.Select(x => x.Longtitute).Where(x => x > 112 && x < 114);
            longtitutes.Count().ShouldBe(14999);
        }
    }
}
