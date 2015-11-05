using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;
using TraceParser.Eutra;
using TraceParser.Outputs;

namespace TraceParser.Test.RRC
{
    [TestFixture]
    public class MeasureMaintainousTest
    {
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00 ",
            "Measure object to remove list:1,Report config to remove list:1,2,3,4,Quantity config:Filter coefficient RSRP:4, Filter coefficient RSRQ:4, S-measure:70")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 48 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 A4 5A 90 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 86 5C 7F 05 80 3F 67 00 02 3A 84 94 34 00 05 42 04 00 ",
            "Measure object to remove list:1,Report config to remove list:1,2,3,4,Quantity config:Filter coefficient RSRP:4, Filter coefficient RSRQ:4, S-measure:70")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE DC 7F 05 80 3F 67 00 01 4A 84 94 34 12 05 42 04 00 ",
            "Measure object to remove list:1,Report config to remove list:1,2,3,4,Quantity config:Filter coefficient RSRP:4, Filter coefficient RSRQ:4, S-measure:70")]
        public void Test_RemoveMeasList(string source, string description)
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
            Assert.IsNotNull(config.measObjectToRemoveList);
            Assert.IsNotNull(config.reportConfigToRemoveList);
            Assert.AreEqual(config.GetMaintainousOutputs(), description);
        }

        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00 ",
            "Target PCI:389, Carrier frequency:Downlink:100, Uplink:18100, Carrier bandwidth:Downlink:20M, Uplink:20M, Additional spectrum emission:1, T304:2000ms, New UE-ID:0011110000101000'B,"
            + " Radio resource common:RACH config common:Preamble info:Number of RA preambles:52, Preambles group-A config:Size of RA preamble group-A:48, Message size group-A:56bits, Message power offset group-B:8dB,"
            + " Power ramping parameters:Power ramping step:2dB, Preamble initial received target power:-100dBm, RA supervision info:Preamble Transmission max:8, RA response window size:10 subframes, MAC contention resolution timer:64 subframes,"
            + " Max HRAQ msg3 Transmissions:5, PRACH config:Root sequence index:552, Prach config info:PRACH config index:11, High-speed flag:False, Zero-correlation zone config:8, PRACH frequency offset:90,"
            + " PDSCH config common:Reference signal power:15, Pb:1, PUSCH config common:PUSCH config basic:nSB:1, Hopping mode:inter subframes, PUSCH hopping offset:12, Enabled 64QAM:True,"
            + " UL RS PUSCH:Group hopping enabled:False, Group assignment PUSCH:3, Sequence hopping enabled:False, Cyclic shift:3, PHICH config:PHICH duration:normal, PHICH resource:half,"
            + " PUCCH config common:Delta PUCCH shift:ds1, nRB CQI:1, nCS AN:0, n1 PUCCH AN:144, Sounding RS UL config common:release,"
            + " Uplink power control common:P0 norminal PUSCH:-67, Alpha:8, P0 nominal PUCCH:-105, Delta F list PUCCH:Format1:2, Format1b:3, Format2:1, Format2a:2, Format2b:2, Delta preamble msg3:0,"
            + " Antenna Info common:Antenna ports count:2, p-Max:23, UL cyclic prfix length:1, RACH config dedicated:RA preamble index:52, RA PRACH mask index:0")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 48 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 A4 5A 90 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 86 5C 7F 05 80 3F 67 00 02 3A 84 94 34 00 05 42 04 00 ",
            "Target PCI:388, Carrier frequency:Downlink:100, Uplink:18100, Carrier bandwidth:Downlink:20M, Uplink:20M, Additional spectrum emission:1, T304:2000ms, New UE-ID:0011110000101000'B,"
            + " Radio resource common:RACH config common:Preamble info:Number of RA preambles:52, Preambles group-A config:Size of RA preamble group-A:48, Message size group-A:56bits, Message power offset group-B:8dB,"
            + " Power ramping parameters:Power ramping step:2dB, Preamble initial received target power:-100dBm, RA supervision info:Preamble Transmission max:8, RA response window size:10 subframes, MAC contention resolution timer:64 subframes,"
            + " Max HRAQ msg3 Transmissions:5, PRACH config:Root sequence index:552, Prach config info:PRACH config index:10, High-speed flag:False, Zero-correlation zone config:8, PRACH frequency offset:90,"
            + " PDSCH config common:Reference signal power:12, Pb:1, PUSCH config common:PUSCH config basic:nSB:1, Hopping mode:inter subframes, PUSCH hopping offset:12, Enabled 64QAM:True,"
            + " UL RS PUSCH:Group hopping enabled:False, Group assignment PUSCH:3, Sequence hopping enabled:False, Cyclic shift:3, PHICH config:PHICH duration:normal, PHICH resource:half,"
            + " PUCCH config common:Delta PUCCH shift:ds1, nRB CQI:1, nCS AN:0, n1 PUCCH AN:144, Sounding RS UL config common:release,"
            + " Uplink power control common:P0 norminal PUSCH:-67, Alpha:8, P0 nominal PUCCH:-105, Delta F list PUCCH:Format1:2, Format1b:3, Format2:1, Format2a:2, Format2b:2, Delta preamble msg3:0,"
            + " Antenna Info common:Antenna ports count:2, p-Max:23, UL cyclic prfix length:1, RACH config dedicated:RA preamble index:52, RA PRACH mask index:0")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE DC 7F 05 80 3F 67 00 01 4A 84 94 34 12 05 42 04 00 ",
            "Target PCI:389, Carrier frequency:Downlink:100, Uplink:18100, Carrier bandwidth:Downlink:20M, Uplink:20M, Additional spectrum emission:1, T304:2000ms, New UE-ID:0011110000101000'B,"
            + " Radio resource common:RACH config common:Preamble info:Number of RA preambles:52, Preambles group-A config:Size of RA preamble group-A:48, Message size group-A:56bits, Message power offset group-B:8dB,"
            + " Power ramping parameters:Power ramping step:2dB, Preamble initial received target power:-100dBm, RA supervision info:Preamble Transmission max:8, RA response window size:10 subframes, MAC contention resolution timer:64 subframes,"
            + " Max HRAQ msg3 Transmissions:5, PRACH config:Root sequence index:552, Prach config info:PRACH config index:11, High-speed flag:False, Zero-correlation zone config:8, PRACH frequency offset:90,"
            + " PDSCH config common:Reference signal power:15, Pb:1, PUSCH config common:PUSCH config basic:nSB:1, Hopping mode:inter subframes, PUSCH hopping offset:12, Enabled 64QAM:True,"
            + " UL RS PUSCH:Group hopping enabled:False, Group assignment PUSCH:3, Sequence hopping enabled:False, Cyclic shift:3, PHICH config:PHICH duration:normal, PHICH resource:half,"
            + " PUCCH config common:Delta PUCCH shift:ds1, nRB CQI:1, nCS AN:0, n1 PUCCH AN:144, Sounding RS UL config common:release,"
            + " Uplink power control common:P0 norminal PUSCH:-67, Alpha:8, P0 nominal PUCCH:-105, Delta F list PUCCH:Format1:2, Format1b:3, Format2:1, Format2a:2, Format2b:2, Delta preamble msg3:0,"
            + " Antenna Info common:Antenna ports count:2, p-Max:23, UL cyclic prfix length:1, RACH config dedicated:RA preamble index:52, RA PRACH mask index:0")]
        public void Test_MobilityControlInfo(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            MobilityControlInfo config =
                result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.mobilityControlInfo;
            Assert.IsNotNull(config);
            Assert.AreEqual(config.GetOutputs(), description);
        }

        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00 ",
            "Handover type:Intra LTE:Security algorithm config:Ciphering algorithm:EEA0, Integrity protection algorithm:EIA2, Key change indicator:False, Next hop chaining count:0")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 48 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 A4 5A 90 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 86 5C 7F 05 80 3F 67 00 02 3A 84 94 34 00 05 42 04 00 ",
            "Handover type:Intra LTE:Security algorithm config:Ciphering algorithm:EEA0, Integrity protection algorithm:EIA2, Key change indicator:False, Next hop chaining count:0")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 48 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 A4 5A 90 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 86 5C 7F 05 80 3F 67 00 02 3A 84 94 34 00 05 42 04 00 ",
            "Handover type:Intra LTE:Security algorithm config:Ciphering algorithm:EEA0, Integrity protection algorithm:EIA2, Key change indicator:False, Next hop chaining count:0")]
        public void Test_SecurityConfigHO(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            SecurityConfigHO config = result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.securityConfigHO;
            Assert.IsNotNull(config);
            Assert.AreEqual(config.GetOutputs(), description);
        }
    }
}
