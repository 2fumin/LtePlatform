using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Evaluations.DataService.Dump
{
    [TestFixture]
    public class FlowServiceTest
    {
        private FlowService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            MySqlMapperService.MapFlow();
        }

        [SetUp]
        public void Setup()
        {
            _service = new FlowService(null, null);
        }

        [TestCase(@"开始时间,周期 (分钟),网元名称,小区,小区PDCP层所发送的下行数据的总吞吐量 (比特),小区PDCP层所接收到的上行数据的总吞吐量 (比特),小区可用时长 (秒),小区内的平均用户数 (无),小区内的最大用户数 (无),平均RRC连接许可用户数 (无),平均激活用户数 (无),最大激活用户数 (无),上行平均激活用户数 (无),上行最大激活用户数 (无),下行平均激活用户数 (无),下行最大激活用户数 (无),小区下行有数据传输总时长(1ms精度) (毫秒),小区上行有数据传输总时长(1ms精度) (毫秒),小区Uu接口寻呼用户个数 (无),下行Physical Resource Block被使用的平均个数 (无),下行PDSCH DRB的Physical Resource Block被使用的平均个数 (无),下行可用的PRB个数 (无),上行Physical Resource Block被使用的平均个数 (无),上行PUSCH DRB的Physical Resource Block被使用的平均个数 (无),上行可用的PRB个数 (无),小区接收到属于Group A的Preamble消息次数 (无),小区接收到属于Group B的Preamble消息的次数 (无),小区接收到专用前导消息的次数 (无),统计周期内上行DCI所使用的PDCCH CCE个数 (无),统计周期内下行DCI所使用的PDCCH CCE个数 (无),公共DCI所占用的PDCCH CCE的个数 (无),统计周期内可用的PDCCH CCE的个数 (无),PUCCH的PRB资源分配的平均值 (无),使UE缓存为空的最后一个TTI所传的上行PDCP吞吐量 (比特),扣除使UE缓存为空的最后一个TTI之后的上行数传时长 (毫秒),使缓存为空的最后一个TTI所传的下行PDCP吞吐量 (比特),扣除使下行缓存为空的最后一个TTI之后的数传时长 (毫秒)
05/03/2016 00:00:00,60,桂洲泰安,""eNodeB名称 = 桂洲泰安, 本地小区标识=2, 小区名称=桂洲泰安_2, eNodeB标识=500420, 小区双工模式=CELL_FDD"",155539608,34640208,3600,2.961,9,1800,0.17,4,0.087,3,0.084,3,30369,104620,74961,1.125,0.377,100,4.553,0.153,100,421,80,187,1004038,1893090,0,62048137,3.723,21079608,82893,104557368,1407
05/03/2016 00:00:00, 60, 桂洲泰安, ""eNodeB名称=桂洲泰安, 本地小区标识=1, 小区名称=桂洲泰安_1, eNodeB标识=500420, 小区双工模式=CELL_FDD"", 66947936, 10010472, 3600, 0.952, 5, 1800, 0.018, 2, 0.008, 2, 0.01, 2, 5253, 8558, 74961, 0.827, 0.082, 100, 2.651, 0.028, 100, 153, 1, 512, 114845, 1697797, 0, 61245849, 2.001, 4172416, 5237, 25406104, 1074
05/03/2016 00:00:00, 60, 桂洲泰安, ""eNodeB名称=桂洲泰安, 本地小区标识=0, 小区名称=桂洲泰安_0, eNodeB标识=500420, 小区双工模式=CELL_FDD"", 130648456, 43020664, 3600, 2.5, 8, 1800, 0.077, 2, 0.028, 2, 0.05, 2, 17257, 35421, 74961, 1.053, 0.309, 100, 2.747, 0.098, 100, 410, 4, 140, 361468, 1780042, 0, 61427119, 2.004, 28435776, 18907, 76047872, 1264
05/03/2016 00:00:00, 60, 桂洲泰安, ""eNodeB名称=桂洲泰安, 本地小区标识=5, 小区名称=桂洲泰安_5, eNodeB标识=500420, 小区双工模式=CELL_FDD"", 192520816, 41654760, 3600, 4.719, 12, 1800, 0.105, 4, 0.037, 3, 0.07, 4, 29949, 46271, 74961, 1.182, 0.338, 75, 2.877, 0.092, 75, 1221, 60, 101, 634326, 1925065, 0, 47626030, 2.096, 22320736, 24186, 80191088, 3500
05/03/2016 00:00:00, 60, 桂洲泰安, ""eNodeB名称=桂洲泰安, 本地小区标识=4, 小区名称=桂洲泰安_4, eNodeB标识=500420, 小区双工模式=CELL_FDD"", 400688792, 36056976, 3600, 2.95, 9, 1800, 0.066, 4, 0.018, 3, 0.05, 3, 17542, 21659, 74961, 1.039, 0.222, 75, 2.738, 0.062, 75, 542, 13, 68, 249915, 1747418, 0, 44664330, 2.032, 17805904, 10244, 119638240, 3433",
            5, 2.95)]
        public void Test_ReadFromMemory(string testInput, int count, double averageUsers)
        {
            var reader = testInput.GetStreamReader();
            _service.UploadFlowHuaweis(reader);
            Assert.AreEqual(_service.FlowHuaweiCount, count);
            var item = _service.GetTopHuaweiItem();
            Assert.AreEqual(item.AverageUsers, averageUsers, 1E-7);
        }

        [Test]
        public void Test_Integrity()
        {
            var testDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CsvFiles");
            var path = Path.Combine(testDir, "4G话务模型(05042016 144512)_20160504_144519.csv");
            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            _service.UploadFlowHuaweis(reader);
            Assert.AreEqual(_service.FlowHuaweiCount, 5610);
            var item = _service.GetTopHuaweiItem();
            Assert.AreEqual(item.AverageUsers, 1.718, 1E-7);
            Assert.AreEqual(item.ENodebId, 552552);
        }
    }
}
