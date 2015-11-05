using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;
using TraceParser.Eutra;
using TraceParser.Outputs;

namespace TraceParser.Test.RRC
{
    [TestFixture]
    public class MeasureConfigTest
    {
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 00 81 23 04 21 00 89 4F C6 09 29 B0 48 06 00 00 20 08 80 21 80 68 23 00",
            new[] { "1, Carrier:100, Bandwidth:mbw100, Presence Antenna Port 1:False, Neighbor Cell Config:No MBSFN, Offset Freq:0dB, Measure Cycle Scell:No such field for early version" })]
        [TestCase("26 10 15 20 00 00 00 32 52 30 02 84 36 52 00 81 23 04 21 00 89 4F C6 09 29 B0 48 06 00 00 20 08 80 21 80 71 80",
            new[] { "1, Carrier:100, Bandwidth:mbw100, Presence Antenna Port 1:False, Neighbor Cell Config:No MBSFN, Offset Freq:0dB, Measure Cycle Scell:No such field for early version" })]
        [TestCase("26 10 15 A0 00 00 03 90 C2 40 02 84 38 52 00 81 0A 04 21 00 89 4F C6 C8 5E 07 01 40 01 80 20 24 A6 C1 20 20 00 00 80 22 00 86 01 90 04 48 8C",
            new[] { "1, Carrier:1825, Bandwidth:mbw75, Presence Antenna Port 1:False, Neighbor Cell Config:No MBSFN, Offset Freq:0dB, Measure Cycle Scell:No such field for early version" })]
        [TestCase("26 10 15 a0 00 00 00 32 52 30 02 84 36 51 70 81 23 04 21 00 89 4f c6 c8 6e 06 01 40 18 00 00 80 22 00 86 01 85 18",
            new[] { "1, Carrier:100, Bandwidth:mbw100, Presence Antenna Port 1:False, Neighbor Cell Config:No MBSFN, Offset Freq:0dB, Measure Cycle Scell:No such field for early version" })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 06 01 40 18 00 00 80 22 00 86 01 85 18",
            new[] { "1, Carrier:100, Bandwidth:mbw100, Presence Antenna Port 1:False, Neighbor Cell Config:No MBSFN, Offset Freq:0dB, Measure Cycle Scell:No such field for early version" })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 07 01 40 01 80 18 00 00 80 22 00 86 01 A4 46",
            new[] { "1, Carrier:100, Bandwidth:mbw100, Presence Antenna Port 1:False, Neighbor Cell Config:No MBSFN, Offset Freq:0dB, Measure Cycle Scell:No such field for early version" })]
        [TestCase("26 10 15 20 00 00 03 90 c2 40 02 84 36 51 70 81 23 04 21 00 89 4f c6 c8 6e 06 01 40 20 24 a6 c1 20 20 00 00 80 22 00 86 01 90 04 8c",
            new[] { "1, Carrier:1825, Bandwidth:mbw75, Presence Antenna Port 1:False, Neighbor Cell Config:No MBSFN, Offset Freq:0dB, Measure Cycle Scell:No such field for early version" })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 07 01 40 01 80 18 00 00 80 22 00 86 01 85 18",
            new[] { "1, Carrier:100, Bandwidth:mbw100, Presence Antenna Port 1:False, Neighbor Cell Config:No MBSFN, Offset Freq:0dB, Measure Cycle Scell:No such field for early version" })]
        public void Test_Zte_MeasObject(string source, string[] description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            MeasConfig config = result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.measConfig;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.measObjectToAddModList);
            for (int i = 0; i < config.measObjectToAddModList.Count; i++)
            {
                Assert.AreEqual(config.measObjectToAddModList[i].GetOutputs(), description[i]);
            }
        }

        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 00 81 23 04 21 00 89 4F C6 09 29 B0 48 06 00 00 20 08 80 21 80 68 23 00",
            new[]
            {
                "1, A3 Event: offset: 3, report on leave:False, Hysteresis:3, Time to trigger:160ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:1024ms, Report amount:once",
                "2, A2 Event: threshold-RSRP: 35, Hysteresis:0, Time to trigger:320ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:1024ms, Report amount:once",
                "3, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:10240ms, Report amount:infinity",
                "4, A3 Event: offset: -12, report on leave:True, Hysteresis:6, Time to trigger:1024ms, Trigger quantity:RSRP, Report quantity:same as trigger quantity, Max report cells:3, Report interval:1024ms, Report amount:once"
            })]
        [TestCase("26 10 15 20 00 00 00 32 52 30 02 84 36 52 00 81 23 04 21 00 89 4F C6 09 29 B0 48 06 00 00 20 08 80 21 80 71 80",
            new[]
            {
                "1, A3 Event: offset: 3, report on leave:False, Hysteresis:3, Time to trigger:160ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:1024ms, Report amount:once",
                "2, A2 Event: threshold-RSRP: 35, Hysteresis:0, Time to trigger:320ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:1024ms, Report amount:once",
                "3, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:10240ms, Report amount:infinity",
                "4, A3 Event: offset: -12, report on leave:True, Hysteresis:6, Time to trigger:1024ms, Trigger quantity:RSRP, Report quantity:same as trigger quantity, Max report cells:3, Report interval:1024ms, Report amount:once"
            })]
        [TestCase("26 10 15 a0 00 00 00 32 52 30 02 84 36 51 70 81 23 04 21 00 89 4f c6 c8 6e 06 01 40 18 00 00 80 22 00 86 01 85 18",
            new[]
            {
                "1, A3 Event: offset: 3, report on leave:False, Hysteresis:3, Time to trigger:160ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:480ms, Report amount:infinity",
                "2, A2 Event: threshold-RSRP: 35, Hysteresis:0, Time to trigger:320ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:1024ms, Report amount:once",
                "3, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:10240ms, Report amount:infinity",
                "4, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:5120ms, Report amount:infinity, UE Rx/Tx time diferent periodical:setup"
            })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 06 01 40 18 00 00 80 22 00 86 01 85 18",
            new[]
            {
                "1, A3 Event: offset: 3, report on leave:False, Hysteresis:3, Time to trigger:160ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:1024ms, Report amount:twice",
                "2, A2 Event: threshold-RSRP: 40, Hysteresis:0, Time to trigger:320ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:1024ms, Report amount:once",
                "3, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:10240ms, Report amount:infinity",
                "4, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:5120ms, Report amount:infinity, UE Rx/Tx time diferent periodical:setup"
            })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 07 01 40 01 80 18 00 00 80 22 00 86 01 A4 46",
            new[]
            {
                "1, A3 Event: offset: 3, report on leave:False, Hysteresis:3, Time to trigger:160ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:1024ms, Report amount:twice",
                "2, A2 Event: threshold-RSRP: 40, Hysteresis:0, Time to trigger:320ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:1024ms, Report amount:once",
                "3, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:10240ms, Report amount:infinity",
                "4, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:5120ms, Report amount:infinity, UE Rx/Tx time diferent periodical:setup, Include location info:true"
            })]
        [TestCase("26 10 15 20 00 00 03 90 c2 40 02 84 36 51 70 81 23 04 21 00 89 4f c6 c8 6e 06 01 40 20 24 a6 c1 20 20 00 00 80 22 00 86 01 90 04 8c",
            new[]
            {
                "1, A3 Event: offset: 3, report on leave:False, Hysteresis:3, Time to trigger:160ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:480ms, Report amount:infinity",
                "2, A2 Event: threshold-RSRP: 35, Hysteresis:0, Time to trigger:320ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:1024ms, Report amount:once",
                "3, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:10240ms, Report amount:infinity",
                "4, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:5120ms, Report amount:infinity, UE Rx/Tx time diferent periodical:setup",
                "5, A3 Event: offset: -12, report on leave:True, Hysteresis:6, Time to trigger:1024ms, Trigger quantity:RSRP, Report quantity:same as trigger quantity, Max report cells:3, Report interval:1024ms, Report amount:once"
            })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 07 01 40 01 80 18 00 00 80 22 00 86 01 85 18",
            new[]
            {
                "1, A3 Event: offset: 3, report on leave:False, Hysteresis:3, Time to trigger:160ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:1024ms, Report amount:twice",
                "2, A2 Event: threshold-RSRP: 40, Hysteresis:0, Time to trigger:320ms, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:1024ms, Report amount:once",
                "3, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:3, Report interval:10240ms, Report amount:infinity",
                "4, Periodical: purpose: report strongest cells, Trigger quantity:RSRP, Report quantity:both, Max report cells:1, Report interval:5120ms, Report amount:infinity, UE Rx/Tx time diferent periodical:setup, Include location info:true"
            })]
        public void Test_Zte_ReportConfig(string source, string[] description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            MeasConfig config = result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.measConfig;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.reportConfigToAddModList);
            for (int i = 0; i < config.reportConfigToAddModList.Count; i++)
            {
                Assert.AreEqual(config.reportConfigToAddModList[i].GetOutputs(), description[i]);
            }
        }

        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 00 81 23 04 21 00 89 4F C6 09 29 B0 48 06 00 00 20 08 80 21 80 68 23 00",
            new[]
            {
                "measId: 1, measObjectId: 1, reportConfigId: 1",
                "measId: 2, measObjectId: 1, reportConfigId: 2",
                "measId: 3, measObjectId: 1, reportConfigId: 3",
                "measId: 4, measObjectId: 1, reportConfigId: 4"
            })]
        [TestCase("26 10 15 20 00 00 00 32 52 30 02 84 36 52 00 81 23 04 21 00 89 4F C6 09 29 B0 48 06 00 00 20 08 80 21 80 71 80",
            new[]
            {
                "measId: 1, measObjectId: 1, reportConfigId: 1",
                "measId: 2, measObjectId: 1, reportConfigId: 2",
                "measId: 3, measObjectId: 1, reportConfigId: 3",
                "measId: 4, measObjectId: 1, reportConfigId: 4"
            })]
        [TestCase("26 10 15 A0 00 00 03 90 C2 40 02 84 38 52 00 81 0A 04 21 00 89 4F C6 C8 5E 07 01 40 01 80 20 24 A6 C1 20 20 00 00 80 22 00 86 01 90 04 48 8C",
            new[]
            {
                "measId: 1, measObjectId: 1, reportConfigId: 1",
                "measId: 2, measObjectId: 1, reportConfigId: 2",
                "measId: 3, measObjectId: 1, reportConfigId: 3",
                "measId: 4, measObjectId: 1, reportConfigId: 4",
                "measId: 5, measObjectId: 1, reportConfigId: 5"
            })]
        [TestCase("26 10 15 a0 00 00 00 32 52 30 02 84 36 51 70 81 23 04 21 00 89 4f c6 c8 6e 06 01 40 18 00 00 80 22 00 86 01 85 18",
            new[]
            {
                "measId: 1, measObjectId: 1, reportConfigId: 1",
                "measId: 2, measObjectId: 1, reportConfigId: 2",
                "measId: 3, measObjectId: 1, reportConfigId: 3",
                "measId: 4, measObjectId: 1, reportConfigId: 4"
            })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 06 01 40 18 00 00 80 22 00 86 01 85 18",
            new[]
            {
                "measId: 1, measObjectId: 1, reportConfigId: 1",
                "measId: 2, measObjectId: 1, reportConfigId: 2",
                "measId: 3, measObjectId: 1, reportConfigId: 3",
                "measId: 4, measObjectId: 1, reportConfigId: 4"
            })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 07 01 40 01 80 18 00 00 80 22 00 86 01 A4 46",
            new[]
            {
                "measId: 1, measObjectId: 1, reportConfigId: 1",
                "measId: 2, measObjectId: 1, reportConfigId: 2",
                "measId: 3, measObjectId: 1, reportConfigId: 3",
                "measId: 4, measObjectId: 1, reportConfigId: 4"
            })]
        [TestCase("26 10 15 20 00 00 03 90 c2 40 02 84 36 51 70 81 23 04 21 00 89 4f c6 c8 6e 06 01 40 20 24 a6 c1 20 20 00 00 80 22 00 86 01 90 04 8c",
            new[]
            {
                "measId: 1, measObjectId: 1, reportConfigId: 1",
                "measId: 2, measObjectId: 1, reportConfigId: 2",
                "measId: 3, measObjectId: 1, reportConfigId: 3",
                "measId: 4, measObjectId: 1, reportConfigId: 4",
                "measId: 5, measObjectId: 1, reportConfigId: 5"
            })]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 07 01 40 01 80 18 00 00 80 22 00 86 01 85 18",
            new[]
            {
                "measId: 1, measObjectId: 1, reportConfigId: 1",
                "measId: 2, measObjectId: 1, reportConfigId: 2",
                "measId: 3, measObjectId: 1, reportConfigId: 3",
                "measId: 4, measObjectId: 1, reportConfigId: 4"
            })]
        public void Test_Zte_MeasId(string source, string[] description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            MeasConfig config = result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.measConfig;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.measIdToAddModList);
            for (int i = 0; i < config.measIdToAddModList.Count; i++)
            {
                Assert.AreEqual(config.measIdToAddModList[i].GetOutputs(), description[i]);
            }
        }

    }
}
