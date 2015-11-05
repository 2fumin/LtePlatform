using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
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
