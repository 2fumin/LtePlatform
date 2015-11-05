using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
{
    public static class IOOperations
    {
        public static StreamReader GetStreamReader(this string source)
        {
            byte[] stringAsByteArray = Encoding.UTF8.GetBytes(source);
            Stream stream = new MemoryStream(stringAsByteArray);

            var streamReader = new StreamReader(stream, Encoding.UTF8);
            return streamReader;
        }
    }
}
