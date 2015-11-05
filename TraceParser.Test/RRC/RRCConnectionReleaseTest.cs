using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;
using TraceParser.Eutra;

namespace TraceParser.Test.RRC
{
    [TestFixture]
    public  class RRCConnectionReleaseTest
    {
        [TestCase("28 22 80 02 50", true)]
        [TestCase("28 02 ", false)]
        public void Test_Decode(string source, bool redirect)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 5);
            RRCConnectionRelease signal = RRCConnectionRelease.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(signal);
            Assert.AreEqual(signal.rrc_TransactionIdentifier, 0);
            RRCConnectionRelease_r8_IEs item = signal.criticalExtensions.c1.rrcConnectionRelease_r8;
            Assert.IsNotNull(item);
            Assert.AreEqual(item.releaseCause, ReleaseCause.other);
            if (redirect)
            {
                Assert.IsNotNull(item.redirectedCarrierInfo);
                Assert.AreEqual(item.redirectedCarrierInfo.eutra, 0);
                Assert.AreEqual(item.redirectedCarrierInfo.utra_FDD, 0);
                Assert.AreEqual(item.redirectedCarrierInfo.utra_TDD, 0);
                Assert.IsNotNull(item.redirectedCarrierInfo.cdma2000_HRPD);
                Assert.AreEqual(item.redirectedCarrierInfo.cdma2000_HRPD.bandClass, BandclassCDMA2000.bc0);
                Assert.AreEqual(item.redirectedCarrierInfo.cdma2000_HRPD.arfcn, 37);
            }
            else
            {
                Assert.IsNull(item.redirectedCarrierInfo);
            }
        }
    }
}
