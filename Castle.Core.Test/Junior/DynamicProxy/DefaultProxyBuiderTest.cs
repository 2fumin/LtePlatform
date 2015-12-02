using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Castle.Core.Test.Junior.DynamicProxy
{
    [TestFixture]
    public class DefaultProxyBuiderTest
    {
        [Test]
        public void Test_SimpleConstructor()
        {
            var builder = new DefaultProxyBuiderTest();
            Assert.IsNotNull(builder);
        }
    }
}
