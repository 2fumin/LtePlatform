using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels;
using NUnit.Framework;

namespace Lte.Evaluations.Test.TestService
{
    public static class CellViewTestService
    {
        public static void AssertEqual(this CellView view, string cellName, double baiduLontitute,
            double baiduLattitute, double azimuth, string indoor)
        {
            Assert.AreEqual(view.ENodebName + "-" + view.SectorId, cellName);
            Assert.AreEqual(view.BaiduLongtitute, baiduLontitute);
            Assert.AreEqual(view.BaiduLattitute, baiduLattitute);
            Assert.AreEqual(view.Azimuth, azimuth);
            Assert.AreEqual(view.Indoor, indoor);
        }

        public static void AssertEqual(this CdmaCellView view, string cellName, double baiduLontitute,
            double baiduLattitute, double azimuth, string indoor)
        {
            Assert.AreEqual(view.BtsName + "-" + view.SectorId, cellName);
            Assert.AreEqual(view.BaiduLongtitute, baiduLontitute);
            Assert.AreEqual(view.BaiduLattitute, baiduLattitute);
            Assert.AreEqual(view.Azimuth, azimuth);
            Assert.AreEqual(view.Indoor, indoor);
        }

        public static void AssertEqual(this SectorView view, string cellName, double baiduLontitute,
            double baiduLattitute, double azimuth, string indoor)
        {
            Assert.AreEqual(view.CellName, cellName);
            Assert.AreEqual(view.BaiduLongtitute, baiduLontitute);
            Assert.AreEqual(view.BaiduLattitute, baiduLattitute);
            Assert.AreEqual(view.Azimuth, azimuth);
            Assert.AreEqual(view.Indoor, indoor);
        }
    }
}
