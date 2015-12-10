using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Castle.Core.Test.Junior.DynamicProxy
{
    [TestFixture]
    public class CreateInstanceTest
    {
        class MyClass1
        {
            private readonly int _member1;
            private readonly double _member2;

            public MyClass1(int member1, double member2)
            {
                _member1 = member1;
                _member2 = member2;
            }

            public MyClass1() { }

            public bool AssertValue(int member1, double member2)
            {
                return _member1 == member1 && _member2 == member2;
            }
        }

        [Test]
        public void Test_CreateInstance_MyClass1()
        {
            var resultItem = (MyClass1)Activator.CreateInstance(typeof (MyClass1), 1, 2.1);
            Assert.IsTrue(resultItem.AssertValue(1, 2.1));
        }
    }
}
