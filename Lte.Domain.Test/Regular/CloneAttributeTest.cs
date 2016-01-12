using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class CloneAttributeTest
    {
        class A
        {
            public int M1 { get; set; }

            public int M2 { get; set; }
        }

        class B : IDisposable
        {
            public int M1 { get; set; }

            public int M2 { get; set; }

            public void Dispose()
            { }
        }

        class A1
        {
            public int M1 { get; set; }

            [CloneProtection]
            public int M2 { get; set; }
        }

        [TestFixtureSetUp]
        public void SetupMapping()
        {
            Mapper.CreateMap<A, A>();
            Mapper.CreateMap<A, B>();
        }

        [Test]
        public void TestCloneSelf()
        {
            var a = new A { M1 = 11, M2 = 22 };
            var a2 = Mapper.Map<A>(a);
            Assert.AreEqual(a2.M1, 11);
            Assert.AreEqual(a2.M2, 22);
        }

        [Test]
        public void TestClone_InterClass_NoProtection()
        {
            var a = new A { M1 = 11, M2 = 22 };
            var b = Mapper.Map<A, B>(a);
            Assert.AreEqual(b.M1, 11);
            Assert.AreEqual(b.M2, 22);

            a.CloneProperties(b, true);
            Assert.AreEqual(b.M1, 11);
            Assert.AreEqual(b.M2, 22);
        }

        [Test]
        public void TestClone_InterClass_Protection()
        {
            var a = new A1 { M1 = 11, M2 = 22 };

            using (var b = new B { M1 = 33, M2 = 44 })
            {
                a.CloneProperties(b);
                Assert.AreEqual(b.M1, 11);
                Assert.AreEqual(b.M2, 22);
            }

            using (var b = new B { M1 = 33, M2 = 44 })
            {
                a.CloneProperties(b, true);
                Assert.AreEqual(b.M1, 11);
                Assert.AreEqual(b.M2, 44);
            }
        }
    }
}
