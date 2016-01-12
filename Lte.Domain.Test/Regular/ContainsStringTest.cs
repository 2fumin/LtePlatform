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
    public class ContainsStringTest
    {
        [TestCase("123", "123")]
        [TestCase("123x", "123")]
        [TestCase("12.3x", "12")]
        [TestCase("x123", "123")]
        [TestCase("12x3456", "12")]
        [TestCase("abcd123x456", "123")]
        public void Test_GetFirstNumber(string source, string number)
        {
            Assert.AreEqual(TouTouRegex.GetFirstNumberByString(source), number);
        }

        [TestCase("12", "12")]
        [TestCase("a456", "456")]
        [TestCase("12x34a56", "56")]
        public void Test_GetLastNumber(string source, string number)
        {
            Assert.AreEqual(TouTouRegex.GetLastNumberByString(source), number);
        }
    }
}
