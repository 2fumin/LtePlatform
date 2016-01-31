using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Lte.Domain.LinqToExcel;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;
using Lte.Domain.Test.LinqToExcel;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Parameters.Test.Excel
{
    [TestFixture]
    public class CellExcelWithTextIdTest : SQLLogStatements_Helper
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
            _excelFileName = Path.Combine(excelFilesDirectory, "CellWithTextENodebId.xlsx");
            _worksheetName = "Cell";
        }
        
        [Test]
        public void Test_Read_SheetCellExcelWithTextENodebId()
        {
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
            var info = (from c in _repo.Worksheet<CellExcelWithTextENodebId>(_worksheetName)
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 10);
            info[0].ENodebIdText.ShouldBe("502575");
        }

        [Test]
        public void Test_Read_SheetCellExcel()
        {
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
            var info = (from c in _repo.Worksheet<CellExcel>(_worksheetName)
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 10);
            info[0].ENodebId.ShouldBe(502575);
        }

        [Test]
        public void Test_Read_TextId2()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            _excelFileName = Path.Combine(excelFilesDirectory, "CellText2.xlsx");
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
            var info = (from c in _repo.Worksheet<CellExcelWithTextENodebId>(_worksheetName)
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 7);
            info[3].ENodebIdText.ShouldBe("502573");
        }

        [Test]
        public void Test_Read_TextId2_Temp()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            _excelFileName = Path.Combine(excelFilesDirectory, "Temp.xlsx");
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
            _worksheetName = "小区级";
            var info = (from c in _repo.Worksheet<CellExcelWithTextENodebId>(_worksheetName)
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 22);
            info[3].ENodebIdText.ShouldBe("500843");
            info[8].ENodebIdText.ShouldBeNull();
        }

        class CellExcelWithTextENodebId
        {
            [ExcelColumn("eNodeB ID")]
            public string ENodebIdText { get; set; }
        }
    }
}
