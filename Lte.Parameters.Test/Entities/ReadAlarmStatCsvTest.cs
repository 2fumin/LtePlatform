using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.ExcelCsv;
using Lte.Parameters.MockOperations;
using NUnit.Framework;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class ReadAlarmStatCsvTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            CoreMapperService.MapAlarms();
        }
        
        [Test]
        public void Test_ZteFile()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var csvFilesDirectory = Path.Combine(testDirectory, "CsvFiles");
            var path = Path.Combine(csvFilesDirectory, "当前告警查询20160405(所有列).csv");

            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));

            var infos = CsvContext.Read<AlarmStatCsv>(reader, CsvFileDescription.CommaDescription).ToList();
            Assert.AreEqual(infos.Count, 397);
            var info1 = infos[0];
            Assert.IsNotNull(info1);
            Assert.AreEqual(info1.ENodebId, 552136);
            var stat1 = Mapper.Map<AlarmStatCsv, AlarmStat>(info1);
            Assert.IsNotNull(stat1);
            Assert.AreEqual(stat1.ENodebId, 552136);
        }
    }
}
