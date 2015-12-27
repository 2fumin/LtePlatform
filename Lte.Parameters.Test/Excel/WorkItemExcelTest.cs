using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToExcel;
using Lte.Domain.Test.LinqToExcel;
using Lte.Parameters.Entities.Work;
using NUnit.Framework;

namespace Lte.Parameters.Test.Excel
{
    [TestFixture]
    public class WorkItemExcelTest : SQLLogStatements_Helper
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
            _excelFileName = Path.Combine(excelFilesDirectory, "工单查询结果_20151225145352.xls");
            _worksheetName = "工单查询结果";
        }

        [SetUp]
        public void s()
        {
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
        }

        [Test]
        public void Test_Read_Sheet()
        {
            var info = (from c in _repo.Worksheet<WorkItemExcel>(_worksheetName)
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 1004);
        }
    }
}
