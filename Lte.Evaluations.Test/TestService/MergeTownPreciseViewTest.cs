using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using NUnit.Framework;

namespace Lte.Evaluations.Test.TestService
{
    [TestFixture]
    public class MergeTownPreciseViewTest
    {
        [TestCase("Foshan", "Chancheng", new[] {"town-1", "town-2"}, 
            new[] { 1, 2 }, new[] { 3, 4 }, new[] { 6, 2 })]
        [TestCase("Foshan", "Nanhai", new[] { "town-1", "town-2", "town-1" }, 
            new[] { 1, 2, 4 }, new[] { 3, 4, 2 }, new[] { 6, 2, 5 })]
        public void TestOneDistrict(string city, string district, string[] towns, int[] totalMrs, int[] firstNeighbors,
            int[] secondNeighbors)
        {
            var townViews = towns.Select((t, i) => new TownPreciseView
            {
                City = city,
                District = district,
                Town = t,
                TotalMrs = totalMrs[i],
                FirstNeighbors = firstNeighbors[i],
                SecondNeighbors = secondNeighbors[i]
            }).ToList();
            var districtViews = PreciseRegionStatService.Merge(townViews);
            Assert.AreEqual(districtViews.Count(), 1);
            PreciseViewTestService.AssertEqual(districtViews.ElementAt(0), new DistrictPreciseView
            {
                City = city,
                District = district,
                TotalMrs = totalMrs.Sum(),
                FirstNeighbors = firstNeighbors.Sum(),
                SecondNeighbors = secondNeighbors.Sum()
            });
        }

        [TestCase(1, "Foshan", new[] { "Chancheng", "Nanhai" }, new[] { "town-1", "town-2" },
            new[] { 1, 2 }, new[] { 3, 4 }, new[] { 6, 2 }, 2,
            new[] { 1, 2 }, new[] { 3, 4 }, new[] { 6, 2 })]
        [TestCase(2, "Foshan", new[] { "Nanhai", "Shunde", "Nanhai" }, new[] { "town-1", "town-2", "town-1" },
            new[] { 1, 2, 4 }, new[] { 3, 4, 2 }, new[] { 6, 2, 5 }, 2,
            new[] { 5, 2 }, new[] { 5, 4 }, new[] { 11, 2 })]
        public void TestMultiDistricts(int testNo, string city, string[] districts, string[] towns,
            int[] totalMrs, int[] firstNeighbors, int[] secondNeighbors, int districtCounts,
            int[] districtMrs, int[] districtFirsts, int[] districtSeconds)
        {
            var townViews = towns.Select((t, i) => new TownPreciseView
            {
                City = city,
                District = districts[i],
                Town = t,
                TotalMrs = totalMrs[i],
                FirstNeighbors = firstNeighbors[i],
                SecondNeighbors = secondNeighbors[i]
            }).ToList();
            var districtViews = PreciseRegionStatService.Merge(townViews);
            Assert.AreEqual(districtViews.Count(), districtCounts);
            for (var i=0; i<districtCounts; i++)
            {
                PreciseViewTestService.AssertEqual(districtViews.ElementAt(i), new DistrictPreciseView
                {
                    City = city,
                    District = districts[i],
                    TotalMrs = districtMrs[i],
                    FirstNeighbors = districtFirsts[i],
                    SecondNeighbors = districtSeconds[i]
                });
            }
        }
    }
}
