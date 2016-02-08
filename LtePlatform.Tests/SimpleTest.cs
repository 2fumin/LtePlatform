using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LtePlatform.Tests
{
    [TestFixture]
    public class SimpleTest
    {
        [Test]
        public void Test_1()
        {
            Assert.IsTrue("20160201".StartsWith("201602"));
        }
    }
}
