﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using Lte.Domain.LinqToExcel;
using Lte.Domain.Test.LinqToExcel;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Parameters.Test.Excel
{
    [TestFixture]
    public class CellExcelTest : SQLLogStatements_Helper
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
            _excelFileName = Path.Combine(excelFilesDirectory, "Cell.xlsx");
            _worksheetName = "Cell";
        }

        [SetUp]
        public void s()
        {
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
        }

        [Test]
        public void Test_Read_Sheet()
        {
            var info = (from c in _repo.Worksheet<CellExcel>(_worksheetName)
                select c).ToList();

            Assert.IsNotNull(info);
            Assert.AreEqual(info.Count, 5);
            info[0].ENodebId.ShouldEqual(499773);
            info[1].SectorId.ShouldEqual((byte)1);
            info[2].AntennaInfo.ShouldEqual("6端口三频C/F/T");
        }
    }
}