using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Should;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Lte.Domain.Regular;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Evaluations.Test.MapperService
{
    [TestFixture]
    public class MapFromENodebContainerServiceTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            ParametersDumpMapperService.MapFromENodebContainerService();
        }

        [TestCase("abc", "ieowue", 1, 2, "10.17.165.0", "10.17.165.100")]
        [TestCase("arebc", "ieo--wue", 3, 4, "219.128.254.0", "219.128.254.41")]
        public void Test_OneItem(string name, string address, int townId, int eNodebId, string gatewayAddress, string ipAddress)
        {
            var container = new ENodebExcelWithTownIdContainer
            {
                ENodebExcel = new ENodebExcel
                {
                    Name = name,
                    Address = address,
                    ENodebId = eNodebId,
                    Ip = new IpAddress(ipAddress),
                    Gateway = new IpAddress(gatewayAddress)
                },
                TownId = townId
            };
            var item = Mapper.Map<ENodebExcelWithTownIdContainer, ENodebWithTownIdContainer>(container);
  
            item.ENodeb.ENodebId.ShouldEqual(eNodebId);
            item.ENodeb.Name.ShouldEqual(name);
            item.ENodeb.Address.ShouldEqual(address);
            item.TownId.ShouldEqual(townId);
            item.ENodeb.Ip.AddressString.ShouldEqual(ipAddress);
        }

        [TestCase(new [] { "abc"}, new [] { "ieowue"}, new [] { 1}, new [] { 2})]
        [TestCase(new[] { "abc", "ert" }, new[] { "ieowue", "oe90w" }, new[] { 1, 100 }, new[] { 2, 2077 })]
        [TestCase(new[] { "arebc"}, new[] { "ieo--wue"}, new[] { 3}, new[] { 4})]
        public void Test_MultiItems(string[] names, string[] addresses, int[] townIds, int[] eNodebIds)
        {
            var containers = names.Select((t, i) => new ENodebExcelWithTownIdContainer
            {
                ENodebExcel = new ENodebExcel
                {
                    Name = t,
                    Address = addresses[i],
                    ENodebId = eNodebIds[i]
                },
                TownId = townIds[i]
            });

            var items =
                Mapper.Map<IEnumerable<ENodebExcelWithTownIdContainer>, List<ENodebWithTownIdContainer>>(containers);
            items.ForEach(x=> { x.ENodeb.TownId = x.TownId; });
            var results = items.Select(x => x.ENodeb);
            results.Select(x => x.ENodebId).ToArray().ShouldEqual(eNodebIds);
            results.Select(x => x.Name).ToArray().ShouldEqual(names);
            results.Select(x => x.Address).ToArray().ShouldEqual(addresses);
            results.Select(x => x.TownId).ToArray().ShouldEqual(townIds);
        }
    }

}
