using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.CellTrace
{
    [Row(InterColumnSplitter = ',', IntraColumnSplitter = '=')]
    public class ZteTraceRecord
    {
        [Column(Name = "Sequence")]
        public int Sequence { get; set; }

        [Column(Name = "Time", DateTimeFormat = "yyyy-MM-dd HH:mm:ss:fff")]
        public DateTime Time { get; set; }

        [Column(Name = "MsgType")]
        public string MsgType { get; set; }

        [Column(Name = "MsgName")]
        public string MsgName { get; set; }

        [Column(Name = "Direction")]
        public string Direction { get; set; }

        [Column(Name = "eNodeBId")]
        public int ENodebId { get; set; }

        [Column(Name = "Cell")]
        public byte CellId { get; set; }

        [Column(Name = "GID")]
        public int Gid { get; set; }

        [Column(Name = "MsgId")]
        public int MsgId { get; set; }

        [Column(Name = "DataId")]
        public byte DataId { get; set; }

        [Column(Name = "MsgCode")]
        public string MsgCode { get; set; }
    }
}
