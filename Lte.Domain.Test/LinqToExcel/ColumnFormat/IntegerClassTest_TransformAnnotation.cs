using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using Lte.Domain.LinqToExcel;
using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.LinqToExcel.ColumnFormat
{
    [TestFixture]
    public class IntegerClassTest_TransformAnnotation : SQLLogStatements_Helper
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
            _excelFileName = Path.Combine(excelFilesDirectory, "IntegerFormat.xls");
        }

        [SetUp]
        public void s()
        {
            _repo = new ExcelQueryFactory { FileName = _excelFileName };
        }

        [Test]
        public void Test_SaftyChangeTransform()
        {
            _worksheetName = "DoublePoints";
            _loggedEvents.Clear();

            var rows = (from c in _repo.Worksheet<IntegerClassWithDefaultTransform>(_worksheetName)
                        select c).ToList();

            Assert.AreEqual(rows.Count, 2);
            Assert.AreEqual(rows[0].IntegerColumn, 0);
            Assert.AreEqual(rows[1].IntegerColumn, 0);

            var events = _loggedEvents.GetEvents();
            Console.Write("0:{0}\n1:{1}\n2:{2}",
                events[0].RenderedMessage, events[1].RenderedMessage, events[2].RenderedMessage);
            var warningEvents = events.Where(x => x.Level == Level.Warn || x.Level == Level.Alert);
            Assert.AreEqual(warningEvents.Count(), 0);
        }

        [Test]
        public void Test_RemovePointsSaveTransform()
        {
            _worksheetName = "DoublePoints";
            _loggedEvents.Clear();

            var rows = (from c in _repo.Worksheet<IntegerClassWithDotsTransform>(_worksheetName)
                        select c).ToList();

            Assert.AreEqual(rows.Count, 2);
            Assert.AreEqual(rows[0].IntegerColumn, 25);
            Assert.AreEqual(rows[1].IntegerColumn, 7);

            var events = _loggedEvents.GetEvents();
            Console.Write("0:{0}\n1:{1}\n2:{2}",
                events[0].RenderedMessage, events[1].RenderedMessage, events[2].RenderedMessage);
            var warningEvents = events.Where(x => x.Level == Level.Warn || x.Level == Level.Alert);
            Assert.AreEqual(warningEvents.Count(), 0);
        }
    }
}
