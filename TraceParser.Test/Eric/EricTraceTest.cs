using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Lte.Domain.Common;
using NUnit.Framework;
using TraceParser.Eric;
using TraceParser.Eutra;

namespace TraceParser.Test.Eric
{
    [TestFixture]
    public class EricTraceTest
    {
        [Test]
        public void EricTraceTestBasic()
        {
            string binPath = @"C:\Users\XF\Desktop\Asn\A20140319.1615+0800-1630+0800_SubNetwork=ONRM_ROOT_MO_R,SubNetwork=LRAN,MeContext=GZLB4015_celltracefile_DUL1_1.bin";
            EricssTrace.EricParse("", binPath);
        }

        private BitArrayInputStream GetInputStream(string HexWithOutSp)
        {
            HexWithOutSp = HexWithOutSp.Replace(" ", "");
            Stream byteStream = new MemoryStream();
            int result;
            Math.DivRem(HexWithOutSp.Length, 2, out result);
            List<byte> list = new List<byte>();
            if (result == 0)
            {
                for (int i = 0; i < HexWithOutSp.Length; i++)
                {
                    int num3 = Convert.ToInt16(HexWithOutSp.Substring(i, 2), 0x10);
                    list.Add((byte)num3);
                    i++;
                }
            }
            byte[] buffer = list.ToArray();
            byteStream.Write(buffer, 0, buffer.Length);
            byteStream.Position = 0L;
            return new BitArrayInputStream(byteStream) { Position = 0L };
        }

        [Test]
        public void ParseMutiGz()
        {
            string str5;
            Stopwatch.StartNew();
            string path = @"C:\Users\XF\Desktop\Trace\EricBin2";
            string str2 = @"C:\Users\XF\Desktop\Trace\EricXml2";
            foreach (string str3 in Directory.GetFiles(path))
            {
                EricssTrace.EricParse(Path.Combine(str2, Path.GetFileName(str3) + ".xml"), str3, true);
            }
            StreamReader reader = new StreamReader(@"C:\Users\XF\Desktop\Trace\Kt_Eric\LTE_CELL_SDR_20150119100500_DG.log");
            while ((str5 = reader.ReadLine()) != null)
            {
                string[] strArray2 = str5.Split(new[] { "##" }, StringSplitOptions.None);
                if (strArray2.Length > 8)
                {
                }
            }
        }

        [Test]
        public void 测试按行读文件()
        {
            StringBuilder builder = new StringBuilder();
            string[] strArray = { "LTE_CELL_SDR_20150119100500_DG.log", "LTE_CELL_SDR_20150119101000_DG.log", "LTE_CELL_SDR_20150119101500_DG.log", "LTE_CELL_SDR_20150119102000_DG.log", "LTE_CELL_SDR_20150119102500_DG.log", "LTE_CELL_SDR_20150119103000_DG.log", "LTE_CELL_SDR_20150119103500_DG.log", "LTE_CELL_SDR_20150119104000_DG.log" };
            string str2 = @"C:\Users\XF\Desktop\Trace\Kt_Eric\";
            foreach (string str3 in strArray)
            {
                string path = Path.Combine(str2, str3);
                string str5 = Path.Combine(str2, "result", str3 + ".txt");
                using (StreamReader reader = new StreamReader(path))
                {
                    using (StreamWriter writer = new StreamWriter(str5, true))
                    {
                        string str;
                        while ((str = reader.ReadLine()) != null)
                        {
                            string[] strArray2 = str.Split(new[] { "##" }, StringSplitOptions.None);
                            if ((strArray2.Length > 8) && (strArray2[7] == "497671"))
                            {
                                builder.AppendLine(str);
                                if (builder.Length > 0xa00000)
                                {
                                    writer.Write(builder.ToString());
                                    builder.Clear();
                                }
                            }
                        }
                        if (builder.Length > 0)
                        {
                            writer.Write(builder.ToString());
                            builder.Clear();
                        }
                    }
                }
            }
        }

        [Test]
        public void 单条信令测试()
        {
            string hexWithOutSp = "60129808FD4E018380B9807CAA8B5535B380013CA11434000340";
            BitArrayInputStream inputStream = GetInputStream(hexWithOutSp);
            inputStream.Reverse();
            DL_CCCH_Message.PerDecoder.Instance.Decode(inputStream);
        }
    }
}
