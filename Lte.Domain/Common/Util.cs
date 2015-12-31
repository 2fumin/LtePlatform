using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common
{
    public class Util
    {
        public static string to_bitstring(int p)
        {
            return p.ToString();
        }

        public static string NormalizeString(string s)
        {
            return s?.Replace("\r\n", "\n");
        }
    }
}
