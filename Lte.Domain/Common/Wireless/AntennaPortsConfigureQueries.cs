using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common.Wireless
{
    public static class AntennaPortsConfigureQueries
    {
        public static AntennaPortsConfigure GetAntennaPortsConfig(this string description)
        {
            switch (description.ToUpper().Trim())
            {
                case "2T2R":
                    return AntennaPortsConfigure.Antenna2T2R;
                case "1T1R":
                    return AntennaPortsConfigure.Antenna1T1R;
                case "2T8R":
                    return AntennaPortsConfigure.Antenna2T8R;
                case "4T4R":
                    return AntennaPortsConfigure.Antenna4T4R;
                default:
                    return AntennaPortsConfigure.Antenna2T4R;
            }
        }
    }
}
