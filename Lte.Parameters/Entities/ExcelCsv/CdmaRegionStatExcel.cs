using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class CdmaRegionStatExcel
    {
        [ExcelColumn("地市")]
        public string Region { get; set; }

        [ExcelColumn("日期")]
        public DateTime StatDate { get; set; }

        [ExcelColumn("2G全天话务量含切换")]
        public double ErlangIncludingSwitch { get; set; }

        [ExcelColumn("2G全天话务量不含切换")]
        public double ErlangExcludingSwitch { get; set; }

        [ExcelColumn("掉话分子")]
        public int Drop2GNum { get; set; }

        [ExcelColumn("掉话分母")]
        public int Drop2GDem { get; set; }
        
        [ExcelColumn("呼建分子")]
        public int CallSetupNum { get; set; }

        [ExcelColumn("呼建分母")]
        public int CallSetupDem { get; set; }
        
        [ExcelColumn("EcIo分子")]
        public long EcioNum { get; set; }

        [ExcelColumn("EcIo分母")]
        public long EcioDem { get; set; }
        
        [ExcelColumn("2G利用率分子")]
        public int Utility2GNum { get; set; }

        [ExcelColumn("2G利用率分母")]
        public int Utility2GDem { get; set; }
        
        [ExcelColumn("全天流量MB")]
        public double Flow { get; set; }

        [ExcelColumn("DO全天话务量erl")]
        public double Erlang3G { get; set; }

        [ExcelColumn("掉线分子")]
        public int Drop3GNum { get; set; }

        [ExcelColumn("掉线分母")]
        public int Drop3GDem { get; set; }
        
        [ExcelColumn("连接分子")]
        public int ConnectionNum { get; set; }

        [ExcelColumn("连接分母")]
        public int ConnectionDem { get; set; }
        
        [ExcelColumn("CI分子")]
        public long CiNum { get; set; }

        [ExcelColumn("CI分母")]
        public long CiDem { get; set; }
        
        [ExcelColumn("反向链路繁忙率分子")]
        public int LinkBusyNum { get; set; }

        [ExcelColumn("反向链路繁忙率分母")]
        public int LinkBusyDem { get; set; }
        
        [ExcelColumn("3G切2G流量比分子")]
        public long DownSwitchNum { get; set; }

        [ExcelColumn("3G切2G流量比分母")]
        public int DownSwitchDem { get; set; }
        
        [ExcelColumn("3G利用率分子")]
        public int Utility3GNum { get; set; }

        [ExcelColumn("3G利用率分母_载扇数")]
        public int Utility3GDem { get; set; }
    }
}
