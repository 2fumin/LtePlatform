using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.Test.DataService.Queries
{
    public static class DumpServiceTestQueries
    {
        public static void ShouldEqual(this ENodeb eNodeb, string name, string address, int townId, int eNodebId,
            string gatewayAddress, string ipAddress)
        {
            eNodeb.Name.ShouldEqual(name);
            eNodeb.Address.ShouldEqual(address);
            eNodeb.TownId.ShouldEqual(townId);
            eNodeb.ENodebId.ShouldEqual(eNodebId);
            eNodeb.GatewayIp.AddressString.ShouldEqual(gatewayAddress);
            eNodeb.Ip.AddressString.ShouldEqual(ipAddress);
        }
    }
}
