using System.Data.OleDb;
using System.Linq;
using Lte.Domain.LinqToExcel;
using Lte.Domain.LinqToExcel.Entities;
using NUnit.Framework;

namespace Lte.Domain.Test.LinqToExcel
{
    [TestFixture]
    public class ConnectionString_UnitTests : SQLLogStatements_Helper
    {
        [TestFixtureSetUp]
        public void fs()
        {
            InstantiateLogger();
        }

        [SetUp]
        public void s()
        {
            ClearLogEvents();
        }

        [Test]
        public void xls_connection_string()
        {
            var companies = from c in ExcelQueryFactory.Worksheet(null, "spreadsheet.xls", null)
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={"spreadsheet.xls"
                    };Extended Properties=""Excel 8.0;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xls_readonly_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xls") {ReadOnly = true};

            var companies = from c in excel.Worksheet<Company>()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={"spreadsheet.xls"
                    };Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xls_with_Ace_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xls") {DatabaseEngine = ExcelDatabaseEngine.Ace};

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xls"
                    };Extended Properties=""Excel 12.0;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xls_readonly_with_Ace_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xls")
            {
                DatabaseEngine = ExcelDatabaseEngine.Ace,
                ReadOnly = true
            };

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xls"
                    };Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void unknown_file_type_connection_string()
        {
            var companies = from c in ExcelQueryFactory.Worksheet(null, "spreadsheet.dlo", null)
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={"spreadsheet.dlo"
                    };Extended Properties=""Excel 8.0;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void unknown_file_type_readonly_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.dlo") {ReadOnly = true};

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={"spreadsheet.dlo"
                    };Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void unknown_file_type_with_Ace_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.dlo") {DatabaseEngine = ExcelDatabaseEngine.Ace};

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.dlo"
                    };Extended Properties=""Excel 12.0;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void unknown_file_type_readonly_with_Ace_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.dlo")
            {
                DatabaseEngine = ExcelDatabaseEngine.Ace,
                ReadOnly = true
            };

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.dlo"
                    };Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void csv_connection_string()
        {
            var companies = from c in ExcelQueryFactory.Worksheet(null, @"C:\Desktop\spreadsheet.csv", null)
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={@"C:\Desktop"
                    };Extended Properties=""text;HDR=YES;FMT=Delimited;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void csv_readonly_connection_string()
        {
            var excel = new ExcelQueryFactory(@"C:\Desktop\spreadsheet.csv") {ReadOnly = true};

            var companies = from c in excel.Worksheet<Company>()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={@"C:\Desktop"
                    };Extended Properties=""text;HDR=YES;FMT=Delimited;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void csv_with_Ace_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory(@"C:\Desktop\spreadsheet.csv") {DatabaseEngine = ExcelDatabaseEngine.Ace};

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={@"C:\Desktop"
                    };Extended Properties=""text;Excel 12.0;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void csv_readonly_with_Ace_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory(@"C:\Desktop\spreadsheet.csv")
            {
                DatabaseEngine = ExcelDatabaseEngine.Ace,
                ReadOnly = true
            };

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={@"C:\Desktop"
                    };Extended Properties=""text;Excel 12.0;HDR=YES;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xlsx_connection_string()
        {
            var companies = from c in ExcelQueryFactory.Worksheet(null, "spreadsheet.xlsx", null)
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xlsx"
                    };Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xlsx_readonly_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xlsx") {ReadOnly = true};

            var companies = from c in excel.Worksheet<Company>()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xlsx"
                    };Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xlsx_with_Jet_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xlsx") {DatabaseEngine = ExcelDatabaseEngine.Jet};

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xlsx"
                    };Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xlsm_connection_string()
        {
            var companies = from c in ExcelQueryFactory.Worksheet(null, "spreadsheet.xlsm", null)
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xlsm"
                    };Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xlsm_with_Jet_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xlsm") {DatabaseEngine = ExcelDatabaseEngine.Jet};

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xlsm"
                    };Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xlsb_connection_string()
        {
            var companies = from c in ExcelQueryFactory.Worksheet(null, "spreadsheet.xlsb", null)
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xlsb"
                    };Extended Properties=""Excel 12.0;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xlsb_readonly_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xlsb") {ReadOnly = true};

            var companies = from c in excel.Worksheet<Company>()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xlsb"
                    };Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void xlsb_with_Jet_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xlsb") {DatabaseEngine = ExcelDatabaseEngine.Jet};

            var companies = from c in excel.Worksheet()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xlsb"
                    };Extended Properties=""Excel 12.0;HDR=YES;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void no_header_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xls");
            var companies = from c in excel.WorksheetNoHeader()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={"spreadsheet.xls"
                    };Extended Properties=""Excel 8.0;HDR=NO;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void no_header_readonly_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xls") {ReadOnly = true};

            var companies = from c in excel.WorksheetNoHeader()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={"spreadsheet.xls"
                    };Extended Properties=""Excel 8.0;HDR=NO;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void no_header_with_Jet_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xls") {DatabaseEngine = ExcelDatabaseEngine.Ace};

            var companies = from c in excel.WorksheetNoHeader()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xls"
                    };Extended Properties=""Excel 12.0;HDR=NO;IMEX=1""";
            Assert.AreEqual(expected, GetConnectionString());
        }

        [Test]
        public void no_header_readonly_with_Jet_DatabaseEngine_connection_string()
        {
            var excel = new ExcelQueryFactory("spreadsheet.xls")
            {
                DatabaseEngine = ExcelDatabaseEngine.Ace,
                ReadOnly = true
            };

            var companies = from c in excel.WorksheetNoHeader()
                            select c;

            try { companies.GetEnumerator(); }
            catch (OleDbException) { }
            var expected =
                $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={"spreadsheet.xls"
                    };Extended Properties=""Excel 12.0;HDR=NO;IMEX=1;READONLY=TRUE""";
            Assert.AreEqual(expected, GetConnectionString());
        }
    }
}
