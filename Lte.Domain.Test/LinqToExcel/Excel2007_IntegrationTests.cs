﻿using System;
using System.IO;
using System.Linq;
using Lte.Domain.LinqToExcel;
using Lte.Domain.LinqToExcel.Entities;
using NUnit.Framework;

namespace Lte.Domain.Test.LinqToExcel
{
    [TestFixture]
    public class Excel2007_IntegrationTests
    {
        string _filesDirectory;

        [TestFixtureSetUp]
        public void fs()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _filesDirectory = Path.Combine(testDirectory, "ExcelFiles");
        }

        [Test]
        public void xlsx()
        {
            var fileName = Path.Combine(_filesDirectory, "Companies.xlsx");
            var companies = from c in ExcelQueryFactory.Worksheet<Company>("MoreCompanies", fileName)
                            select c;

            //Using ToList() because using Count() first would change the sql 
            //string to "SELECT COUNT(*)" which we're not testing here
            Assert.AreEqual(3, companies.ToList().Count);
        }

        [Test]
        public void xlsm()
        {
            var fileName = Path.Combine(_filesDirectory, "Companies.xlsm");
            var companies = from c in ExcelQueryFactory.Worksheet<Company>("MoreCompanies", fileName, null)
                            select c;

            //Using ToList() because using Count() first would change the sql 
            //string to "SELECT COUNT(*)" which we're not testing here
            Assert.AreEqual(3, companies.ToList().Count);
        }
        
        [Test]
        public void xls_with_Ace_DatabaseEngine()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            var excelFileName = Path.Combine(excelFilesDirectory, "Companies.xls");

            var excel = new ExcelQueryFactory(excelFileName);
            excel.DatabaseEngine = ExcelDatabaseEngine.Ace;
            var companies = from c in excel.Worksheet<Company>()
                            select c;

            Assert.AreEqual(7, companies.ToList().Count);
        }
    }
}
