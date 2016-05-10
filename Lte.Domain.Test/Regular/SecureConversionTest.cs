using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class SecureConversionTest
    {
        [TestCase("1", 1)]
        [TestCase("123,456", 123456)]
        public void Test_ToInt(string str, int result)
        {
            var actual = str.Replace(",","").ConvertToInt(0);
            Assert.AreEqual(actual, result);
        }

        [TestCase("1.23", 1.23)]
        [TestCase("123,456.789", 123456.789)]
        public void Test_ToDouble(string str, double result)
        {
            var actual = str.Replace(",", "").ConvertToDouble(0);
            Assert.AreEqual(actual, result);
        }
    }
}
