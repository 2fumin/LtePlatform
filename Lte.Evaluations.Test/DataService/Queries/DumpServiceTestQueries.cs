using Lte.Parameters.Entities.Basic;
using Shouldly;

namespace Lte.Evaluations.Test.DataService.Queries
{
    public static class DumpServiceTestQueries
    {
        public static void ShouldBe(this ENodeb eNodeb, string name, string address, int townId, int eNodebId,
            string gatewayAddress, string ipAddress)
        {
            eNodeb.Name.ShouldBe(name);
            eNodeb.Address.ShouldBe(address);
            eNodeb.TownId.ShouldBe(townId);
            eNodeb.ENodebId.ShouldBe(eNodebId);
            eNodeb.GatewayIp.AddressString.ShouldBe(gatewayAddress);
            eNodeb.Ip.AddressString.ShouldBe(ipAddress);
        }

        public static void ShouldBe(this CdmaBts bts, string name, string address, int townId, int btsId,
            double longtitute, double lattitute)
        {
            bts.Name.ShouldBe(name);
            bts.Address.ShouldBe(address);
            bts.TownId.ShouldBe(townId);
            bts.BtsId.ShouldBe(btsId);
            bts.Longtitute.ShouldBe(longtitute);
            bts.Lattitute.ShouldBe(lattitute);
        }
    }
}
