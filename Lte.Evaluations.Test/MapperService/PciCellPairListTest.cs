using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.MapperSerive;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.Test.MapperService
{
    [TestFixture]
    public class PciCellPairListTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            AutoMapperHelper.CreateMap(typeof (PciCellPair));
        }

        [Test]
        public void Test_MapDistinct()
        {
            var originalList = new List<PciCell>
            {
                new PciCell {ENodebId = 1, SectorId = 2, Pci = 3},
                new PciCell {ENodebId = 1, SectorId = 2, Pci = 4},
                new PciCell {ENodebId = 2, SectorId = 2, Pci = 1},
                new PciCell {ENodebId = 3, SectorId = 2, Pci = 3},
                new PciCell {ENodebId = 1, SectorId = 2, Pci = 4},
                new PciCell {ENodebId = 3, SectorId = 2, Pci = 3},
                new PciCell {ENodebId = 1, SectorId = 4, Pci = 3},
                new PciCell {ENodebId = 2, SectorId = 4, Pci = 1},
            };
            var destList=originalList.MapTo<IEnumerable<PciCellPair>>().Distinct(new PciCellPairComparer()).ToList();
            destList.Count.ShouldBe(4);
            destList[0].ENodebId.ShouldBe(1);
            destList[0].Pci.ShouldBe((short)3);
            destList[1].Pci.ShouldBe((short)4);
            destList[2].ENodebId.ShouldBe(2);
            destList[3].ENodebId.ShouldBe(3);
        }
    }
}
