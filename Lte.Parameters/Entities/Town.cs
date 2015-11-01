using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities
{
    public class Town : Entity
    {
        [MaxLength(20)]
        public string CityName { get; set; }

        [MaxLength(20)]
        public string DistrictName { get; set; }

        [MaxLength(20)]
        public string TownName { get; set; }
    }
}
