using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Castle.Core.Test.Junior.DynamicProxy
{
    [TestFixture]
    public class ModuleScopeTest
    {
        [Test]
        public void TestAdd()
        {
            Assert.AreEqual(1+1, 2);
        }

        [Test]
        public void Test_SimpleConstructor()
        {
            var scope = new ModuleScopeTest();
            Assert.IsNotNull(scope);
        }
    }
}
