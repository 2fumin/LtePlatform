using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class CdmaRegionStat : Entity
    {
        public string Region { get; set; }
        
        public DateTime StatDate { get; set; }
        
        public double ErlangIncludingSwitch { get; set; }
        
        public double ErlangExcludingSwitch { get; set; }
        
        public int Drop2GNum { get; set; }
        
        public int Drop2GDem { get; set; }
        
        public double Drop2GRate => Drop2GDem == 0 ? 0 : (double)Drop2GNum / Drop2GDem;

        public int CallSetupNum { get; set; }
        
        public int CallSetupDem { get; set; }
        
        public double CallSetupRate => CallSetupDem ==0 ? 1: (double)CallSetupNum / CallSetupDem;

        public long EcioNum { get; set; }
        
        public long EcioDem { get; set; }
        
        public double Ecio => EcioDem == 0 ? 1 : (double)EcioNum / EcioDem;

        public int Utility2GNum { get; set; }
        
        public int Utility2GDem { get; set; }

        public double Utility2GRate => Utility2GDem == 0 ? 0 : (double)Utility2GNum / Utility2GDem;

        public double Flow { get; set; }
        
        public double Erlang3G { get; set; }
        
        public int Drop3GNum { get; set; }
        
        public int Drop3GDem { get; set; }
        
        public double Drop3GRate => Drop3GDem == 0 ? 0 : (double)Drop3GNum / Drop3GDem;

        public int ConnectionNum { get; set; }
        
        public int ConnectionDem { get; set; }
        
        public double ConnectionRate => ConnectionDem == 0 ? 1 : (double)ConnectionNum / ConnectionDem;

        public long CiNum { get; set; }
        
        public long CiDem { get; set; }
        
        public double Ci => CiDem == 0 ? 1 : (double)CiNum / CiDem;

        public int LinkBusyNum { get; set; }
        
        public int LinkBusyDem { get; set; }

        public double LinkBusyRate => LinkBusyDem == 0 ? 0 : (double)LinkBusyNum / LinkBusyDem;

        public long DownSwitchNum { get; set; }
        
        public int DownSwitchDem { get; set; }
        
        public double DownSwitchRate => DownSwitchDem == 0 ? 100 : (double)DownSwitchNum / DownSwitchDem;

        public int Utility3GNum { get; set; }
        
        public int Utility3GDem { get; set; }

        public double Utility3GRate => Utility3GDem == 0 ? 0 : (double)Utility3GNum / Utility3GDem;

        public CdmaRegionStat() { }

        public CdmaRegionStat(CdmaRegionStatExcel item)
        {
            item.CloneProperties(this);
        }
    }
}
