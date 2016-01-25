using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

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

        public static void ShouldEqual(this CdmaBts bts, string name, string address, int townId, int btsId,
            double longtitute, double lattitute)
        {
            bts.Name.ShouldEqual(name);
            bts.Address.ShouldEqual(address);
            bts.TownId.ShouldEqual(townId);
            bts.BtsId.ShouldEqual(btsId);
            bts.Longtitute.ShouldEqual(longtitute);
            bts.Lattitute.ShouldEqual(lattitute);
        }
    }
}
