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

namespace Lte.Parameters.Test.Excel
{
    [TestFixture]
    public class RealENodebExcelTest : SQLLogStatements_Helper
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
            _worksheetName = "基站级";
        }

        [SetUp]
        public void s()
        {
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
        }

        [Test]
        public void Test_ReadENodebs_Sheet()
        {
            var info = (from c in _repo.Worksheet<ENodebExcel>(_worksheetName)
                select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 5566);
        }

        [Test]
        public void Test_ReadENodebs_District_Chancheng()
        {
            var info = (from c in _repo.Worksheet<ENodebExcel>(_worksheetName)
                        where c.DistrictName == "禅城"
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 890);
        }

        [Test]
        public void Test_ReadENodebs_District_Nanhai()
        {
            var info = (from c in _repo.Worksheet<ENodebExcel>(_worksheetName)
                        where c.DistrictName == "南海"
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 1892);
        }

        [Test]
        public void Test_ReadENodebs_District_Shunde()
        {
            var info = (from c in _repo.Worksheet<ENodebExcel>(_worksheetName)
                        where c.DistrictName == "顺德"
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 1903);
        }

        [Test]
        public void Test_ReadENodebs_District_Sanshui()
        {
            var info = (from c in _repo.Worksheet<ENodebExcel>(_worksheetName)
                        where c.DistrictName == "三水"
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 521);
        }

        [Test]
        public void Test_ReadENodebs_District_Gaoming()
        {
            var info = (from c in _repo.Worksheet<ENodebExcel>(_worksheetName)
                        where c.DistrictName == "高明"
                        select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 360);
        }
    }
}
