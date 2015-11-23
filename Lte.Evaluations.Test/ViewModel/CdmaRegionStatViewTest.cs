using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Evaluations.Test.ViewModel
{
    [TestFixture]
    public class CdmaRegionStatViewTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            KpiMapperService.MapCdmaRegionStat();
        }

        [TestCase("Region-1", "2015-1-1", 12.1, 2, 3, 4, 5)]
        [TestCase("Region-2", "2015-11-1", 12.7, 2, 0, 4, 5)]
        [TestCase("Region-3", "2015-1-1", 12.1, 2, 3, 9, 0)]
        [TestCase("Region-1", "2015-1-15", -22.1, 7, 0, 4, 0)]
        public void Test_Constructor(string region, string dateString, double erlang, int drop2GNum, int drop2GDem,
            int ecioNum, int ecioDem)
        {
            var stat = new CdmaRegionStat
            {
                Region = region,
                StatDate = DateTime.Parse(dateString),
                ErlangIncludingSwitch = erlang,
                Drop2GNum = drop2GNum,
                Drop2GDem = drop2GDem,
                EcioNum = ecioNum,
                EcioDem = ecioDem
            };
            var view = CdmaRegionStatView.ConstructView(stat);
            Assert.AreEqual(view.Region, region);
            Assert.AreEqual(view.ErlangIncludingSwitch, erlang);
            Assert.AreEqual(view.Drop2GRate, drop2GDem == 0 ? 0 : (double)drop2GNum / drop2GDem);
            Assert.AreEqual(view.Ecio, ecioDem == 0 ? 1 : (double)ecioNum / ecioDem);
        }
    }
}
