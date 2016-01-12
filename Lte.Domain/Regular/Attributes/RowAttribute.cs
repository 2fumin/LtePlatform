using System;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RowAttribute : Attribute
    {
        public char InterColumnSplitter { get; set; }

        public char IntraColumnSplitter { get; set; }

        public RowAttribute()
        {
            InterColumnSplitter = ',';
            IntraColumnSplitter = '=';
        }
    }
}
