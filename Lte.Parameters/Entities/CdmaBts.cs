using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Common.Geo;

namespace Lte.Parameters.Entities
{
    /// <summary>
    /// 定义CDMA基站的数据库对应的ORM对象。
    /// </summary>
    /// <remarks>需要定义与CdmaBtsView之间的映射关系</remarks>
    public class CdmaBts : Entity
    {
        public int ENodebId { get; set; } = -1;

        [MaxLength(50)]
        public string Name { get; set; }

        public int TownId { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string Address { get; set; }

        public int BtsId { get; set; }

        public short BscId { get; set; }
    }
}
