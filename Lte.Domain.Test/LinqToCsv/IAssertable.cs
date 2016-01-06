using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.LinqToCsv.Test
{
    public interface IAssertable<in T>
    {
        void AssertEqual(T other);
    }
}
