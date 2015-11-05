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
    public class RRCConnectionSetupCompleteTest
    {
        [TestCase("22 00 09 8F 4A 2E E2", 1)]
        [TestCase("22 00 09 8E E8 DA 8E", 1)]
        [TestCase("22 20 44 01 01 2B 17 BA E4 12 32 12 07 41 41 0B F6 64 F0 11 44 01 01 CF 1C CC 91 02 F0 70 00 05 02 0F D0 32 D1 52 64 F0 11 7A 03 5C 0A 00 5D 01 00", 1)]
        [TestCase("22 00 09 8E 67 53 CC ", 1)]
        public void Test_Decode(string source, int transactionId)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionSetupComplete signal = RRCConnectionSetupComplete.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(signal);
            Assert.AreEqual(signal.rrc_TransactionIdentifier, transactionId);
            RRCConnectionSetupComplete_r8_IEs item = signal.criticalExtensions.c1.rrcConnectionSetupComplete_r8;
            Assert.IsNotNull(item);
        }

        [TestCase("22 00 09 8F 4A 2E E2",
            "Selected PLMN ID:1, Dedicated info NAS:C7A51771'H")]
        [TestCase("22 00 09 8E E8 DA 8E",
            "Selected PLMN ID:1, Dedicated info NAS:C7746D47'H")]
        [TestCase("22 00 09 8E 67 53 CC ",
            "Selected PLMN ID:1, Dedicated info NAS:C733A9E6'H")]
        [TestCase("22 20 44 01 01 2B 17 BA E4 12 32 12 07 41 41 0B F6 64 F0 11 44 01 01 CF 1C CC 91 02 F0 70 00 05 02 0F D0 32 D1 52 64 F0 11 7A 03 5C 0A 00 5D 01 00",
            "Selected PLMN ID:1, Dedicated info NAS:17BAE41232120741410BF664F011440101CF1CCC9102F0700005020FD032D15264F0117A035C0A005D0100'H"
            + ", Registered MME:MMEGI:0100010000000001'B, MME:00000001'B")]
        public void Test_CentralPart(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 4);
            RRCConnectionSetupComplete signal = RRCConnectionSetupComplete.PerDecoder.Instance.Decode(stream);
            RRCConnectionSetupComplete_r8_IEs item = signal.criticalExtensions.c1.rrcConnectionSetupComplete_r8;
            Assert.AreEqual(item.GetOutputs(), description);
        }
    }
}
