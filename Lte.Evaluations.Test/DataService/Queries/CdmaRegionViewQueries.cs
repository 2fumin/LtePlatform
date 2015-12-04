using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.ViewModels;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.Queries
{
    public static class CdmaRegionViewQueries
    {
        public static void AssertRegionAndErlang2G(this CdmaRegionStatView view, string region, double erlang)
        {
            Assert.AreEqual(view.Region, region);
            Assert.AreEqual(view.ErlangIncludingSwitch, erlang);
        }

        public static void AssertRegionAndErlang2G(this IEnumerable<CdmaRegionStatView> views, string[] regions, 
            double[] erlangs, int[] matchedIndices, string city)
        {
            double sum = 0;
            for (var i = 0; i < matchedIndices.Length; i++)
            {
                AssertRegionAndErlang2G(views.ElementAt(i), regions[matchedIndices[i]], erlangs[matchedIndices[i]]);
                sum += erlangs[matchedIndices[i]];
            }
            AssertRegionAndErlang2G(views.ElementAt(matchedIndices.Length), city, sum);
        }

        public static void AssertErlang2G(this CdmaRegionStatView view, double erlang)
        {
            Assert.AreEqual(view.ErlangIncludingSwitch, erlang);
        }

        public static void AssertRegionAndDropRate(this CdmaRegionStatView view, string region, double dropRate)
        {
            Assert.AreEqual(view.Region, region);
            Assert.AreEqual(view.Drop2GRate, dropRate);
        }

    }
}
