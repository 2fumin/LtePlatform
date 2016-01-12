using System;

namespace Lte.Domain.Regular.Attributes
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
