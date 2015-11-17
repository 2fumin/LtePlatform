using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels;
using NUnit.Framework;

namespace Lte.Evaluations.Test.TestService
{
    public static class PreciseViewTestService
    {
        public static void AssertEqual(DistrictPreciseView left, DistrictPreciseView right)
        {
            Assert.AreEqual(left.City, right.City);
            Assert.AreEqual(left.District, right.District);
            Assert.AreEqual(left.FirstNeighbors, right.FirstNeighbors);
            Assert.AreEqual(left.SecondNeighbors, right.SecondNeighbors);
            Assert.AreEqual(left.TotalMrs, right.TotalMrs);
        }
    }
}
