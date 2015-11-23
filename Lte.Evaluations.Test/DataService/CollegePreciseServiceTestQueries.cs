﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor.Diagnostics.DebuggerViews;
using Lte.Evaluations.ViewModels;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    public static class CollegePreciseServiceTestQueries
    {
        public static void AssertBasicParameters(this CellPreciseKpiView view, int eNodebId, byte sectorId,
            double rsPower, double height, string indoor, double downTilt)
        {
            Assert.AreEqual(view.RsPower, rsPower);
            Assert.AreEqual(view.ENodebId, eNodebId);
            Assert.AreEqual(view.SectorId, sectorId);
            Assert.AreEqual(view.Height, height);
            Assert.AreEqual(view.Indoor, indoor);
            Assert.AreEqual(view.DownTilt, downTilt, 1E-6);
        }
    }
}
