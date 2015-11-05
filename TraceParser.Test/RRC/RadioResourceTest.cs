using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;
using TraceParser.Eutra;
using TraceParser.Outputs;

namespace TraceParser.Test.RRC
{
    [TestFixture]
    public class RadioResourceTest
    {
        [TestCase("26 02 37 38 14 BB 9C 43 27 00 3E A2 68 02 97 73 88 1B 8C CF A0 06 EE 92 18 71 FC 16 04 AC B2 A8 D9 C0 00 8C A1 2A 18",
            new[] { "SRB ID:2, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
            + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:3, Prioritised bit rate:infinity, "
            + "Bucket size duration:50ms, Logical channel group:0" })]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00 ",
            new[]
            {
                "SRB ID:1, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
                + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:1, Prioritised bit rate:infinity, "
                + "Bucket size duration:500ms, Logical channel group:0",
                "SRB ID:2, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
                + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:3, Prioritised bit rate:infinity, "
                + "Bucket size duration:50ms, Logical channel group:0"
            })]
        [TestCase("22 1B 28 A0 00 30 04 43 41 19 E8 C8 03 22 35 A5 54 19 04 A5 FE 71 63 69 7F 93 70 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 00 FC 16 20 FB B3 80 01 14 A1 4A 1A 00 02 61 02 00 ",
            new[]
            {
                "SRB ID:1, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
                + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:1, Prioritised bit rate:infinity, "
                + "Bucket size duration:500ms, Logical channel group:0",
                "SRB ID:2, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
                + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:3, Prioritised bit rate:infinity, "
                + "Bucket size duration:50ms, Logical channel group:0"
            })]
        [TestCase("20 1b 28 a0 00 30 04 43 41 19 e4 08 03 22 35 a5 54 19 28 f9 fe 71 63 69 7f 91 e0 a4 59 9a 81 90 cc a0 40 48 1d db 4d 45 d5 a0 37 b0 14 bb 9c 43 07 83 81 4b b9 c4 32 70 01 a2 20 14 bb 9c 47 43 33 e8 00 fc 16 20 f5 b3 80 00 94 a1 4a 1a 04 83 a1 02 00",
            new[]
            {
                "SRB ID:1, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
                + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:1, Prioritised bit rate:infinity, "
                + "Bucket size duration:500ms, Logical channel group:0",
                "SRB ID:2, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
                + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:3, Prioritised bit rate:infinity, "
                + "Bucket size duration:50ms, Logical channel group:0"
            })]
        public void Test_RadioResource_Srb(string source, string[] description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            RadioResourceConfigDedicated config =
                result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.radioResourceConfigDedicated;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.srb_ToAddModList);
            for (int i = 0; i < config.srb_ToAddModList.Count; i++)
            {
                Assert.AreEqual(config.srb_ToAddModList[i].GetOutputs(), description[i]);
            }
        }

        [TestCase("26 02 37 38 14 BB 9C 43 27 00 3E A2 68 02 97 73 88 1B 8C CF A0 06 EE 92 18 71 FC 16 04 AC B2 A8 D9 C0 00 8C A1 2A 18",
            new[] { "EpsId:5, DrbId:3, PDCP config: Discard Timer:500ms, RLC AM Status Report Required:False, Header Compression:not used, "
            + "RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, Downlink:Reordering timer:35ms, "
            + "Status prohibit timer:20ms, Logical channel Id:3, Logical channel config:Priority:8, Prioritised bit rate:8kB, "
            + "Bucket size duration:500ms, Logical channel group:3" })]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00 ",
            new[] { "EpsId:, DrbId:3, PDCP config: Discard Timer:undefined, RLC AM Status Report Required:False, Header Compression:not used, "
            + "RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, Downlink:Reordering timer:35ms, "
            + "Status prohibit timer:20ms, Logical channel Id:, Logical channel config:Priority:8, Prioritised bit rate:8kB, "
            + "Bucket size duration:500ms, Logical channel group:3" })]
        [TestCase("20 1b 28 a0 00 30 04 43 41 19 e4 08 03 22 35 a5 54 19 28 f9 fe 71 63 69 7f 91 e0 a4 59 9a 81 90 cc a0 40 48 1d db 4d 45 d5 a0 37 b0 14 bb 9c 43 07 83 81 4b b9 c4 32 70 01 a2 20 14 bb 9c 47 43 33 e8 00 fc 16 20 f5 b3 80 00 94 a1 4a 1a 04 83 a1 02 00",
            new[] { "EpsId:, DrbId:3, PDCP config: Discard Timer:undefined, RLC AM Status Report Required:False, Header Compression:not used, "
            + "RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, Downlink:Reordering timer:35ms, "
            + "Status prohibit timer:20ms, Logical channel Id:, Logical channel config:Priority:11, Prioritised bit rate:8kB, "
            + "Bucket size duration:500ms, Logical channel group:3" })]
        public void Test_RadioResource_Drb(string source, string[] description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            RadioResourceConfigDedicated config =
                result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.radioResourceConfigDedicated;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.drb_ToAddModList);
            for (int i = 0; i < config.drb_ToAddModList.Count; i++)
            {
                Assert.AreEqual(config.drb_ToAddModList[i].GetOutputs(), description[i]);
            }
        }

        [TestCase("26 02 37 38 14 BB 9C 43 27 00 3E A2 68 02 97 73 88 1B 8C CF A0 06 EE 92 18 71 FC 16 04 AC B2 A8 D9 C0 00 8C A1 2A 18",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "DRX config:On duration timer:10psf, DRX inactive timer:100psf, DRX retransmission timer:8psf, Long DRX-Cycle start offset:sf320:24, "
            + "Short DRX:Short DRX cycle:40 subframes, DRX short cycle timer:2, Time alignment timer dedicate:infinity, "
            + "PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:3dB")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00 ",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "DRX config:On duration timer:10psf, DRX inactive timer:100psf, DRX retransmission timer:8psf, Long DRX-Cycle start offset:sf320:314, "
            + "Short DRX:Short DRX cycle:40 subframes, DRX short cycle timer:2, Time alignment timer dedicate:infinity, "
            + "PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:3dB")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 48 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 A4 5A 90 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 86 5C 7F 05 80 3F 67 00 02 3A 84 94 34 00 05 42 04 00 ",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "DRX config:On duration timer:10psf, DRX inactive timer:100psf, DRX retransmission timer:8psf, Long DRX-Cycle start offset:sf320:25, "
            + "Short DRX:Short DRX cycle:40 subframes, DRX short cycle timer:2, Time alignment timer dedicate:infinity, "
            + "PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:3dB")]
        [TestCase("20 1b 28 a0 00 30 04 43 41 19 e4 08 03 22 35 a5 54 19 28 f9 fe 71 63 69 7f 91 e0 a4 59 9a 81 90 cc a0 40 48 1d db 4d 45 d5 a0 37 b0 14 bb 9c 43 07 83 81 4b b9 c4 32 70 01 a2 20 14 bb 9c 47 43 33 e8 00 fc 16 20 f5 b3 80 00 94 a1 4a 1a 04 83 a1 02 00",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "DRX config:, Time alignment timer dedicate:infinity, "
            + "PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:3dB")]
        [TestCase("20 12 15 60 42 00 03 90 c2 26 14 00 4a 60 30 83 6a 10 70 63 42 06 8b e8 30 30 61 02 20 2c 42 02 02 84 36 51 72 c4 42 60 58 90 18 01 40 21 08 04 40 90 a2 29 80 6b 22 30 53 e8 00 fc 10 11 32",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "DRX config:, Time alignment timer dedicate:infinity, "
            + "PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:3dB")]
        public void Test_RadioResouce_MacMainConfig(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            RadioResourceConfigDedicated config =
                result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.radioResourceConfigDedicated;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.mac_MainConfig);
            Assert.AreEqual(config.mac_MainConfig.explicitValue.GetOutputs(), description);
        }

        [TestCase("26 02 37 38 14 BB 9C 43 27 00 3E A2 68 02 97 73 88 1B 8C CF A0 06 EE 92 18 71 FC 16 04 AC B2 A8 D9 C0 00 8C A1 2A 18",
            "Downlink config:release, Uplink config:release")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00 ",
            "Downlink config:release, Uplink config:release")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 48 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 A4 5A 90 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 86 5C 7F 05 80 3F 67 00 02 3A 84 94 34 00 05 42 04 00 ",
            "Downlink config:release, Uplink config:release")]
        [TestCase("20 1b 28 a0 00 30 04 43 41 19 e4 08 03 22 35 a5 54 19 28 f9 fe 71 63 69 7f 91 e0 a4 59 9a 81 90 cc a0 40 48 1d db 4d 45 d5 a0 37 b0 14 bb 9c 43 07 83 81 4b b9 c4 32 70 01 a2 20 14 bb 9c 47 43 33 e8 00 fc 16 20 f5 b3 80 00 94 a1 4a 1a 04 83 a1 02 00",
            "Downlink config:release, Uplink config:release")]
        public void Test_RadioResource_SpsConfig(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            RadioResourceConfigDedicated config =
                result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.radioResourceConfigDedicated;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.sps_Config);
            Assert.AreEqual(config.sps_Config.GetOutputs(), description);
        }

        [TestCase("26 02 37 38 14 BB 9C 43 27 00 3E A2 68 02 97 73 88 1B 8C CF A0 06 EE 92 18 71 FC 16 04 AC B2 A8 D9 C0 00 8C A1 2A 18",
            "Uplink power control dedicated:P0 UE PUSCH:1, Delta MCS enabled:en0, Accumulation enabled:True, P0 UE PUCCH:1, pSRS offset:5, Filter coefficient:8, "
            + "CQI report config:CQI report mode aperiodic:rm30, Nominal PDSCH RS EPRE offset:0, CQI report periodic:CQI PUCCH resource index:0, CQI PMI  config index:70, "
            + "CQI format indicator periodic:wideband CQI, RI config index:644, Simultaneous ack nack and CQI:True, "
            + "Antenna info:Transmission mode:TM3, Code book subset restriction:n2TxAntenna_tm3, UE transmit antenna selection:release")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00 ",
            ", CQI report config:CQI report mode aperiodic:rm30, Nominal PDSCH RS EPRE offset:0, CQI report periodic:CQI PUCCH resource index:0, CQI PMI  config index:40, "
            + "CQI format indicator periodic:wideband CQI, RI config index:644, Simultaneous ack nack and CQI:True, "
            + "Antenna info:Transmission mode:TM3, Code book subset restriction:n2TxAntenna_tm3, UE transmit antenna selection:release, "
            + "Sounding RS UL config dedicated:release, Scheduling request config:SR PUCCH resource index:36, SR config index:7, DSR transmisstion max:n64")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 48 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 A4 5A 90 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 86 5C 7F 05 80 3F 67 00 02 3A 84 94 34 00 05 42 04 00 ",
            ", CQI report config:CQI report mode aperiodic:rm30, Nominal PDSCH RS EPRE offset:0, CQI report periodic:CQI PUCCH resource index:0, CQI PMI  config index:71, "
            + "CQI format indicator periodic:wideband CQI, RI config index:644, Simultaneous ack nack and CQI:True, "
            + "Antenna info:Transmission mode:TM3, Code book subset restriction:n2TxAntenna_tm3, UE transmit antenna selection:release, "
            + "Sounding RS UL config dedicated:release, Scheduling request config:SR PUCCH resource index:0, SR config index:10, DSR transmisstion max:n64")]
        public void Test_RadioResource_PhysicalConfigDecicated(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            RadioResourceConfigDedicated config =
                result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.radioResourceConfigDedicated;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.physicalConfigDedicated);
            Assert.AreEqual(config.physicalConfigDedicated.GetOutputs(), description);
        }
    }
}
