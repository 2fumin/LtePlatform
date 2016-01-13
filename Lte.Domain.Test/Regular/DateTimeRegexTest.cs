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
    public class DateTimeRegexTest
    {
        [TestCase("2015-11-4")]
        [TestCase("8888-12-27")]
        [TestCase("1999-12-33")]
        public void Test_CheckDateByString_True(string source)
        {
            Assert.IsTrue(source.CheckDateByString());
        }

        [TestCase("1234-13-7")]
        [TestCase("11121-8-8")]
        public void Test_CheckDateByString_False(string source)
        {
            Assert.IsFalse(source.CheckDateByString());
        }

        [TestCase("a2015-11-4", "2015-11-4")]
        [TestCase("18888-12-274", "8888-12-2")]
        [TestCase("1999-12-33", "1999-12-3")]
        public void Test_GetFirstDateByString(string source, string result)
        {
            Assert.AreEqual(source.GetFirstDateByString(), result);
        }
    }
}
