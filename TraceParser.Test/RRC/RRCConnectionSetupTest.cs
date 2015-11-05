using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;
using TraceParser.Eutra;
using TraceParser.Outputs;

namespace TraceParser.Test.RRC
{
    [TestFixture]
    public class RRCConnectionSetupTest
    {
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 88 28 00 05 80 ")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 E8 28 00 0A 80 ")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 C8 28 00 07 80 ")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 A8 28 00 08 80 ")]
        public void Test_Decode(string source)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(3), 3);
            RRCConnectionSetup signal = RRCConnectionSetup.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(signal);
        }

        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 88 28 00 05 80 ",
            "SRB ID:1, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
            + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:1, Prioritised bit rate:infinity, "
            + "Bucket size duration:500ms, Logical channel group:0")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 E8 28 00 0A 80 ",
            "SRB ID:1, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
            + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:1, Prioritised bit rate:infinity, "
            + "Bucket size duration:500ms, Logical channel group:0")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 C8 28 00 07 80 ",
            "SRB ID:1, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
            + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:1, Prioritised bit rate:infinity, "
            + "Bucket size duration:500ms, Logical channel group:0")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 A8 28 00 08 80 ",
            "SRB ID:1, RLC config:AM:Uplink:Poll retransmit timer:55ms, Poll pdu:16, Poll byte:infinity, Max retransmit threshold:32, "
            + "Downlink:Reordering timer:35ms, Status prohibit timer:20ms, Logical channel config:Priority:1, Prioritised bit rate:infinity, "
            + "Bucket size duration:500ms, Logical channel group:0")]
        public void Test_SrbToAddModList(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(3), 3);
            RRCConnectionSetup signal = RRCConnectionSetup.PerDecoder.Instance.Decode(stream);
            Assert.AreEqual(signal.rrc_TransactionIdentifier, 1);
            SRB_ToAddMod item =
                signal.criticalExtensions.c1.rrcConnectionSetup_r8.radioResourceConfigDedicated.srb_ToAddModList[0];
            Assert.AreEqual(item.GetOutputs(), description);
        }

        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 88 28 00 05 80 ",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "Time alignment timer dedicate:infinity, PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:6dB")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 E8 28 00 0A 80 ",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "Time alignment timer dedicate:infinity, PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:6dB")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 C8 28 00 07 80 ",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "Time alignment timer dedicate:infinity, PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:6dB")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 A8 28 00 08 80 ",
            "UL SCH config:Max HARQ Transmission:5, Period BSR timer:5 subframes, Retransmission BSR timer:320 subframes, TTI bundling:False, "
            + "Time alignment timer dedicate:infinity, PHR config:Periodic PHR timer:1000 subframes, Prohibit PHR timer:0, Downlink pathloss change:6dB")]
        public void Test_MacMainConfig(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(3), 3);
            RRCConnectionSetup signal = RRCConnectionSetup.PerDecoder.Instance.Decode(stream);
            Assert.AreEqual(signal.rrc_TransactionIdentifier, 1);
            MAC_MainConfig config =
                signal.criticalExtensions.c1.rrcConnectionSetup_r8.radioResourceConfigDedicated.mac_MainConfig
                    .explicitValue;
            Assert.AreEqual(config.GetOutputs(), description);
        }

        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 88 28 00 05 80 ",
            "Downlink config:release, Uplink config:release")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 E8 28 00 0A 80 ",
            "Downlink config:release, Uplink config:release")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 C8 28 00 07 80 ",
            "Downlink config:release, Uplink config:release")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 A8 28 00 08 80 ",
            "Downlink config:release, Uplink config:release")]
        public void Test_SpsConfig(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(3), 3);
            RRCConnectionSetup signal = RRCConnectionSetup.PerDecoder.Instance.Decode(stream);
            Assert.AreEqual(signal.rrc_TransactionIdentifier, 1);
            SPS_Config config =
                signal.criticalExtensions.c1.rrcConnectionSetup_r8.radioResourceConfigDedicated.sps_Config;
            Assert.AreEqual(config.GetOutputs(), description);
        }

        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 88 28 00 05 80 ",
            "Uplink power control dedicated:P0 UE PUSCH:0, Delta MCS enabled:en0, Accumulation enabled:True, P0 UE PUCCH:1, pSRS offset:5, Filter coefficient:8, "
            + "CQI report config:CQI report mode aperiodic:rm30, Nominal PDSCH RS EPRE offset:0, CQI report periodic:CQI PUCCH resource index:0, CQI PMI  config index:68, "
            + "CQI format indicator periodic:wideband CQI, RI config index:, Simultaneous ack nack and CQI:True, "
            + "Antenna info:Transmission mode:TM2, UE transmit antenna selection:release"
            + ", Sounding RS UL config dedicated:release, Scheduling request config:SR PUCCH resource index:0, SR config index:5, DSR transmisstion max:n64"
            + ", PDSCH config dedicated:pA:-3dB, PUCCH config dedicated:Ack/Nack repetition:release, TDD ack/nack feedbackmode:undefined"
            + ", PUSCH config dedicated:Beta offset ack index:8, Beta offset CQI index:7, Beta offset RI index:4, TPC PDCCH config PUCCH:release, TPC PDCCH config PUSCH:release")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 E8 28 00 0A 80 ",
            "Uplink power control dedicated:P0 UE PUSCH:0, Delta MCS enabled:en0, Accumulation enabled:True, P0 UE PUCCH:1, pSRS offset:5, Filter coefficient:8, "
            + "CQI report config:CQI report mode aperiodic:rm30, Nominal PDSCH RS EPRE offset:0, CQI report periodic:CQI PUCCH resource index:0, CQI PMI  config index:71, "
            + "CQI format indicator periodic:wideband CQI, RI config index:, Simultaneous ack nack and CQI:True, "
            + "Antenna info:Transmission mode:TM2, UE transmit antenna selection:release"
            + ", Sounding RS UL config dedicated:release, Scheduling request config:SR PUCCH resource index:0, SR config index:10, DSR transmisstion max:n64"
            + ", PDSCH config dedicated:pA:-3dB, PUCCH config dedicated:Ack/Nack repetition:release, TDD ack/nack feedbackmode:undefined"
            + ", PUSCH config dedicated:Beta offset ack index:8, Beta offset CQI index:7, Beta offset RI index:4, TPC PDCCH config PUCCH:release, TPC PDCCH config PUSCH:release")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 C8 28 00 07 80 ",
            "Uplink power control dedicated:P0 UE PUSCH:0, Delta MCS enabled:en0, Accumulation enabled:True, P0 UE PUCCH:1, pSRS offset:5, Filter coefficient:8, "
            + "CQI report config:CQI report mode aperiodic:rm30, Nominal PDSCH RS EPRE offset:0, CQI report periodic:CQI PUCCH resource index:0, CQI PMI  config index:70, "
            + "CQI format indicator periodic:wideband CQI, RI config index:, Simultaneous ack nack and CQI:True, "
            + "Antenna info:Transmission mode:TM2, UE transmit antenna selection:release"
            + ", Sounding RS UL config dedicated:release, Scheduling request config:SR PUCCH resource index:0, SR config index:7, DSR transmisstion max:n64"
            + ", PDSCH config dedicated:pA:-3dB, PUCCH config dedicated:Ack/Nack repetition:release, TDD ack/nack feedbackmode:undefined"
            + ", PUSCH config dedicated:Beta offset ack index:8, Beta offset CQI index:7, Beta offset RI index:4, TPC PDCCH config PUCCH:release, TPC PDCCH config PUSCH:release")]
        [TestCase("68 13 98 0A 5D CE 21 83 C0 BA 00 7E 13 1F FA 21 1F 0C AA 0D 98 00 08 A8 28 00 08 80 ",
            "Uplink power control dedicated:P0 UE PUSCH:0, Delta MCS enabled:en0, Accumulation enabled:True, P0 UE PUCCH:1, pSRS offset:5, Filter coefficient:8, "
            + "CQI report config:CQI report mode aperiodic:rm30, Nominal PDSCH RS EPRE offset:0, CQI report periodic:CQI PUCCH resource index:0, CQI PMI  config index:69, "
            + "CQI format indicator periodic:wideband CQI, RI config index:, Simultaneous ack nack and CQI:True, "
            + "Antenna info:Transmission mode:TM2, UE transmit antenna selection:release"
            + ", Sounding RS UL config dedicated:release, Scheduling request config:SR PUCCH resource index:0, SR config index:8, DSR transmisstion max:n64"
            + ", PDSCH config dedicated:pA:-3dB, PUCCH config dedicated:Ack/Nack repetition:release, TDD ack/nack feedbackmode:undefined"
            + ", PUSCH config dedicated:Beta offset ack index:8, Beta offset CQI index:7, Beta offset RI index:4, TPC PDCCH config PUCCH:release, TPC PDCCH config PUSCH:release")]
        public void Test_PhysicalConfigDedicated(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(3), 3);
            RRCConnectionSetup signal = RRCConnectionSetup.PerDecoder.Instance.Decode(stream);
            Assert.AreEqual(signal.rrc_TransactionIdentifier, 1);
            PhysicalConfigDedicated config =
                signal.criticalExtensions.c1.rrcConnectionSetup_r8.radioResourceConfigDedicated.physicalConfigDedicated;
            Assert.AreEqual(config.GetOutputs(), description);
        }
    }
}
