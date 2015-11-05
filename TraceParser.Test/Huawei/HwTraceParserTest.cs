using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using TraceParser.Huawei;

namespace TraceParser.Test.Huawei
{
    [TestFixture]
    public class HwTraceParserTest
    {
        [TestCase("B20150925.204044+0800-eNodeB.北滘碧江中学.49793", 10199)]
        [TestCase("B20150925.204044+0800-eNodeB.北滘新城区西.49789", 10404)]
        [TestCase("B20150925.204044+0800-eNodeB.大良南区电信LBBU10.49626", 116)]
        public void TestMethod(string fileName, int length)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            HwTraceFileParser parser = new HwTraceFileParser();
            string zipDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Huawei");
            string zipPath = Path.Combine(zipDir, fileName + ".gz");
            MemoryStream stream = parser.UnzipToMemoryStream(zipPath);
            stream.Position = 0L;
            parser.Parse(stream);
            Assert.AreEqual(parser.lstmsg.Count, length);
            Console.WriteLine("avg {0}ms", stopwatch.ElapsedMilliseconds/1000f);
        }

    }
}
