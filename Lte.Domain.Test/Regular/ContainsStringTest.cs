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

        [TestCase("1234", 1)]
        [TestCase("12.34", 2)]
        [TestCase("12x34y55", 3)]
        public void Test_GetAllNumber(string source, int count)
        {
            var results = TouTouRegex.GetAllNumberByString(source);
            Assert.AreEqual(results.Count, count);
        }

        [TestCase("12345")]
        [TestCase("-1.78")]
        [TestCase("Abcd88x")]
        [TestCase("#*wqio90")]
        public void Test_CheckNumber_True(string source)
        {
            Assert.IsTrue(TouTouRegex.CheckNumberByString(source));
        }

        [TestCase("Abb")]
        [TestCase("-%^&^*")]
        [TestCase("^%UYnk")]
        public void Test_CheckNumber_False(string source)
        {
            Assert.IsFalse(TouTouRegex.CheckNumberByString(source));
        }

        [TestCase("123", 3)]
        [TestCase("493745723", 9)]
        public void Test_CheckLength(string source, int length)
        {
            Assert.IsTrue(TouTouRegex.CheckLengthByString(source, length));
        }

        [TestCase("maaat", "m", "t", "maaa")]
        public void Test_Substring(string source, string startStr, string endStr, string result)
        {
            Assert.AreEqual(TouTouRegex.Substring(source, startStr, endStr), result);
        }
    }
}
