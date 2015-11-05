using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TraceParser.Eutra;
using TraceParser.Zte;

namespace TraceParser.Test.Zte
{
    [TestFixture]
    public class ZteBasicTest
    {
        [Test]
        public void msgTest()
        {
            Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
            dictionary.Add("BCCH_DL_SCH_MessageType", typeof(BCCH_DL_SCH_MessageType.c1_Type));
            dictionary.Add("MCCH_MessageType", typeof(MCCH_MessageType.c1_Type));
            dictionary.Add("PCCH_MessageType", typeof(PCCH_MessageType.c1_Type));
            dictionary.Add("DL_CCCH_MessageType", typeof(DL_CCCH_MessageType.c1_Type));
            dictionary.Add("DL_DCCH_MessageType", typeof(DL_DCCH_MessageType.c1_Type));
            dictionary.Add("UL_CCCH_MessageType", typeof(UL_CCCH_MessageType.c1_Type));
            dictionary.Add("UL_DCCH_MessageType", typeof(UL_DCCH_MessageType.c1_Type));
            foreach (KeyValuePair<string, Type> pair in dictionary)
            {
                foreach (PropertyInfo info in pair.Value.GetProperties())
                {
                    string name = info.PropertyType.Name;
                    string str2 = pair.Key.Replace("Type", "");
                    Console.WriteLine("list.Add(new ZteEvent {EventName = \"" + name 
                        + "\", EventType = \"RRC\", MsgDepend = \"" + str2 + "\"});");
                }
            }
        }

        [Test]
        public void testLongToHex()
        {
            long num = 0x8613316199695L;
            Console.WriteLine(Convert.ToDecimal(num.ToString("X")));
        }

        [TestCase("B20150925.2158+0800-eNodeB.eNodeB481184.64F0119757A0")]
        [TestCase("B20150925.2158+0800-eNodeB.eNodeB481213.64F0111757BD")]
        [TestCase("B20150925.2158+0800-eNodeB.eNodeB481316.64F011B75824")]
        public void zteTest(string fileName)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string zipDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Zte");
            string zipPath = Path.Combine(zipDir, fileName + ".zip");
            ZteTraceCollecFile result = ZteTraceParser.ParseRaw(zipPath);
            Assert.IsNotNull(result);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");
        }
    }
}
