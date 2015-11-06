using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using Lte.Domain.LinqToExcel;
using Lte.Domain.LinqToExcel.Entities;
using NUnit.Framework;

namespace Lte.Domain.Test.LinqToExcel.ColumnFormat
{
    [TestFixture]
    public class IntegerClassTest_ColumnAnnotation : SQLLogStatements_Helper
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
        public void Test_NormalCase()
        {
            _worksheetName = "Normal";

            var rows = (from c in _repo.Worksheet<IntegerClassWithColumnAnnotation>(_worksheetName)
                        select c).ToList();

            Assert.AreEqual(rows.Count, 3);
            Assert.AreEqual(rows[0].StringColumn, "1");
            Assert.AreEqual(rows[1].IntegerColumn, 7);
            Assert.AreEqual(rows[2].StringColumn, "abc");
        }
        
        [Test]
        public void Test_EmptyCase()
        {
            _worksheetName = "EmptyCase";
            _loggedEvents.Clear();

            var rows = (from c in _repo.Worksheet<IntegerClassWithColumnAnnotation>(_worksheetName)
                        select c).ToList();

            Assert.AreEqual(rows.Count, 2);
            Assert.AreEqual(rows[0].IntegerColumn, 0);
            Assert.AreEqual(rows[1].IntegerColumn, 7);

            var events = _loggedEvents.GetEvents();
            Console.Write("0:{0}\n1:{1}\n2:{2}",
                events[0].RenderedMessage, events[1].RenderedMessage, events[2].RenderedMessage);
            var warningEvents = events.Where(x => x.Level == Level.Warn || x.Level == Level.Alert);
            Assert.AreEqual(warningEvents.Count(), 0);
        }

        [Test]
        public void Test_DotCase()
        {
            _worksheetName = "DotCase";
            _loggedEvents.Clear();

            var rows = (from c in _repo.Worksheet<IntegerClassWithColumnAnnotation>(_worksheetName)
                        select c).ToList();

            Assert.AreEqual(rows.Count, 2);
            Assert.AreEqual(rows[0].IntegerColumn, 2, "The first row should be 2.");
            Assert.AreEqual(rows[1].IntegerColumn, 7);

            var events = _loggedEvents.GetEvents();
            Console.Write("0:{0}\n1:{1}\n2:{2}",
                events[0].RenderedMessage, events[1].RenderedMessage, events[2].RenderedMessage);
            var warningEvents = events.Where(x => x.Level == Level.Warn || x.Level == Level.Alert);
            Assert.AreEqual(warningEvents.Count(), 0);
        }

        [Test]
        public void Test_NegativeCase()
        {
            _worksheetName = "NegativeCase";
            _loggedEvents.Clear();

            var rows = (from c in _repo.Worksheet<IntegerClassWithColumnAnnotation>(_worksheetName)
                        select c).ToList();

            Assert.AreEqual(rows.Count, 2);
            Assert.AreEqual(rows[0].IntegerColumn, -2);
            Assert.AreEqual(rows[1].IntegerColumn, -7);

            var events = _loggedEvents.GetEvents();
            Console.Write("0:{0}\n1:{1}\n2:{2}",
                events[0].RenderedMessage, events[1].RenderedMessage, events[2].RenderedMessage);
            var warningEvents = events.Where(x => x.Level == Level.Warn || x.Level == Level.Alert);
            Assert.AreEqual(warningEvents.Count(), 0);
        }

        [Test]
        [ExpectedException(typeof(FormatException), ExpectedMessage = "输入字符串的格式不正确。")]
        public void Test_DoublePoints()
        {
            _worksheetName = "DoublePoints";
            _loggedEvents.Clear();

            var rows = (from c in _repo.Worksheet<IntegerClassWithColumnAnnotation>(_worksheetName)
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
