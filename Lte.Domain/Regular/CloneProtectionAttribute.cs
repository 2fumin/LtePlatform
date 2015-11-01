using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class CloneProtectionAttribute : Attribute
    {
        public bool Protection { get; set; }

        public CloneProtectionAttribute()
        {
            Protection = true;
        }
    }
}
