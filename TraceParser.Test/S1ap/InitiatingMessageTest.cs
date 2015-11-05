using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;
using TraceParser.S1ap;

namespace TraceParser.Test.S1ap
{
    [TestFixture]
    public class InitiatingMessageTest
    {
        [TestCase("00 0C 40 39 00 00 06 00 08 00 04 80 02 46 76 00 1A 00 05 04 C7 A5 17 71 00 43 00 06 00 64 F0 11 7A 03 00 64 40 08 00 64 F0 11 7A 14 B3 10 00 86 40 01 20 00 60 00 06 00 80 FC 0F 33 EE",
            12)]
        [TestCase("00 0C 40 39 00 00 06 00 08 00 04 80 0A 67 DA 00 1A 00 05 04 C7 74 6D 47 00 43 00 06 00 64 F0 11 7A 03 00 64 40 08 00 64 F0 11 7A 14 B3 20 00 86 40 01 20 00 60 00 06 00 40 D1 02 4C 5D",
            12)]
        [TestCase("00 0C 40 56 00 00 05 00 08 00 04 80 02 55 8D 00 1A 00 2C 2B 17 BA E4 12 32 12 07 41 41 0B F6 64 F0 11 44 01 01 CF 1C CC 91 02 F0 70 00 05 02 0F D0 32 D1 52 64 F0 11 7A 03 5C 0A 00 5D 01 00 00 43 00 06 00 64 F0 11 7A 03 00 64 40 08 00 64 F0 11 7A 14 B3 10 00 86 40 01 30",
            12)]
        public void Test_Decode(string source, int procedureCode)
        {
            BitArrayInputStream stream = source.GetInputStream();
            S1AP_PDU pdu = S1AP_PDU.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(pdu.initiatingMessage);
            Assert.AreEqual(pdu.initiatingMessage.procedureCode, procedureCode);
        }
    }
}
