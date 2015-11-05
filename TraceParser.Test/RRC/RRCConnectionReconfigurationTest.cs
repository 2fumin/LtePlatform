using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;
using TraceParser.Eutra;

namespace TraceParser.Test.RRC
{
    [TestFixture]
    public class RRCConnectionReconfigurationTest
    {
        [TestCase("22 02 35 38 11 FB 9C 03 27 60 3E A0 6F 01 D8 75 14 1D 0B C5 BB A4 82 A4 28 08 D9 C0 00 5E 28 40", 1L,
            2L, 1, new [] {5L}, new []{1L}, 
            true,true)]
        [TestCase("22 02 34 38 11 FB 9C 03 27 60 BE A0 6F 01 D8 75 14 1D 0B DF 60 B7 80 EC 3A 8A 2E 85 E2 DD D2 40 72 14", 1L,
            2L, 2, new []{5L, 6L}, new []{1L, 2L},
            true, false)]
        [TestCase("22 02 34 38 11 FB 9C 03 27 60 3E A0 6F 01 D8 75 14 1D 0B C5 BB A4 80 E4 28", 1, 2,
            1, new [] {5L}, new []{1L},
            true, false)]
        public void Test_RadioResource_Dedicated(string source, long transId, long srbId, 
            int drbs, long[] epsId, long[] drbId,
            bool macConfigExist, bool phyConfigExist)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.rrc_TransactionIdentifier, transId);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            RadioResourceConfigDedicated mainPart =
                result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.radioResourceConfigDedicated;
            Assert.IsNotNull(mainPart);
            Assert.IsNotNull(mainPart.srb_ToAddModList);
            Assert.AreEqual(mainPart.srb_ToAddModList.Count, 1);
            SRB_ToAddMod srb = mainPart.srb_ToAddModList[0];
            Assert.AreEqual(srb.srb_Identity, srbId);
            Assert.IsNotNull(mainPart.drb_ToAddModList);
            Assert.AreEqual(mainPart.drb_ToAddModList.Count, drbs);
            for (int i = 0; i < drbs; i++)
            {
                DRB_ToAddMod drb = mainPart.drb_ToAddModList[i];
                Assert.AreEqual(drb.eps_BearerIdentity, epsId[i]);
                Assert.AreEqual(drb.drb_Identity, drbId[i]);
            }
            if (macConfigExist)
            {
                Assert.IsNotNull(mainPart.mac_MainConfig);
            }
            else
            {
                Assert.IsNull(mainPart.mac_MainConfig);
            }
            if (phyConfigExist)
            {
                Assert.IsNotNull(mainPart.physicalConfigDedicated);
            }
            else
            {
                Assert.IsNull(mainPart.physicalConfigDedicated);
            }
        }

        [TestCase("26 10 15 40 42 10 00 32 52 F1 04 00 09 70 BC 06 06 46 2B 18 F0 98 26 38 8D 02", 3,
            2, 1, 2)]
        [TestCase("24 10 15 A8 00 14 03 90 C2 F0 02 56 F2 80 14 01 40 C7 84 11 52 5A 9F 88 01 18 AD 04 06 04 7C 56 82 04 02 20 2C 41 72 93 5E 14 00 00 40 11 00 43 00 C8 02 14 05 46 63 00 00",
            2, 1, 6, 6)]
        public void Test_MeasureConfige(string source, long transId, 
            int measObjects, int reportConfigs, int measIds)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.rrc_TransactionIdentifier, transId);
            Assert.IsNotNull(result.criticalExtensions);
            Assert.IsNotNull(result.criticalExtensions.c1);
            Assert.IsNull(result.criticalExtensions.criticalExtensionsFuture);
            Assert.IsNotNull(result.criticalExtensions.c1.rrcConnectionReconfiguration_r8);
            MeasConfig config = result.criticalExtensions.c1.rrcConnectionReconfiguration_r8.measConfig;
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.measObjectToAddModList);
            Assert.AreEqual(config.measObjectToAddModList.Count, measObjects);
            Assert.IsNotNull(config.reportConfigToAddModList);
            Assert.AreEqual(config.reportConfigToAddModList.Count, reportConfigs);
            Assert.IsNotNull(config.measIdToAddModList);
            Assert.AreEqual(config.measIdToAddModList.Count, measIds);
        }

        [TestCase("26 02 37 38 14 BB 9C 43 27 00 3E A2 68 02 97 73 88 1B 8C CF A0 06 EE 92 18 71 FC 16 04 AC B2 A8 D9 C0 00 8C A1 2A 18")]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 00 81 23 04 21 00 89 4F C6 09 29 B0 48 06 00 00 20 08 80 21 80 68 23 00")]
        [TestCase("20 1B 28 A0 00 30 04 43 41 19 F8 58 03 22 35 A5 54 18 F0 A1 FE 71 63 69 7F 98 A0 B4 5A 96 81 90 CC 80 40 90 1D DB 4D 45 D5 A0 37 B0 14 BB 9C 43 07 83 81 4B B9 C4 32 70 01 A2 20 14 BB 9C 46 E3 33 E8 01 BB A4 CE 9C 7F 05 80 3F 67 00 01 42 84 94 34 12 03 C2 04 00")]
        [TestCase("26 10 15 20 00 00 00 32 52 30 02 84 36 52 00 81 23 04 21 00 89 4F C6 09 29 B0 48 06 00 00 20 08 80 21 80 71 80")]
        [TestCase("26 10 15 A0 00 00 03 90 C2 40 02 84 38 52 00 81 0A 04 21 00 89 4F C6 C8 5E 07 01 40 01 80 20 24 A6 C1 20 20 00 00 80 22 00 86 01 90 04 48 8C")]
        public void Test_Zte(string source)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
        }

        [TestCase("26 10 15 a0 00 00 00 32 52 30 02 84 36 51 70 81 23 04 21 00 89 4f c6 c8 6e 06 01 40 18 00 00 80 22 00 86 01 85 18")]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 06 01 40 18 00 00 80 22 00 86 01 85 18")]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 07 01 40 01 80 18 00 00 80 22 00 86 01 A4 46")]
        [TestCase("26 10 15 20 00 00 03 90 c2 40 02 84 36 51 70 81 23 04 21 00 89 4f c6 c8 6e 06 01 40 20 24 a6 c1 20 20 00 00 80 22 00 86 01 90 04 8c")]
        [TestCase("26 10 15 A0 00 00 00 32 52 30 02 84 36 52 10 81 28 04 21 00 89 4F C6 C8 6E 07 01 40 01 80 18 00 00 80 22 00 86 01 85 18")]
        [TestCase("20 1b 28 a0 00 30 04 43 41 19 e4 08 03 22 35 a5 54 19 28 f9 fe 71 63 69 7f 91 e0 a4 59 9a 81 90 cc a0 40 48 1d db 4d 45 d5 a0 37 b0 14 bb 9c 43 07 83 81 4b b9 c4 32 70 01 a2 20 14 bb 9c 47 43 33 e8 00 fc 16 20 f5 b3 80 00 94 a1 4a 1a 04 83 a1 02 00")]
        [TestCase("20 12 15 60 42 00 03 90 c2 26 14 00 4a 60 30 83 6a 10 70 63 42 06 8b e8 30 30 61 02 20 2c 42 02 02 84 36 51 72 c4 42 60 58 90 18 01 40 21 08 04 40 90 a2 29 80 6b 22 30 53 e8 00 fc 10 11 32")]
        public void Test_ZteCellTrace(string source)
        {
            BitArrayInputStream stream = source.ToUpper().GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionReconfiguration result = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(result);
        }

    }
}
