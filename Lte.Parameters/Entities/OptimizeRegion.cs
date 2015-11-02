using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities
{
    public class OptimizeRegion : Entity
    {
        public string City { get; set; }

        public string Region { get; set; }

        public string District { get; set; }
    }
}
