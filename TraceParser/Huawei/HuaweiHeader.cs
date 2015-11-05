using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceParser.Huawei
{
    public class HuaweiHeader
    {
        public uint file_flag_ui4 { get; set; }

        public uint fno_ui4 { get; set; }

        public ushort fver_ui2 { get; set; }

        public sbyte ndep_ui1 { get; set; }

        public byte ntyp_i1 { get; set; }

        public string nver_s40 { get; set; }

        public ushort ttyp_ui2 { get; set; }
    }
}
