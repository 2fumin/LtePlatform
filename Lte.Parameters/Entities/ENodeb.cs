using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class ENodeb : Entity, IGeoPoint<double>
    {
        public int ENodebId { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        public int TownId { get; set; }

        public double Longtitute { get; set; }
        
        public double Lattitute { get; set; }
        
        public string Factory { get; set; }

        public bool IsFdd { get; set; }
        
        public string Address { get; set; }

        public int Gateway { get; set; }

        public byte SubIp { get; set; }

        public IpAddress GatewayIp
        {
            get
            {
                return new IpAddress
                {
                    AddressValue = Gateway
                };
            }
        }

        public IpAddress Ip
        {
            get
            {
                IpAddress ip = new IpAddress { AddressValue = Gateway, IpByte4 = SubIp };
                return ip;
            }
        }
        
        public string PlanNum { get; set; }
        
        public DateTime OpenDate { get; set; }
    }
}
