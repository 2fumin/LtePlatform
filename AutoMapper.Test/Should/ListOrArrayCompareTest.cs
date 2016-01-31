using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Should
{
    [TestFixture]
    public class ListOrArrayCompareTest
    {
        [Test]
        public void Test_TwoArrays_Same()
        {
            var a = new[] {1, 2, 3};
            var b = a;
            a.ShouldBe(b);
        }

        [Test]
        public void Test_TwoArray_ItemsAreSame()
        {
            var a = new[] { 1, 2, 3 };
            var b = new[] { 1, 2, 3 };
            a.ShouldBe(b);
        }

        [Test]
        public void Test_TwoArray_ItemsAreNotSame()
        {
            var a = new[] { 1, 2, 3 };
            var b = new[] { 1, 4, 3 };
            a.ShouldNotBe(b);
        }

        [Test]
        public void Test_TwoEmptyArrays()
        {
            var a = new int[] {};
            var b = new int[] { };
            a.ShouldBe(b);
        }
    }
}
