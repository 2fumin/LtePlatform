using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using Castle.DynamicProxy;
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
            var scope = new ModuleScope();
            Assert.IsNotNull(scope);
        }

        [Test]
        public void Test_GetKeyPair()
        {
            var keyPair = ModuleScope.GetKeyPair();
            keyPair.Length.ShouldEqual(0);
        }
    }
}
