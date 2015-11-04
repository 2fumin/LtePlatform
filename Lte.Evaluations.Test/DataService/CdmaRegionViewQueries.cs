﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    public static class CdmaRegionViewQueries
    {
        public static void AssertRegionAndErlang2G(this CdmaRegionStatView view, string region, double erlang)
        {
            Assert.AreEqual(view.Region, region);
            Assert.AreEqual(view.ErlangIncludingSwitch, erlang);
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
